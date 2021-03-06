USE [log4service]
GO

/****** Object:  View [dbo].[view_CustomerMontlyTotalInvoice]    Script Date: 3. 7. 2017 16:04:09 ******/
DROP VIEW [dbo].[view_CustomerMontlyTotalInvoice]
GO

/****** Object:  View [dbo].[view_CustomerMontlyTotalInvoice]    Script Date: 3. 7. 2017 16:04:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create view dbo.view_CustomerMontlyTotalInvoice
as
  select i.InvoiceNumber, i.StartBillingPeriod, i. StopBillingPeriod, i.DeliveryDate, i.DueDate
  ,i.CustomerID, i.CustomerIdentification, i.CustomerName
  , sum(i.NumberOfRequest) NumberOfRequest
  , sum(i.ReceivedBytes) ReceivedBytes
  , sum(i.RequestedTime) RequestedTime
  , i.MeasureOfUnits
  , sum(i.TotalPriceWithoutVAT) TotalPriceWithoutVAT
  , sum(i.VAT) VAT
  , sum(i.TotalPriceWithVAT) TotalPriceWithVAT
  FROM [log4service].[dbo].[CATCustomerServiceDetailInvoice] i
  group by i.InvoiceNumber,i.StartBillingPeriod, i. StopBillingPeriod, i.DeliveryDate, i.DueDate
  ,i.CustomerID,i.CustomerIdentification, i.CustomerName, i.MeasureofUnits