USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_DailyLoad]   Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_DailyLoad]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_DailyLoad]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_DailyLoad]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_DailyLoad] 
    @mydebug int = 0,
	@myBatchList varchar(max)
AS
DECLARE
@myQuery nvarchar(max),
@rowCount int = 0
BEGIN
--SELECT DATEADD(month, DATEDIFF(month, 0, getdate()), 0) AS StartOfMonth
-- Insert new 
		SET @myQuery = 'INSERT INTO [dbo].[CATCustomerDailyData]
		(DateOfRequest, [CustomerID], [ServiceID], [NumberOfRequest], [ReceivedBytes], [RequestedTime])   
		select CONVERT(date, i.DateOfRequest), i.CustomerID, i.ServiceID, 
		       count(*), sum(convert(bigint,i.BytesSent)), sum(convert(decimal,i.RequestTime))  from CATLogsOfService i
		where  not exists (select e.CustomerID from CATCustomerDailyData e
		                 where e.CustomerID = i.CustomerID
						 and e.ServiceID = i.ServiceID
						 and e.DateOfRequest = CONVERT(date, i.DateOfRequest))
		and i.BatchID IN'+@myBatchList+'
		and i.CustomerID is not null
		and i.TCActive = 0
		group by CONVERT(date, i.DateOfRequest), i.CustomerID, i.ServiceID';
		EXEC(@myQuery);
		SELECT @rowCount =  @@ROWCOUNT;
		if (@mydebug = 1 ) print 'Insert Daily table record: '+cast(@rowCount as varchar);
			insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('DailyData Insert', @myBatchList, @rowCount);
		IF (@rowCount >0)
		BEGIN
			SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
								SET TCActive = 1
							WHERE  EXISTS (select e.CustomerID FROM CATCustomerDailyData e
								   WHERE e.CustomerID = [CATLogsOfService].CustomerID
									AND e.ServiceID = [CATLogsOfService].ServiceID
									AND e.DateOfRequest = CONVERT(date, [CATLogsOfService].DateOfRequest)) 
							AND	BatchID IN'+@myBatchList+'
							AND CustomerID is not null AND TCActive = 0'
			EXEC(@myQuery);
			SELECT @rowCount =  @@ROWCOUNT;
		    if (@mydebug = 1 ) print 'After Insert SET TCActive = 1 in CATLogsOfService .Number of  record: '+cast(@rowCount as varchar);
		END;
--Update if exist
SET @myQuery = 'UPDATE  [dbo].[CATCustomerDailyData]
						 SET DateOfRequest = u.DateOfRequest
						    ,[CustomerID] = u.[CustomerID]
						    ,[ServiceID] = u.[ServiceID]
						    ,[NumberOfRequest] = CATCustomerDailyData.NumberOfRequest + u.[NumberOfRequest]
						    ,[ReceivedBytes] = CATCustomerDailyData.ReceivedBytes + u.[ReceivedBytes]
						    ,[RequestedTime] = CATCustomerDailyData.RequestedTime + u.[RequestedTime]
							,TCLastUpdate = getdate()
						FROM (select CONVERT(date, i.DateOfRequest) DateOfRequest, i.CustomerID, i.ServiceID, 
							 count(*) [NumberOfRequest] , sum(convert(bigint,i.BytesSent)) [ReceivedBytes], sum(convert(decimal,i.RequestTime)) [RequestedTime]  from CATLogsOfService i
							 where   exists (select e.CustomerID from CATCustomerDailyData e
							 where e.CustomerID = i.CustomerID
							 and e.ServiceID = i.ServiceID
							 and e.DateOfRequest = CONVERT(date, i.DateOfRequest))
							 and i.BatchID  IN'+@myBatchList+'
							 and i.CustomerID is not null
							 and i.TCActive = 0
							 group by CONVERT(date, i.DateOfRequest), i.CustomerID, i.ServiceID) u 
						WHERE 
						     u.DateOfRequest = CATCustomerDailyData.DateOfRequest
						 and u.[CustomerID]  = CATCustomerDailyData.CustomerID
						 and u.[ServiceID]   = CATCustomerDailyData.ServiceID';
						 EXEC(@myQuery);
						 SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'Update Daily table record: '+cast(@rowCount as varchar);
							insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
							values ('DailyData Update', @myBatchList, @rowCount);
						IF (@rowCount >0)
						BEGIN
							SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
												SET TCActive = 1
											WHERE  EXISTS (select e.CustomerID FROM CATCustomerDailyData e
															 WHERE e.CustomerID = [CATLogsOfService].CustomerID
																AND e.ServiceID = [CATLogsOfService].ServiceID
																AND e.DateOfRequest = CONVERT(date, [CATLogsOfService].DateOfRequest)) 
											AND	BatchID IN'+@myBatchList+'
											AND CustomerID is not null AND TCActive = 0'
							EXEC(@myQuery);
							SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'After Update SET TCActive = 1 in CATLogsOfService. Number of  record: '+cast(@rowCount as varchar);
						END;
END


