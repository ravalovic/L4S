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
	@mydebug int = 0
AS
DECLARE
 @rowCount int,
 @ArchivingDay int,
 @ArchiveDetailDataMonth int,
 @ArchiveCumulativeDataMonth int,
 @UnknownServiceStoreDays int,
 @LastArchiveRUN varchar(50)
BEGIN
select  @ArchivingDay = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='ArchivingDay';
select  @LastArchiveRUN = ParamValue from [CONFGeneralSettings] where Paramname='LastArchiveRUN';
select  @ArchiveDetailDataMonth = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='ArchiveDetailDataMonth';
select  @ArchiveCumulativeDataMonth = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='ArchiveCumulativeDataMonth';
select  @UnknownServiceStoreDays = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='UnknownServiceStoreDays';

-- Delete unknown services
delete from CATUnknownService WHERE DATEDIFF( DAY, DateOfRequest, getdate()) > @UnknownServiceStoreDays
SET @rowCount = @@ROWCOUNT;
insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
			values ('Delete CATUnknownService ',  @rowCount);

IF ( (DAY(getdate()) = @ArchivingDay) and (@LastArchiveRUN <> (CAST(FORMAT(YEAR(GETDATE()),'0000')AS VARCHAR) + CAST(FORMAT(MONTH(GETDATE()),'00') AS VARCHAR))))
	BEGIN
	    IF (@ArchiveDetailDataMonth != -1) 
		BEGIN 
			INSERT INTO [dbo].[ARCHLogsOfService]
			           ([BatchID],[RecordID],[CustomerID],[ServiceID],[UserID],[DateOfRequest],[RequestedURL],[RequestStatus],[BytesSent],[RequestTime],[UserAgent],[UserIPAddress])
			SELECT [BatchID],[RecordID],[CustomerID],[ServiceID],[UserID],[DateOfRequest],[RequestedURL],[RequestStatus],[BytesSent],[RequestTime],[UserAgent],[UserIPAddress] 
			FROM [dbo].[CATLogsOfService] WHERE DATEDIFF( MONTH, DateOfRequest, getdate()) > @ArchiveDetailDataMonth
			SET @rowCount = @@ROWCOUNT;
			insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
			values ('Insert to ArchiveLogsOfService',  @rowCount);
		END
		
		DELETE FROM [dbo].[CATLogsOfService] WHERE DATEDIFF( MONTH, DateOfRequest, getdate()) > @ArchiveDetailDataMonth
		SET @rowCount = @@ROWCOUNT;
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Delete from CATLogsOfService',  @rowCount);
		
		IF (@ArchiveCumulativeDataMonth != -1) 
		BEGIN
			INSERT INTO [dbo].[ARCHCustomerDailyData]([DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime])
			SELECT [DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime] 
			FROM [dbo].[CATCustomerDailyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
			SET @rowCount = @@ROWCOUNT;
			insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
			values ('Insert to ArchiveDailyData',  @rowCount);
		END
		
		DELETE FROM [dbo].[CATCustomerDailyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
		SET @rowCount = @@ROWCOUNT;
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Delete from CATCustomerDailyData',  @rowCount);
		
		IF(@ArchiveCumulativeDataMonth != -1)
		BEGIN
			INSERT INTO [dbo].[ARCHCustomerMonthlyData]([DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime])
			SELECT [DateOfRequest],[CustomerID],[ServiceID],[NumberOfRequest],[ReceivedBytes],[RequestedTime] 
			FROM [dbo].[CATCustomerMonthlyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
			SET @rowCount = @@ROWCOUNT;
			insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
			values ('Insert to ArchiveMonthlyData',  @rowCount);    
		END
		
		DELETE FROM [dbo].[CATCustomerMonthlyData] WHERE DATEDIFF( MONTH, [DateOfRequest], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
		SET @rowCount = @@ROWCOUNT;
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Delete from CATCustomerMonthlyData',  @rowCount);    

	END
	ELSE
	BEGIN
		if (@mydebug = 1 ) print 'Archiving is not possible to run in this time.';
    END
END
