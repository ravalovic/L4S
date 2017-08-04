USE [log4service]
GO

/****** Object:  View [dbo].[[[view_DailyData]]]    Script Date: 3. 7. 2017 16:05:46 ******/
DROP VIEW [dbo].[view_DailyData]
GO

/****** Object:  View [dbo].[[[view_DailyData]]]    Script Date: 3. 7. 2017 16:05:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create  view [dbo].[view_DailyData] as
select DateOfRequest
	   ,l.CustomerID
	   ,c.CompanyID as CustomerIdentification, c.CompanyName as CustomerName
	   ,l.ServiceID, s.ServiceCode
	   ,l.NumberOfRequest
	   ,l.ReceivedBytes,  convert(decimal(18,5),l.RequestedTime) as RequestedTime, l.TCActive

from CATCustomerDailyData l, CATCustomerData c, CATServiceParameters s
where c.CompanyID is not null
      and c.PKCustomerDataID = l.CustomerID
	  and s.PKServiceID = l.ServiceID
union all
select DateOfRequest
	   ,l.CustomerID
	   ,c.IndividualID as CustomerIdentification, c.IndividualFirstName+' '+c.IndividualLastName as CustomerName
	   ,l.ServiceID, s.ServiceCode
	   ,l.NumberOfRequest
	   ,l.ReceivedBytes,  convert(decimal(18,5),l.RequestedTime) as RequestedTime, l.TCActive
from CATCustomerDailyData l, CATCustomerData c, CATServiceParameters s
where c.IndividualID is not null
      and c.PKCustomerDataID = l.CustomerID
	  and s.PKServiceID = l.ServiceID