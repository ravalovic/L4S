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
 @ProcessDataStoreDays int,
 @LastArchiveRUN varchar(50),
 @DeleteDaily int =0,
 @DeleteMonthly int =0
BEGIN
select  @ArchivingDay = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='ArchivingDay';
select  @LastArchiveRUN = ParamValue from [CONFGeneralSettings] where Paramname='LastArchiveRUN';
select  @ArchiveDetailDataMonth = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='ArchiveDetailDataMonth';
select  @ArchiveCumulativeDataMonth = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='ArchiveCumulativeDataMonth';
select  @UnknownServiceStoreDays = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='UnknownServiceStoreDays';
select  @ProcessDataStoreDays = CONVERT(int, ParamValue) from [CONFGeneralSettings] where Paramname='ProcessDataStoreDays';

if (@ArchiveDetailDataMonth = -1) OR (@ArchiveDetailDataMonth is null)
 Begin
  SET @DeleteDaily = 1;
  SET @ArchiveDetailDataMonth = 120;
 end
if (@ArchiveCumulativeDataMonth = -1) OR (@ArchiveCumulativeDataMonth is null)
 Begin
  SET @DeleteMonthly = 1;
  SET @ArchiveCumulativeDataMonth = 12;
 end
 if (@UnknownServiceStoreDays = -1) OR (@UnknownServiceStoreDays is null)
 Begin
    SET @UnknownServiceStoreDays = 30;
 end
 if (@ProcessDataStoreDays = -1) OR (@ProcessDataStoreDays is null)
 Begin
    SET @ProcessDataStoreDays = 30;
 end

-- Delete unknown services
    delete from CATUnknownService WHERE DATEDIFF( DAY, DateOfRequest, getdate()) > @UnknownServiceStoreDays;
    SET @rowCount = @@ROWCOUNT;
	IF (@rowCount > 0)
	BEGIN
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
			values ('Delete CATUnknownService ',  @rowCount);
	END
--Delete processing data 
	
	delete from CATProcessStatus WHERE DATEDIFF( DAY, TCInsertTime, getdate()) > @ProcessDataStoreDays;
	SET @rowCount = @@ROWCOUNT;
	IF (@rowCount > 0)
	BEGIN
		  insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		  values ('Delete CATProcessStatus ',  @rowCount);
	END

IF ( (DAY(getdate()) = @ArchivingDay) and (@LastArchiveRUN <> (CAST(right(YEAR(GETDATE())+10000,4)AS VARCHAR) + CAST(right(MONTH(GETDATE())+100,2) AS VARCHAR))))
	BEGIN
	    IF (@DeleteDaily = 0) 
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
		
		
		IF (@DeleteMonthly = 0) 
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
		

		IF(@DeleteMonthly = 0)
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
		

		IF(@DeleteMonthly = 0)
		BEGIN
			INSERT INTO [dbo].[ARCHInputFileInfo] ([FileName] ,[Checksum], [LinesInFile], [InsertDateTime]
							 ,[LoaderBatchID],[LoadedRecord] ,[OriFileName],[OriginalFileChecksum],[TCLastUpdate],[TCActive])
			SELECT  [FileName] ,[Checksum], [LinesInFile], [InsertDateTime]
					 ,[LoaderBatchID],[LoadedRecord] ,[OriFileName],[OriginalFileChecksum],[TCLastUpdate],[TCActive]
			FROM [dbo].[STInputFileInfo] WHERE DATEDIFF( MONTH, [InsertDateTime], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
			SET @rowCount = @@ROWCOUNT;
			insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
			values ('Insert to ARCHInputFileInfo',  @rowCount);    
		END
		
		DELETE FROM [dbo].[STInputFileInfo] WHERE DATEDIFF( MONTH, [InsertDateTime], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
		SET @rowCount = @@ROWCOUNT;
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Delete from STInputFileInfo',  @rowCount);    

		IF(@DeleteMonthly = 0)
		BEGIN
			INSERT INTO [dbo].[ARCHInputFileDuplicity]([OriginalId]
           ,[FileName],[LinesInFile],[Checksum],[LoadDateTime],[InsertDateTime],[OriFileName]
           ,[OriginalFileChecksum],[LoaderBatchID],[TCLastUpdate],[TCActive])
			SELECT [OriginalId]
           ,[FileName],[LinesInFile],[Checksum],[LoadDateTime],[InsertDateTime],[OriFileName]
           ,[OriginalFileChecksum],[LoaderBatchID],[TCLastUpdate],[TCActive]
			FROM [dbo].[STInputFileDuplicity]WHERE DATEDIFF( MONTH, [InsertDateTime], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
			SET @rowCount = @@ROWCOUNT;
			insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
			values ('Insert to [ARCHInputFileDuplicity]',  @rowCount);    
		END
		
		DELETE FROM [dbo].[STInputFileDuplicity] WHERE DATEDIFF( MONTH, [InsertDateTime], getdate()) > @ArchiveCumulativeDataMonth and TCActive = 1
		SET @rowCount = @@ROWCOUNT;
		insert into [dbo].CATProcessStatus ([StepName], [BatchRecordNum])
		values ('Delete from STInputFileInfo',  @rowCount);  
		
		-- Update Generation date
		update [dbo].CONFGeneralSettings
		 SET ParamValue = CAST(right(YEAR(GETDATE())+10000,4)AS VARCHAR) + CAST(right(MONTH(GETDATE())+100,2) AS VARCHAR)
        where ParamName = 'LastArchiveRUN';
		  
	END
	ELSE
	BEGIN
		if (@mydebug = 1 ) print 'Archiving is not possible to run in this time.';
    END
END
