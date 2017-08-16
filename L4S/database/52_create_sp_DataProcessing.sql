USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_DataProcessing]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_DataProcessing]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].sp_DataProcessing
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_DataProcessing]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_DataProcessing] 
@mydebug int = 0,
@tableInput int = 0,
@batchList varchar(max) OUTPUT
AS

DECLARE
       @countNewData int = 0,  
	   @return_value int,
	   @CustomerID int,
	   @ServiceID int,
	   @myUpdateProcessStatus nvarchar(max),
       @myInsert nvarchar(max),
	   @myDelete nvarchar(max),
	   @CustomerQuery nvarchar(max),
	   @serviceQuery nvarchar(max),
	   @rowcountPreprocess int = 0,
       @rowcountService int = 0 ,
	   @rowcountCustomer int = 0,
	   @rowcountUnknown int = 0,
	   @rowcountAll int = 0,
	   @rowcount int = 0
BEGIN
     
    IF (@tableInput = 0) SELECT  @countNewData = count(*) from [dbo].STLogImport;
	ELSE SET @countNewData =1;
	IF (@countNewData>0) 
	BEGIN
	    EXEC	@return_value = [dbo].[sp_GetBatchList]
		        @myInput = @tableInput,
			    @myBatchList = @batchList OUTPUT;
		if (@return_value = 0) 
		BEGIN
		     DECLARE runCursor CURSOR FOR SELECT PKCustomerDataID FROM [dbo].CATCustomerData where TCActive<>99;
		     OPEN runCursor
			 FETCH NEXT FROM runCursor INTO @CustomerID
			 WHILE @@FETCH_STATUS = 0   
			    BEGIN
					EXEC	@return_value = [dbo].[sp_FindCustomer]
							@myCustomerID = @CustomerID,
							@myInput = @tableInput,
							@myBatchList = @batchList,
							@myCustomerQuery = @CustomerQuery OUTPUT;
                      if (@mydebug = 1 ) print @CustomerQuery;
				      EXEC(@CustomerQuery);
					  SELECT @rowcount = @@ROWCOUNT;
					  SET @rowcountCustomer = @rowcountCustomer+ @rowcount;
		              if (@mydebug = 1 ) print 'CustomerID: ' + cast(@CustomerID as varchar) +' find in '+ cast(@rowcount as varchar) +' lines. Total lines of Customer: ' +cast(@rowcountCustomer as varchar);
					  FETCH NEXT FROM runCursor INTO @CustomerID
		        END
				
			 CLOSE runCursor   
			 DEALLOCATE runCursor
		END
		if (@return_value = 0) 	
		BEGIN	
		 DECLARE runCursor CURSOR FOR SELECT PKServiceID FROM [dbo].CATServiceParameters where TCActive<>99;
		     OPEN runCursor
			 FETCH NEXT FROM runCursor INTO @ServiceID
			 WHILE @@FETCH_STATUS = 0   
			    BEGIN
					EXEC	@return_value = [dbo].[sp_FindService]
							@myServiceID = @ServiceID,
							@myInput = @tableInput,
							@myBatchList = @batchList,
							@myserviceQuery = @serviceQuery OUTPUT;
					if (@mydebug = 1 ) print @serviceQuery;
					EXEC(@serviceQuery);
					SELECT @rowcount = @@ROWCOUNT;
					SET @rowcountService =  @rowcountService + @rowcount;
		            if (@mydebug = 1 ) print 'ServiceID: ' + cast(@ServiceID as varchar) +' find in '+ cast(@rowcount as varchar) +' lines. Total lines of Services: '+cast(@rowcountService as varchar);;
					FETCH NEXT FROM runCursor INTO @ServiceID
		        END
			 CLOSE runCursor   
			 DEALLOCATE runCursor
		END
		

		--Unknown services services
	   IF (@tableInput = 0)
		BEGIN
			SET @myInsert = '
							INSERT INTO [dbo].[CATUnknownService]([BatchID],[RecordID],[CustomerID],[ServiceID],[UserID],[DateOfRequest],[RequestedURL],[RequestStatus],[BytesSent],[RequestTime],[UserIPAddress])
							SELECT [BatchID], [RecordID],[CustomerID], 0, [UserID]
								  ,[DatDate],[RequestedURL],[RequestStatus],[BytesSent],[RequestTime],[UserIPAddress]
							FROM [dbo].[STLogImport] WHERE BatchID IN ' + @batchList +'
							and not exists (select s.batchid, s.recordid  from CATLogsOfService s where s.batchid=STLogImport.batchid and s.recordid = STLogImport.recordid)';
			if (@mydebug = 1 ) print @myInsert;
			EXEC(@myInsert);
			SELECT @rowcountUnknown = @@ROWCOUNT;
			if (@mydebug = 1 ) print 'Unknown Service find in '+ cast(@rowcountUnknown as varchar) +' lines';
		
			SET @myDelete = 'DELETE FROM [dbo].[STLogImport] WHERE BatchID IN' + @batchList;
			if (@mydebug = 1 ) print @myDelete;
			EXEC(@myDelete);
			SELECT @rowcountAll = @@ROWCOUNT;
			if (@mydebug = 1 ) print 'Deleted rows from STLogImport '+ cast(@rowcountAll as varchar) +' lines';
			IF ( @batchList > 0 or @rowcountAll >0 or @rowcountService >0 or @rowcountCustomer >0 or @rowcountUnknown>0 or @rowcountPreprocess >0)
			BEGIN
				insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum], [NumberOfService] ,[NumberOfCustomer],[NumberOfUnknownService], [NumberOfPreprocessDelete])
				values ('DataProcessing', @batchList, @rowcountAll,  @rowcountService, @rowcountCustomer, @rowcountUnknown, @rowcountPreprocess);
			END
		END
		ELSE 
		    IF ( @batchList > 0 or @rowcountAll >0 or @rowcountService >0 or @rowcountCustomer >0 or @rowcountUnknown>0 or @rowcountPreprocess >0)
			BEGIN
  				insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum], [NumberOfService] ,[NumberOfCustomer],[NumberOfUnknownService], [NumberOfPreprocessDelete])
				values ('DataReprocessing', @batchList, @rowcountAll,  @rowcountService, @rowcountCustomer, @rowcountUnknown, @rowcountPreprocess);
			END
	END
END
