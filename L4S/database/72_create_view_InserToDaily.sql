USE [log4service]
GO

/****** Object:  View [dbo].[view_InsertToDaily]    Script Date: 3. 7. 2017 16:04:09 ******/
DROP VIEW [dbo].[view_InsertToDaily]
GO

/****** Object:  View [dbo].[view_InsertToDaily]    Script Date: 3. 7. 2017 16:04:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create  view [dbo].[view_InsertToDaily] as
select dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)) DateOfRequest, i.CustomerID, i.ServiceID, 
    count(*) NumberOfRequest, sum(convert(bigint,i.BytesSent)) ReceivedBytes, sum(convert(decimal(18,5),i.RequestTime)) RequestedTime, i.TCActive  from CATLogsOfService i
  group by dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)), i.CustomerID, i.ServiceID, i.TCActive

	
GO

