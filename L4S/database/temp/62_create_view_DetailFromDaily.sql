USE [log4service]
GO

/****** Object:  View [dbo].[view_DetailFromDaily]    Script Date: 3. 7. 2017 16:05:46 ******/
DROP VIEW [dbo].[view_DetailFromDaily]
GO

/****** Object:  View [dbo].[view_DetailFromDaily]    Script Date: 3. 7. 2017 16:05:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create view [dbo].[view_DetailFromDaily] as
select s.BatchID, s.RecordID, s.DateOfRequest, convert(date,s.DateOfRequest) DayDate, s.CustomerID, s.ServiceID, s.BytesSent, s.RequestTime,s.RequestedURL, s.RequestStatus, s.UserIPAddress from CATLogsOfService s
where exists( select d.CustomerID from CATCustomerDailyData d
               where s.CustomerID = d.CustomerID
			   and s.ServiceID = d.ServiceID
			   and convert(date, s.DateOfRequest) = d.DateOfRequest)
GO

