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
	@myBatchList varchar(max)
AS
declare
@myQuery nvarchar(max),
@myTestRowcount int = 0,
@rowCount int = 0
begin
--SELECT DATEADD(month, DATEDIFF(month, 0, getdate()), 0) AS StartOfMonth

SET @myQuery = 'SELECT TCActive from [CATCustomerDailyData] a 
                 where exists (select CustomerID from [CATLogsOfService] b  where batchid in'+@myBatchList+'
				 and TCActive = 0
				 and a.RequestDate = convert(date,b.DateofRequest) 
				 and a.[CustomerID] = b.[CustomerID] 
				 and a.[ServiceID] = b.[ServiceID])';
print @myQuery;
EXEC sp_executesql @myQuery;
SELECT @myTestRowcount =  @@ROWCOUNT;
if (@myTestRowcount = 0)
	BEGIN		
	     SET @myQuery = 'INSERT INTO [dbo].[CATCustomerDailyData]
							([RequestDate], [CustomerID], [ServiceID], [NumberOfRequest], [ReceivedBytes], [RequestedTime])   
						 SELECT convert(date,DateofRequest), [CustomerID], [ServiceID], count(*)
						      , sum(convert (bigint,[BytesSent])), sum(convert (bigint,[RequestTime]))/1000 
						 FROM [dbo].[CATLogsOfService] WHERE CustomerID IS NOT NULL 
						 and  BATCHID IN'+@myBatchList+' 
						 and TCActive = 0
						 GROUP BY convert(date,DateofRequest), [CustomerID], [ServiceID]'
			EXEC sp_executesql @myQuery;
            SELECT @rowCount =  @@ROWCOUNT;	
			insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('DailyData Insert', @myBatchList, @rowCount);
			--mark  that was processed
			IF ( @rowCount > 0 )
			BEGIN
				SET @myQuery = 'UPDATE [dbo].[CATLogsOfService]
									SET TCActive = 1
								WHERE   BatchID IN'+@myBatchList+' AND CustomerID is not null AND TCActive = 0'
				EXEC sp_executesql @myQuery;
			END
		END
		ELSE
		BEGIN
		SET @myQuery = 'UPDATE  [dbo].[CATCustomerDailyData]
						 SET [RequestDate] = i.[RequestDate]
						    ,[CustomerID] = i.[CustomerID]
						    ,[ServiceID] = i.[ServiceID]
						    ,[NumberOfRequest] = CATCustomerDailyData.NumberOfRequest + i.[NumberOfRequest]
						    ,[ReceivedBytes] = CATCustomerDailyData.ReceivedBytes + i.[ReceivedBytes]
						    ,[RequestedTime] = CATCustomerDailyData.RequestedTime + i.[RequestedTime]
							,TCLastUpdate = getdate()
						FROM (SELECT convert(date,DateofRequest) RequestDate
								,[CustomerID]
								,[ServiceID]
								,count(*) [NumberOfRequest]
								,sum(convert (bigint,[BytesSent])) [ReceivedBytes]
								,sum(convert (bigint,[RequestTime]))/1000 [RequestedTime]
							  FROM [dbo].[CATLogsOfService] 
									WHERE CustomerID is not null AND  batchid in'+@myBatchList+' AND TCActive = 0
									GROUP BY convert(date,DateofRequest) 
									      ,[CustomerID]
									      ,[ServiceID]) i 
						WHERE 
						     i.RequestDate = CATCustomerDailyData.RequestDate
						 and i.[CustomerID] = CATCustomerDailyData.CustomerID
						 and i.[ServiceID] = CATCustomerDailyData.ServiceID'
		EXEC sp_executesql @myQuery;
        SELECT @rowCount =  @@ROWCOUNT;
		insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('DailyData Update', @myBatchList, @rowCount);
		IF ( @rowCount > 0 )
		BEGIN
  			--mark  that was processed
			SET @myQuery = 'UPDATE [dbo].[CATLogsOfService]
								SET TCActive = 1
							WHERE   BatchID IN'+@myBatchList+' AND CustomerID is not null AND TCActive = 0'
			EXEC sp_executesql @myQuery;
         
		END
	END
END

