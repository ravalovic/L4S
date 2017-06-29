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
	@myBatchList varchar(max)
AS
declare
@myQuery nvarchar(max),
@myTestRowcount int = 0,
@rowCount int = 0
begin

SET @myQuery = 'SELECT  TCActive from [CATCustomerMonthlyData] a 
                 where exists (select CustomerID from [CATLogsOfService] b  where batchid in'+@myBatchList+'
				 and TCActive = 1
				 and a.RequestDate = DATEADD(month, DATEDIFF(month, 0,convert(date,DateofRequest)), 0)
				 and a.[CustomerID] = b.[CustomerID] 
				 and a.[ServiceID] = b.[ServiceID])';
EXEC (@myQuery);
SELECT @myTestRowcount =  @@ROWCOUNT;

if (@myTestRowcount = 0)
	BEGIN		
	     SET @myQuery = 'INSERT INTO [dbo].[CATCustomerMonthlyData]
							([RequestDate], [CustomerID], [ServiceID], [NumberOfRequest], [ReceivedBytes], [RequestedTime])   
						 SELECT DATEADD(month, DATEDIFF(month, 0,convert(date,DateofRequest)), 0), [CustomerID], [ServiceID], count(*)
						      , sum(convert (bigint,[BytesSent])), sum(convert (decimal,[RequestTime]))
						 FROM [dbo].[CATLogsOfService] WHERE CustomerID IS NOT NULL 
						 and  BATCHID IN'+@myBatchList+' 
						 and  TCActive = 1
						 GROUP BY DATEADD(month, DATEDIFF(month, 0,convert(date,DateofRequest)), 0), [CustomerID], [ServiceID]'
			EXEC(@myQuery);
            SELECT @rowCount =  @@ROWCOUNT;
			insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('MonthlyData Insert', @myBatchList, @rowCount);
			IF ( @rowCount > 0 )
			BEGIN			 
		    --mark  that was processed for month
				SET @myQuery = 'UPDATE [dbo].[CATLogsOfService]
									SET TCActive = 2
								WHERE   BatchID IN'+@myBatchList+' AND CustomerID is not null AND TCActive = 1'
				EXEC(@myQuery);
			END
		END
		ELSE
		BEGIN
		SET @myQuery = 'UPDATE  [dbo].[CATCustomerMonthlyData]
						 SET [RequestDate] = i.[RequestDate]
						    ,[CustomerID] = i.[CustomerID]
						    ,[ServiceID] = i.[ServiceID]
						    ,[NumberOfRequest] = CATCustomerMonthlyData.NumberOfRequest + i.[NumberOfRequest]
						    ,[ReceivedBytes] = CATCustomerMonthlyData.ReceivedBytes + i.[ReceivedBytes]
						    ,[RequestedTime] = CATCustomerMonthlyData.RequestedTime + i.[RequestedTime]
							,TCLastUpdate = getdate()
						FROM (SELECT DATEADD(month, DATEDIFF(month, 0,convert(date,DateofRequest)), 0) RequestDate
								,[CustomerID]
								,[ServiceID]
								,count(*) [NumberOfRequest]
								,sum(convert (bigint,[BytesSent])) [ReceivedBytes]
								,sum(convert (decimal,[RequestTime])) [RequestedTime]
							  FROM [dbo].[CATLogsOfService] 
									WHERE CustomerID is not null AND  batchid in'+@myBatchList+' AND TCActive = 1
									GROUP BY DATEADD(month, DATEDIFF(month, 0,convert(date,DateofRequest)), 0)
									      ,[CustomerID]
									      ,[ServiceID]) i 
						WHERE 
						     i.RequestDate = CATCustomerMonthlyData.RequestDate
						 and i.[CustomerID] = CATCustomerMonthlyData.CustomerID
						 and i.[ServiceID] = CATCustomerMonthlyData.ServiceID'
		EXEC(@myQuery);
        SELECT @rowCount =  @@ROWCOUNT;
		insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
			values ('MonthlyData Update', @myBatchList, @rowCount);

  		--mark  that was processed for month
		IF ( @rowCount > 0 )
		BEGIN
			SET @myQuery = 'UPDATE [dbo].[CATLogsOfService]
								SET TCActive = 2
							WHERE   BatchID IN'+@myBatchList+' AND CustomerID is not null AND TCActive = 1'
			EXEC(@myQuery);
		END
	END
END

