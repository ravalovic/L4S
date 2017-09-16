USE [log4service]
GO

/****** Object:  View [dbo].[view_InsertToMonthly]   Script Date: 3. 7. 2017 16:04:09 ******/
DROP VIEW [dbo].[view_InsertToMonthly]
GO

/****** Object:  View [dbo].[view_InsertToMonthly]   Script Date: 3. 7. 2017 16:04:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create  view [dbo].[view_InsertToMonthly] as
 select DATEADD(month, DATEDIFF(month, 0,convert(date,i.DateOfRequest)), 0) DateOfRequest, i.CustomerID, i.ServiceID, 
 count(*) NumberOfRequest, sum(convert(bigint,i.BytesSent)) ReceivedBytes, sum(convert(decimal(18,5),i.RequestTime)) RequestedTime, i.TCActive from CATLogsOfService i
		group by DATEADD(month, DATEDIFF(month, 0,convert(date,i.DateofRequest)), 0), i.CustomerID, i.ServiceID, i.TCActive;
GO

