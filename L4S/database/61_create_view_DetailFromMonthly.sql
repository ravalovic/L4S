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
select s.BatchID, s.RecordID, s.DateOfRequest, DATEADD(month, DATEDIFF(month, 0,convert(date,s.DateofRequest)), 0) Monthdate, s.CustomerID, s.ServiceID, convert(decimal(18,5),s.BytesSent) as BytesSent,  convert(decimal(18,5),s.RequestTime) as RequestTime, s.RequestedURL, s.RequestStatus, s.UserIPAddress from CATLogsOfService s
where exists( select d.CustomerID from CATCustomerMonthlyData d
               where s.CustomerID = d.CustomerID
			   and s.ServiceID = d.ServiceID
			   and DATEADD(month, DATEDIFF(month, 0,convert(date,s.DateofRequest)), 0) = d.DateOfRequest)
GO

