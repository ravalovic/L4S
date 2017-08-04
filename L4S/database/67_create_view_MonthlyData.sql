USE [log4service]
GO

/****** Object:  View [dbo].[[view_InvoiceByMonth]]    Script Date: 3. 7. 2017 16:05:46 ******/
DROP VIEW [dbo].[view_MonthlyData]
GO

/****** Object:  View [dbo].[[view_InvoiceByMonth]]    Script Date: 3. 7. 2017 16:05:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create  view [dbo].[view_MonthlyData] as
select convert(date,l.DateOfRequest) as DateOfRequest
	   ,l.CustomerID
	   ,c.CompanyID as CustomerIdentification, c.CompanyName as CustomerName
	   ,l.ServiceID, s.ServiceCode
	   ,l.NumberOfRequest
	   ,l.ReceivedBytes,  convert(decimal(18,5),l.RequestedTime) as RequestedTime, l.TCActive

from CATCustomerMonthlyData l, CATCustomerData c, CATServiceParameters s
where c.CompanyID is not null
      and c.PKCustomerDataID = l.CustomerID
	  and s.PKServiceID = l.ServiceID
union all
select convert(date,l.DateOfRequest) as DateOfRequest
	   ,l.CustomerID
	   ,c.IndividualID as CustomerIdentification, c.IndividualFirstName+' '+c.IndividualLastName as CustomerName
	   ,l.ServiceID, s.ServiceCode
	   ,l.NumberOfRequest
	   ,l.ReceivedBytes,  convert(decimal(18,5),l.RequestedTime) as RequestedTime, l.TCActive
from CATCustomerMonthlyData l, CATCustomerData c, CATServiceParameters s
where c.IndividualID is not null
      and c.PKCustomerDataID = l.CustomerID
	  and s.PKServiceID = l.ServiceID



GO

   
