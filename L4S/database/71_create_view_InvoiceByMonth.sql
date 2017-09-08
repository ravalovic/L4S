USE [log4service]
GO

/****** Object:  View [dbo].[[view_InvoiceByMonth]]    Script Date: 3. 7. 2017 16:05:46 ******/
DROP VIEW [dbo].[view_InvoiceByMonth]
GO

/****** Object:  View [dbo].[[view_InvoiceByMonth]]    Script Date: 3. 7. 2017 16:05:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create view [dbo].[view_InvoiceByMonth] as

SELECT [ID]
	  ,[DateofRequest]
      ,[CustomerID]
      ,[CustomerIdentification]
      ,[CustomerName]
      ,[ServiceID]
      ,[ServiceCode]
      ,[ServiceDescription]
      ,[NumberOfRequest]
      ,[ReceivedBytes]
      ,[RequestedTime]
      ,[CustomerServiceCode]
      ,[CustomerServicename]
      ,[UnitPrice]
      ,[MeasureofUnits]
      ,[BasicPriceWithoutVAT]
      ,[VAT]
      ,[BasicPriceWithVAT]
      ,[TCActive]
  FROM [dbo].[CATInvoiceByMonth]
GO


