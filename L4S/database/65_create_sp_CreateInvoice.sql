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

AS
DECLARE
 @rowCount int,
 @DayOfInvoiceCreate int
BEGIN
select  @DayOfInvoiceCreate = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='DenTvorbyFaktury';

-- Update TCActive = 1 for all invoice with 0 If TCActive = 0 mean actual billing period
UPDATE [dbo].[CATBillingInfo]
 SET TCActive = 1
     ,TCLastUpdate = GETDATE()
WHERE TCActive = 0;

--select DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0) --First day of previous month
--select DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1) --Last Day of previous month

--Insert new InvoiceData 
INSERT INTO [dbo].[CATBillingInfo]
           ([StartBillingPeriod], [StopBillingPeriod], [DeliveryDate], [DueDate]
		   ,[InvoiceNumber], [CustomerID], [ServiceID]
           ,[NumberOfRequest], [RequestedTime], [ReceivedBytes], [MeasureofUnits]
           ,[UnitPrice], [TotalPriceWithoutVAT], [VAT], [TotalPriceWithVAT]
           )
SELECT [DateOfRequest], DATEADD(SECOND, -1, DATEADD(DAY, 1, DATEDIFF(DAY, 0,  DATEADD(MONTH, DATEDIFF(MONTH, -1, DateOfRequest), -1)))), DATEADD(MONTH, DATEDIFF(MONTH, -1, DateOfRequest), -1)
      ,[CustomerID]
      ,[ServiceID]
      ,[NumberOfRequest]
      ,[ReceivedBytes]
      ,[RequestedTime]
      ,[ServiceCode]
      ,[ServiceName]
      ,[UnitPrice]
      ,[MeasureOfUnits]
      ,[BasicPriceWithoutVAT]
      ,[VAT]
      ,[BasicPriceWithVAT]
  FROM [dbo].[view_InvoiceByMonth] where [TCActive] = 0 order by [CustomerID], [ServiceID] 

     VALUES
           (<StartBillingPeriod, datetime,>
           ,<StopBillingPeriod, datetime,>
           ,<DeliveryDate, datetime,>
           ,<DueDate, datetime,>
           ,<CustomerID, int,>
           ,<NumberOfRequest, bigint,>
           ,<ReceivedBytes, bigint,>
           ,<RequestedTime, decimal(18,5),>
           ,<UnitPrice, decimal(18,5),>
           ,<TotalPriceWithoutVAT, decimal(18,5),>
           ,<VAT, decimal(18,2),>
           ,<TotalPriceWithVAT, decimal(18,5),>
           ,<TCInsertTime, datetime,>
           ,<TCLastUpdate, datetime,>
           ,<TCActive, int,>
           ,<InvoiceNumber, nvarchar(50),>
           ,<ServiceID, int,>
           ,<MeasureofUnits, nvarchar(50),>)
GO


UPDATE [dbo].[CATCustomerDailyData]
 SET TCActive = 1
     ,TCLastUpdate = GETDATE()
WHERE TCActive = 0;
UPDATE [dbo].[CATCustomerMonthlyData]
 SET TCActive = 1
     ,TCLastUpdate = GETDATE()
WHERE TCActive = 0;

END
