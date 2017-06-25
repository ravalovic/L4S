USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_CATLoadLogsOfServices]    Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_CATLoadLogsOfServices] 
	-- Add the parameters for the stored procedure here
	@ServiceID int = 0
AS

DECLARE
       @countNewData int = 1,
       @myLike varchar(8000),
       @myInsert varchar(8000),
	   @myDelete varchar(8000),
	   @myUpdateProcessStatus varchar(2000),
	   @myUpdateCustomer varchar(8000),
       
	   @myWhere varchar(100) = ' l.RequestedURL like ''',
       @myOR varchar(100) = ' or l.RequestedURL like ''',
       @myENDLike varchar(4) = '''',
       @i int = 0,
	   
	   @myServiceCursorQuery nvarchar(500),
	   @cursorSQL nvarchar(max),
	   @myServiceID int,
	   @serviceCursor cursor, 
	   
	   @myBatchList varchar(200)='0',
	   @myBatch varchar(10),
	   
	   @myCustomerID int,
	   @myCustomerIdentifier varchar(2000),
	   @myCustomerQuery varchar(8000),
	   @myCustomerIdentificator varchar(8000),
	   @myCustomerIdentificatorLike varchar(100) = ' UserIPAddress like ''%',
	   @myCustomerIdentificatorORLike varchar(100) = ' or UserIPAddress like ''%',
	   @myENDLikeC varchar(5) = '%'''
	    
BEGIN
     
    
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--Read batch from STLogImport
	--SELECT  @countNewData = count(*) from [dbo].STLogImport;
	IF (@countNewData>0) 
	BEGIN
	        --list of batch from STLogImport
		    DECLARE cBatch CURSOR FOR SELECT DISTINCT BatchID FROM [dbo].STLogImport;
		    OPEN cBatch
		    FETCH NEXT FROM cBatch INTO @myBatch
		    WHILE @@FETCH_STATUS = 0   
		    BEGIN  
			       SET @myUpdateProcessStatus ='UPDATE [dbo].[CATProcessStatus]
	                              SET [ActualStepName] = ''CatalogServiceLoader''
								    , [ActualStepID] = 2 
									, [ActualStepStatus] = 0 
									, [TCLastUpdate] = GETDATE()
								  WHERE BatchID = ' + @myBatch;
				   print @myUpdateProcessStatus;
				   SET @myBatchList = @myBatchList + ',' + @myBatch;
				   FETCH NEXT FROM cBatch INTO @myBatch
		    END   
		    CLOSE cBatch   
		    DEALLOCATE cBatch
		    --print @myBatchList;
			--All services or only one defined
			IF (@ServiceID=0)
			  SET @myServiceCursorQuery = 'select PKserviceID from [dbo].[CATServiceParameters]';
		    ELSE 
			SET @myServiceCursorQuery = 'select PKserviceID from [dbo].[CATServiceParameters] where PKServiceID = ' + cast(@ServiceID as varchar);

			print @myServiceCursorQuery
			SET @cursorSQL = 'set @cursor = cursor forward_only static for ' + @myServiceCursorQuery + ' open @cursor;'
		    EXEC sys.sp_executesql
		    @cursorSQL
		    ,N'@cursor cursor output'
		    ,@serviceCursor output
		
		    fetch next from @serviceCursor into @ServiceID
		    while (@@fetch_status = 0)
		    begin
				SET @myInsert = '
				INSERT INTO [dbo].[CATLogsOfService]([BatchID],[RecordID],[ServiceID],[UserID],[DateOfRequest],[RequestedURL],[RequestStatus],[BytesSent],[RequestTime],[UserIPAddress])
			    SELECT l.[BatchID], l.[RecordID], ' + cast(@ServiceID as varchar) + ',l.[UserID]
				       ,dateadd(hour,convert(int,substring([DateOfRequest],len([DateOfRequest])-5,4)) ,convert(datetime, substring([DateOfRequest],0,12)+'' ''+ substring([DateOfRequest],13,8),104))
					   ,l.[RequestedURL],l.[RequestStatus],l.[BytesSent],l.[RequestTime],l.[UserIPAddress]
			   FROM [dbo].[STLogImport] l WHERE BatchID IN('+@myBatchList+') AND (';
			   SET @myDelete = 'DELETE FROM [dbo].[STLogImport] l WHERE BatchID IN('+@myBatchList+') AND (';
		        declare cLike CURSOR FOR select p.PatternLike from CATServicePatterns p where p.FKServiceID = @ServiceID;
		        OPEN cLike
		        FETCH NEXT FROM cLike INTO @myLike
		        WHILE @@FETCH_STATUS = 0   
		        BEGIN   
		               IF (@i=0)
						BEGIN
		        			SET @myInsert = @myInsert + @myWhere + @myLike+  @myENDLike;
							SET @myDelete = @myDelete + @myWhere + @myLike+  @myENDLike;
						END
		        	   ELSE 
						BEGIN
		        			SET @myInsert = @myInsert + @myOR + @myLike + @myENDLike;
							SET @myDelete = @myDelete + @myOR + @myLike + @myENDLike;
						END
		               FETCH NEXT FROM cLike INTO @myLike   
		        	   SET @i = @i+1;
		        END  
				SET @myInsert = @myInsert +')'; 
		        CLOSE cLike   
		        DEALLOCATE cLike
				print cast(@ServiceID as varchar);
				print @myInsert;
				--exec(@myInsert);
				print @myDelete;
				--exec(@myDelete);
				SET @i = 0;
		        fetch next from @serviceCursor into @ServiceID
		    end
		    close @serviceCursor
		    deallocate @serviceCursor
		
		 --   --Update CustomerID 

		    SET @i = 0;
		    DECLARE cCustomer CURSOR FOR SELECT DISTINCT PKCustomerDataID FROM [dbo].CATCustomerData;
		    OPEN cCustomer
		    FETCH NEXT FROM cCustomer INTO @myCustomerID
		    WHILE @@FETCH_STATUS = 0   
		    BEGIN  
					SET @myCustomerQuery = 'UPDATE [dbo].[CATLogsOfService]
											SET 
												 [CustomerID] = '+cast(@myCustomerID as varchar)+'
												,[TCLastUpdate] = getdate()
											WHERE BatchID IN('+@myBatchList+') AND (';
												
					DECLARE cIdentifier CURSOR FOR SELECT  CustomerIdentifier FROM [dbo].CATCustomerIdentifiers WHERE  FKCustomerID = @myCustomerID
					OPEN cIdentifier
					FETCH NEXT FROM cIdentifier INTO @myCustomerIdentifier
					WHILE @@FETCH_STATUS = 0   
					BEGIN  
						IF (@i=0)
						 BEGIN
		        			--SET @myInsert = @myInsert + @myWhere + @myLike+  @myENDLike;
							SET @myCustomerQuery = @myCustomerQuery + @myCustomerIdentificatorLike + @myCustomerIdentifier+  @myENDLikeC;
							
						 END
		        	    ELSE 
						 BEGIN
		        			SET @myCustomerQuery = @myCustomerQuery + @myCustomerIdentificatorORLike + @myCustomerIdentifier + @myENDLikeC;
						 END
		               FETCH NEXT FROM cIdentifier INTO @myCustomerIdentifier
					   SET @i = @i+1;
					END   
					SET @myCustomerQuery = @myCustomerQuery +')'; 
					CLOSE cIdentifier   
					DEALLOCATE cIdentifier
					SET @i = 0;
				    print @myCustomerQuery;
					FETCH NEXT FROM cCustomer INTO @myCustomerID
		    END   
		    CLOSE cCustomer   
		    DEALLOCATE cCustomer







		 SET @myUpdateProcessStatus ='UPDATE [dbo].[CATProcessStatus]
	                              SET [ActualStepStatus] = 1 
								     ,[TCLastUpdate] = GETDATE()
								  WHERE BatchID IN (' + @myBatchList + ') AND 
								  [ActualStepID] = 2';
         print @myUpdateProcessStatus;
		 --exec (@myUpdateProcessStatus);
		 

	END
END
