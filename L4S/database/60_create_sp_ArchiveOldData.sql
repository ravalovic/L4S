USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_ArchiveOldData]  Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_ArchiveOldData]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_ArchiveOldData]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_ArchiveOldData]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ArchiveOldData] 

AS
DECLARE
 @rowCount int

BEGIN
INSERT INTO [dbo].[ARCHLogsOfService]
           ([BatchID],[RecordID],[CustomerID],[ServiceID],[UserID],[DateOfRequest],[RequestedURL],[RequestStatus],[BytesSent],[RequestTime],[UserAgent],[UserIPAddress])
SELECT [BatchID],[RecordID],[CustomerID],[ServiceID],[UserID],[DateOfRequest],[RequestedURL],[RequestStatus],[BytesSent],[RequestTime],[UserAgent],[UserIPAddress] 
FROM [dbo].[CATLogsOfService] WHERE DATEDIFF( MONTH, DateOfRequest, getdate()) > 6

DELETE FROM [dbo].[CATLogsOfService] WHERE DATEDIFF( MONTH, DateOfRequest, getdate()) > 6
SET @rowCount = @@ROWCOUNT;
insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
values ('ArchiveLogsOfService',  @rowCount);

INSERT INTO [dbo].[ARCHCustomerDailyData]([DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime])
SELECT [DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime] 
FROM [dbo].[CATCustomerDailyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > 12

DELETE FROM [dbo].[CATCustomerDailyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > 12
SET @rowCount = @@ROWCOUNT;
insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
values ('ArchiveDailyData',  @rowCount);


INSERT INTO [dbo].[ARCHCustomerMonthlyData]([DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime])
SELECT [DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime] 
FROM [dbo].[CATCustomerMonthlyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > 12

DELETE FROM [dbo].[CATCustomerMonthlyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > 12
SET @rowCount = @@ROWCOUNT;
insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
values ('ArchiveMonthlyData',  @rowCount);    

END
