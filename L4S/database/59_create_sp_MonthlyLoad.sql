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
		select i.DateOfRequest, i.CustomerID, i.ServiceID, i.NumberOfRequest, i.ReceivedBytes, i.RequestedTime from view_InsertToMonthly i
		where  not exists (select e.DateOfRequest, e.CustomerID, e.ServiceID from CATCustomerMonthlyData e
								 where e.CustomerID = i.CustomerID
								 and e.ServiceID = i.ServiceID	
								 and year(e.DateOfRequest)=year(i.DateOfRequest)
								 and month(e.DateOfRequest)=month(i.DateOfRequest)
								 )
		and i.CustomerID is not null
		and i.TCActive = 1';
		EXEC(@myQuery);
		SELECT @rowCount =  @@ROWCOUNT;
		if (@mydebug = 1 ) print 'Insert Monthly table record: '+cast(@rowCount as varchar);
		IF 	(@myBatchList != null)
		BEGIN
			insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('MonthlyData Insert', @myBatchList, @rowCount);
		END
		IF (@rowCount >0)
		BEGIN
			SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
								SET TCActive = 2
							WHERE  EXISTS (select e.DateOfRequest, e.CustomerID, e.ServiceID FROM CATCustomerMonthlyData e
											WHERE e.CustomerID = [CATLogsOfService].CustomerID
											 and e.ServiceID = [CATLogsOfService].ServiceID
											 and year(e.DateOfRequest)=year([CATLogsOfService].DateOfRequest)
											 and month(e.DateOfRequest)=month([CATLogsOfService].DateOfRequest)) 
							AND CustomerID is not null 
							AND TCActive = 1'
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
						FROM (select i.DateOfRequest, i.CustomerID, i.ServiceID, i.NumberOfRequest, i.ReceivedBytes, i.RequestedTime from view_InsertToMonthly i
								where  exists (select e.DateOfRequest, e.CustomerID, e.ServiceID from CATCustomerMonthlyData e
								 where e.CustomerID = i.CustomerID
								 and e.ServiceID = i.ServiceID	
								 and year(e.DateOfRequest)=year(i.DateOfRequest)
								 and month(e.DateOfRequest)=month(i.DateOfRequest))
								and i.CustomerID is not null
								and i.TCActive = 1) u 
						WHERE 
						     u.DateOfRequest = CATCustomerMonthlyData.DateOfRequest
						 and u.[CustomerID]  = CATCustomerMonthlyData.CustomerID
						 and u.[ServiceID]   = CATCustomerMonthlyData.ServiceID';
						 EXEC(@myQuery);
						 SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'Update Monthly table record: '+cast(@rowCount as varchar);
						IF 	(@myBatchList != null)
						BEGIN	
							insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
							values ('MonthlyData Update', @myBatchList, @rowCount);
						END
						IF (@rowCount >0)
						BEGIN
							SET @myQuery = 'UPDATE [dbo].[CATLogsOfService] 
								SET TCActive = 2
							WHERE  EXISTS (select e.DateOfRequest, e.CustomerID, e.ServiceID FROM CATCustomerMonthlyData e
											WHERE e.CustomerID = [CATLogsOfService].CustomerID
											 and e.ServiceID = [CATLogsOfService].ServiceID
											 and year(e.DateOfRequest)=year([CATLogsOfService].DateOfRequest)
											 and month(e.DateOfRequest)=month([CATLogsOfService].DateOfRequest)) 
							AND CustomerID is not null 
							AND TCActive = 1'
							EXEC(@myQuery);
							SELECT @rowCount =  @@ROWCOUNT;
							if (@mydebug = 1 ) print 'After Update SET TCActive = 2 in CATLogsOfService. Number of  record: '+cast(@rowCount as varchar);
						END;

END

