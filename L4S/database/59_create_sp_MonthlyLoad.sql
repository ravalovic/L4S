USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_MonthlyLoad]   Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_MonthlyLoad]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_MonthlyLoad]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_MonthlyLoad]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_MonthlyLoad] 
	@mydebug int = 0,
	@myBatchList varchar(max)
AS
DECLARE
@myQuery nvarchar(max),
@rowCount int = 0
BEGIN
-- Insert new 
		SET @myQuery = 'INSERT INTO [dbo].[CATCustomerMonthlyData]
		(DateOfRequest, [CustomerID], [ServiceID], [NumberOfRequest], [ReceivedBytes], [RequestedTime])   
		select DATEADD(month, DATEDIFF(month, 0,convert(date,i.DateofRequest)), 0), i.CustomerID, i.ServiceID, 
		       count(*), sum(convert(bigint,i.BytesSent)), sum(convert(decimal,i.RequestTime))  from CATLogsOfService i
		where  not exists (select e.CustomerID from CATCustomerMonthlyData e
		                 where e.CustomerID = i.CustomerID
						 and e.ServiceID = i.ServiceID
						 and e.DateOfRequest = CONVERT(date, i.DateOfRequest))
		and i.BatchID IN'+@myBatchList+'
		and i.CustomerID is not null
		and i.TCActive = 1
		group by DATEADD(month, DATEDIFF(month, 0,convert(date,i.DateofRequest)), 0), i.CustomerID, i.ServiceID';
		EXEC(@myQuery);
		SELECT @rowCount =  @@ROWCOUNT;
		if (@mydebug = 1 ) print 'Insert Monthly table record: '+cast(@rowCount as varchar);
			insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('MonthlyData Insert', @myBatchList, @rowCount);
		IF (@rowCount >0)
		BEGIN
			SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
								SET TCActive = 2
							WHERE  EXISTS (select e.CustomerID FROM CATCustomerMonthlyData e
								   WHERE e.CustomerID = [CATLogsOfService].CustomerID
									AND e.ServiceID = [CATLogsOfService].ServiceID
									AND e.DateOfRequest = DATEADD(month, DATEDIFF(month, 0,convert(date,[CATLogsOfService].DateOfRequest)), 0)) 
							AND	BatchID IN'+@myBatchList+'
							AND CustomerID is not null AND TCActive = 1'
			EXEC(@myQuery);
			SELECT @rowCount =  @@ROWCOUNT;
		    if (@mydebug = 1 ) print 'After Insert SET TCActive = 2 in CATLogsOfService .Number of  record: '+cast(@rowCount as varchar);
		END;
--Update if exist
SET @myQuery = 'UPDATE  [dbo].[CATCustomerMonthlyData]
						 SET DateOfRequest = u.DateOfRequest
						    ,[CustomerID] = u.[CustomerID]
						    ,[ServiceID] = u.[ServiceID]
						    ,[NumberOfRequest] = CATCustomerMonthlyData.NumberOfRequest + u.[NumberOfRequest]
						    ,[ReceivedBytes] = CATCustomerMonthlyData.ReceivedBytes + u.[ReceivedBytes]
						    ,[RequestedTime] = CATCustomerMonthlyData.RequestedTime + u.[RequestedTime]
							,TCLastUpdate = getdate()
						FROM (select DATEADD(month, DATEDIFF(month, 0,convert(date,i.DateofRequest)), 0) DateOfRequest, i.CustomerID, i.ServiceID, 
							 count(*) [NumberOfRequest] , sum(convert(bigint,i.BytesSent)) [ReceivedBytes], sum(convert(decimal,i.RequestTime)) [RequestedTime]  from CATLogsOfService i
							 where   exists (select e.CustomerID from CATCustomerMonthlyData e
							 where e.CustomerID = i.CustomerID
							 and e.ServiceID = i.ServiceID
							 and e.DateOfRequest = CONVERT(date, i.DateOfRequest))
							 and i.BatchID  IN'+@myBatchList+'
							 and i.CustomerID is not null
							 and i.TCActive = 1
							 group by DATEADD(month, DATEDIFF(month, 0,convert(date,i.DateofRequest)), 0), i.CustomerID, i.ServiceID) u 
						WHERE 
						     u.DateOfRequest = CATCustomerMonthlyData.DateOfRequest
						 and u.[CustomerID]  = CATCustomerMonthlyData.CustomerID
						 and u.[ServiceID]   = CATCustomerMonthlyData.ServiceID';
						 EXEC(@myQuery);
						 SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'Update Monthly table record: '+cast(@rowCount as varchar);
							insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
							values ('MonthlyData Update', @myBatchList, @rowCount);
						IF (@rowCount >0)
						BEGIN
							SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
								SET TCActive = 2
							WHERE  EXISTS (select e.CustomerID FROM CATCustomerMonthlyData e
								   WHERE e.CustomerID = [CATLogsOfService].CustomerID
									AND e.ServiceID = [CATLogsOfService].ServiceID
									AND e.DateOfRequest = DATEADD(month, DATEDIFF(month, 0,convert(date,[CATLogsOfService].DateOfRequest)), 0)) 
							AND	BatchID IN'+@myBatchList+'
							AND CustomerID is not null AND TCActive = 1'
							EXEC(@myQuery);
							EXEC(@myQuery);
							SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'After Update SET TCActive = 2 in CATLogsOfService. Number of  record: '+cast(@rowCount as varchar);
						END;

END

