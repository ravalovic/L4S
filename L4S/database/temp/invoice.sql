USE [log4service]
GO

SELECT [ID]
      ,[StartBillingPeriod]
      ,[StopBillingPeriod]
      ,[DeliveryDate]
      ,[DueDate]
      ,[CustomerID]
	  ,ServiceID
      ,[NumberOfRequest]
      ,[ReceivedBytes]
      ,[RequestedTime]
      ,[UnitPrice]
      ,[TotalPriceWithoutVAT]
      ,[VAT]
      ,[TotalPriceWithVAT]
      ,[TCInsertTime]
      ,[TCLastUpdate]
      ,[TCActive]
      ,[InvoiceNumber]
      ,[ServiceID]
      ,[MeasureofUnits]
      ,[ServiceCode]
      ,[ServiceName]
  FROM [dbo].[CATBillingInfo]
GO

USE [log4service]
GO
select      b.InvoiceNumber, b.StartBillingPeriod, b.StopBillingPeriod, b.DeliveryDate, b.DueDate, b.CustomerID
           
		   ,sum(Case 
            when g.ParamValue = UPPER('NUMREQ') then (b.NumberOfRequest) 
	        when g.ParamValue = UPPER('BYTE') then (b.ReceivedBytes)
	        when g.ParamValue = UPPER('MBYTE') then (b.ReceivedBytes)
	        when g.ParamValue = UPPER('GBYTE') then (b.ReceivedBytes)
           end) as NumberOfUnits 
		   , b.MeasureofUnits 
		   ,sum(b.ReceivedBytes) as NumberOfUnits
		   ,sum(b.TotalPriceWithoutVAT) TotalPriceWithoutVAT, sum(b.VAT) VAT, sum(b.TotalPriceWithVAT)  TotalPriceWithVAT
		   from [dbo].[CATBillingInfo] b
		   , CONFGeneralSettings g
		   where g.ParamName = UPPER('METRICUNIT')
group by b.InvoiceNumber, b.StartBillingPeriod, b.StopBillingPeriod, b.DeliveryDate, b.DueDate, b.CustomerID, b.MeasureofUnits 
,g.ParamValue





INSERT INTO [dbo].[CATSummaryInvoice]
           ([InvoiceNumber]
           ,[StartBillingPeriod]
           ,[StopBillingPeriod]
           ,[DeliveryDate]
           ,[DueDate]
           ,[CustomerID]
           ,[NumberOfUnits]
           ,[MeasureofUnits]
           ,[UnitPrice]
           ,[TotalPriceWithoutVAT]
           ,[VAT]
           ,[TotalPriceWithVAT]
           ,[TCInsertTime]
           ,[TCLastUpdate]
           ,[TCActive])
     VALUES
           (<InvoiceNumber, nvarchar(50),>
           ,<StartBillingPeriod, datetime,>
           ,<StopBillingPeriod, datetime,>
           ,<DeliveryDate, datetime,>
           ,<DueDate, datetime,>
           ,<CustomerID, int,>
           ,<NumberOfUnits, bigint,>
           ,<MeasureofUnits, nvarchar(50),>
           ,<UnitPrice, decimal(18,2),>
           ,<TotalPriceWithoutVAT, decimal(18,2),>
           ,<VAT, decimal(18,2),>
           ,<TotalPriceWithVAT, decimal(18,2),>
           ,<TCInsertTime, datetime,>
           ,<TCLastUpdate, datetime,>
           ,<TCActive, int,>)
GO