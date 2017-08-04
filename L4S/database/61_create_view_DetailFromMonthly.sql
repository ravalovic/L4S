USE [log4service]
GO

/****** Object:  View [dbo].[view_DetailFromMonthly]    Script Date: 3. 7. 2017 16:04:09 ******/
DROP VIEW [dbo].[view_DetailFromMonthly]
GO

/****** Object:  View [dbo].[view_DetailFromMonthly]    Script Date: 3. 7. 2017 16:04:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create  view [dbo].[view_DetailFromMonthly] as
select l.BatchID, l.RecordID 
       ,l.DateOfRequest, convert(date,DATEADD(month, DATEDIFF(month, 0,convert(date,l.DateofRequest)), 0)) Monthdate
	   ,l.CustomerID
	   ,c.CompanyID as CustomerIdentification, c.CompanyName as CustomerName
	   ,l.ServiceID, s.ServiceCode
	   ,l.BytesSent,  convert(decimal(18,5),l.RequestTime) as RequestTime, l.RequestedURL, l.RequestStatus, l.UserIPAddress, l.TCActive, l.TCInsertTime

from CATLogsOfService l, CATCustomerData c, CATServiceParameters s
where  c.CompanyID is not null 
       and exists( select d.CustomerID from CATCustomerMonthlyData d
               where l.CustomerID = d.CustomerID
			   and l.ServiceID = d.ServiceID
			   and DATEADD(month, DATEDIFF(month, 0,convert(date,l.DateofRequest)), 0) = d.DateOfRequest)
	  and c.PKCustomerDataID = l.CustomerID
	  and s.PKServiceID = l.ServiceID
	  
UNION ALL

select l.BatchID, l.RecordID 
       ,l.DateOfRequest, convert(date,DATEADD(month, DATEDIFF(month, 0,convert(date,l.DateofRequest)), 0)) Monthdate
	   ,l.CustomerID
	   ,c.IndividualID as CustomerIdentification, c.IndividualFirstName+' '+c.IndividualLastName as CustomerName
	   ,l.ServiceID, s.ServiceCode
	   ,l.BytesSent,  convert(decimal(18,5),l.RequestTime) as RequestTime, l.RequestedURL, l.RequestStatus, l.UserIPAddress, l.TCActive, l.TCInsertTime

from CATLogsOfService l, CATCustomerData c, CATServiceParameters s
where c.IndividualID is not null
	  and  exists( select d.CustomerID from CATCustomerMonthlyData d
               where l.CustomerID = d.CustomerID
			   and l.ServiceID = d.ServiceID
			   and DATEADD(month, DATEDIFF(month, 0,convert(date,l.DateofRequest)), 0) = d.DateOfRequest)
	  and c.PKCustomerDataID = l.CustomerID
	  and s.PKServiceID = l.ServiceID
	
GO

