USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_CreateInvoice]  Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_CreateInvoice]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_CreateInvoice]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateInvoice]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CreateInvoice] 
	@mydebug int = 0
AS
DECLARE
 @rowCount int,
 @DayOfInvoiceCreate int,
 @LastInvoiceGenerate varchar(50)
BEGIN
	select  @DayOfInvoiceCreate = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='InvoiceCreationDay';
	select  @LastInvoiceGenerate = ParamValue from [CONFGeneralSettings] where Paramname='LastInvoiceGenerate'
	IF ( (DAY(getdate()) = @DayOfInvoiceCreate) and (@LastInvoiceGenerate <> CAST(FORMAT(YEAR(GETDATE()),'0000')AS VARCHAR) + CAST(FORMAT(MONTH(GETDATE()),'00') AS VARCHAR)))
	BEGIN
	    -- Update Generation date
		update [dbo].CONFGeneralSettings
		 SET ParamValue = CAST(FORMAT(YEAR(GETDATE()),'0000')AS VARCHAR) + CAST(FORMAT(MONTH(GETDATE()),'00') AS VARCHAR)
        where ParamName = 'LastInvoiceGenerate';

		-- Update TCActive = 1 for all invoice with 0 If TCActive = 0 mean actual billing period
		UPDATE [dbo].[CATBillingInfo]
		 SET TCActive = 1
		     ,TCLastUpdate = GETDATE()
		 WHERE TCActive = 0;

			--select DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0) --First day of previous month
			--select DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1) --Last Day of previous month

		--Insert new InvoiceData 
		INSERT INTO [dbo].[CATBillingInfo]
		       ([StartBillingPeriod]
			   ,[StopBillingPeriod]
			   ,[DeliveryDate]
			   ,[DueDate]
			   ,[InvoiceNumber]
			   ,[CustomerID], [ServiceID], [ServiceCode], [ServiceName]
		       ,[NumberOfRequest], [RequestedTime], [ReceivedBytes], [MeasureofUnits]
		       ,[UnitPrice], [TotalPriceWithoutVAT], [VAT], [TotalPriceWithVAT]
		       )
		SELECT [DateOfRequest]
		       , DATEADD(SECOND, -1, DATEADD(DAY, 1, DATEDIFF(DAY, 0,  DATEADD(MONTH, DATEDIFF(MONTH, -1, v.DateOfRequest), -1))))
			   , cast(DATEADD(MONTH, DATEDIFF(MONTH, -1, v.DateOfRequest), -1) as date)
		       , DATEADD(DAY, DATEDIFF(DAY,0, GETDATE()),convert(int,p.ParamValue))
			   , SUBSTRING(CAST(YEAR(v.DateOfRequest) AS VARCHAR),3,2) + CAST(FORMAT(MONTH(v.DateOfRequest),'00') AS VARCHAR) + FORMAT(v.CustomerID,'000000')
		       , v.CustomerID, v.ServiceID, v.ServiceCode, v.ServiceName
		       , v.NumberOfRequest, v.RequestedTime, v.ReceivedBytes, v.MeasureOfUnits, v.BasicPriceWithVAT
		       , v.UnitPrice, v.BasicPriceWithoutVAT, v.VAT  
		FROM view_InvoiceByMonth v, CONFGeneralSettings p where v.TCActive = 0 and p.ParamName='DueDateDays' order by DateOfRequest, CustomerID, ServiceID 
		SELECT @rowcount = @@ROWCOUNT;
		if (@mydebug = 1 ) print 'Create ' + cast(@rowcount as varchar) +' invoices ';

		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Create Invoices: NumberOfInvoiceRecord',  @rowCount);

		UPDATE [dbo].[CATCustomerDailyData]
		SET TCActive = 1
			 ,TCLastUpdate = GETDATE()
		WHERE  
		EXISTS (select e.CustomerID FROM CATBillingInfo e
									   WHERE e.CustomerID = [CATCustomerDailyData].CustomerID
										AND e.ServiceID = [CATCustomerDailyData].ServiceID
										AND e.StartBillingPeriod = DATEADD(MONTH, DATEDIFF(MONTH, 0, CONVERT(date, [CATCustomerDailyData].DateOfRequest)), 0)
										) 
		AND	TCActive = 0;
		SELECT @rowcount = @@ROWCOUNT;
		if (@mydebug = 1 ) print 'Mark ' + cast(@rowcount as varchar) +'lines from [CATCustomerDailyData] as processed for Invoice ';
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Create Invoices: MarkDailyRecods as processed',  @rowCount);
		UPDATE [dbo].[CATCustomerMonthlyData]
		SET TCActive = 1
			 ,TCLastUpdate = GETDATE()
		WHERE  
		EXISTS (select e.CustomerID FROM CATBillingInfo e
									   WHERE e.CustomerID = [CATCustomerMonthlyData].CustomerID
										AND e.ServiceID = [CATCustomerMonthlyData].ServiceID
										AND e.StartBillingPeriod = [CATCustomerMonthlyData].DateOfRequest
										) 
		AND	TCActive = 0;
		SELECT @rowcount = @@ROWCOUNT;
		if (@mydebug = 1 ) print 'Mark ' + cast(@rowcount as varchar) +'lines from [CATCustomerMonthlyData] as processed for Invoice ';
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Create Invoices: MarkMonthlyRecods as processed',  @rowCount);

		-- create summaryInvoice
	END
	ELSE
	BEGIN
		if (@mydebug = 1 ) print 'Create Invoice is not possible to run in this time.';
    END
END

