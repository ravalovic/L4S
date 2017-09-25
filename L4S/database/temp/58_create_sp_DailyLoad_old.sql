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
		select dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)), i.CustomerID, i.ServiceID, 
		       count(*), sum(convert(bigint,i.BytesSent)), sum(convert(decimal(18,5),i.RequestTime))  from CATLogsOfService i
		where  not exists (select e.CustomerID from CATCustomerDailyData e
		                 where e.CustomerID = i.CustomerID
						 and e.ServiceID = i.ServiceID
						 and dateadd(second, 0, dateadd(day, datediff(day, 0, e.DateOfRequest), 0)) = dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)))
		and i.BatchID IN'+@myBatchList+'
		and i.CustomerID is not null
		and i.TCActive = 0
		group by dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)), i.CustomerID, i.ServiceID';
		EXEC(@myQuery);
		SELECT @rowCount =  @@ROWCOUNT;
		if (@mydebug = 1 ) print 'Insert Daily table record: '+cast(@rowCount as varchar);
		IF 	(@myBatchList != null)
		BEGIN	
			insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('DailyData Insert', @myBatchList, @rowCount);
		END
		IF (@rowCount >0)
		BEGIN
			SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
								SET TCActive = 1
							WHERE  EXISTS (select e.CustomerID FROM CATCustomerDailyData e
								   WHERE e.CustomerID = [CATLogsOfService].CustomerID
									AND e.ServiceID = [CATLogsOfService].ServiceID
									AND e.DateOfRequest = dateadd(second, 0, dateadd(day, datediff(day, 0, [CATLogsOfService].DateOfRequest), 0))) 
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
						FROM (select dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)) DateOfRequest, i.CustomerID, i.ServiceID, 
							 count(*) [NumberOfRequest] , sum(convert(bigint,i.BytesSent)) [ReceivedBytes], sum(convert(decimal(18,5),i.RequestTime)) [RequestedTime]  from CATLogsOfService i
							 where   exists (select e.CustomerID from CATCustomerDailyData e
							 where e.CustomerID = i.CustomerID
							 and e.ServiceID = i.ServiceID
							 and e.DateOfRequest = dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)))
							 and i.BatchID  IN'+@myBatchList+'
							 and i.CustomerID is not null
							 and i.TCActive = 0
							 group by dateadd(second, 0, dateadd(day, datediff(day, 0, i.DateOfRequest), 0)), i.CustomerID, i.ServiceID) u 
						WHERE 
						     u.DateOfRequest = CATCustomerDailyData.DateOfRequest
						 and u.[CustomerID]  = CATCustomerDailyData.CustomerID
						 and u.[ServiceID]   = CATCustomerDailyData.ServiceID';
						 EXEC(@myQuery);
						 SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'Update Daily table record: '+cast(@rowCount as varchar);
						IF 	(@myBatchList != null)
						BEGIN
							insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
							values ('DailyData Update', @myBatchList, @rowCount);
						END
						IF (@rowCount >0)
						BEGIN
							SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
												SET TCActive = 1
											WHERE  EXISTS (select e.CustomerID FROM CATCustomerDailyData e
															 WHERE e.CustomerID = [CATLogsOfService].CustomerID
																AND e.ServiceID = [CATLogsOfService].ServiceID
																AND e.DateOfRequest = dateadd(second, 0, dateadd(day, datediff(day, 0, [CATLogsOfService].DateOfRequest), 0))) 
											AND	BatchID IN'+@myBatchList+'
											AND CustomerID is not null AND TCActive = 0'
							EXEC(@myQuery);
							SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'After Update SET TCActive = 1 in CATLogsOfService. Number of  record: '+cast(@rowCount as varchar);
						END;
END


