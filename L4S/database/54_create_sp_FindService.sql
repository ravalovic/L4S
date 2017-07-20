USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_FindService]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_FindService]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].sp_FindService
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_FindService]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].sp_FindService
	-- Add the parameters for the stored procedure here
	@myServiceID int = 0,
	@myInput int = 0,
	@myBatchList varchar(max),
	@myServiceQuery varchar(max) ='' out
AS
DECLARE
       @myLike varchar(max),
	   @first int = 0,
	   @myWhere varchar(max) = ' RequestedURL like ''',
       @myOR varchar(max) = ' or RequestedURL like ''',
       @myENDLike varchar(max) = '''',
	   @myTable varchar(100),
	   @myDate varchar(100)
BEGIN
     IF (@myInput = 0) 
		BEGIN
			SET @myTable = '[dbo].[STLogImport]';
			SET @myDate = '[DatDate]';
		END
	 IF (@myInput = 1) 
		BEGIN
			SET @myTable = '[dbo].[CATUnknownService]';
			SET @myDate = '[DateOfRequest]';
		END

    SET NOCOUNT ON;
	--Get service parameters
		   SET @myServiceQuery = '
				INSERT INTO [dbo].[CATLogsOfService]([BatchID], [RecordID],[CustomerID], [ServiceID], [UserID], [DateOfRequest], [RequestedURL], [RequestStatus], [BytesSent], [RequestTime], [UserIPAddress])
			    SELECT [BatchID], [RecordID],[CustomerID], ' + cast(@myServiceID as varchar) + ', [UserID], '+@myDate+'
					   ,[RequestedURL], [RequestStatus], [BytesSent], [RequestTime], [UserIPAddress] FROM '+@myTable+' WHERE BatchID IN '+@myBatchList+' AND (';
			DECLARE myCursor CURSOR FOR SELECT PatternLike FROM [dbo].[CATServicePatterns] WHERE FKServiceID = @myServiceID and TCActive<>99;
		    OPEN myCursor
		    FETCH NEXT FROM myCursor INTO @myLike
		    WHILE @@FETCH_STATUS = 0   
		    BEGIN  
			       IF @first = 0  
				    BEGIN
				        SET @myServiceQuery = @myServiceQuery + @myWhere + @myLike+  @myENDLike;
						SET @first =1 ;
					END
				   ELSE SET @myServiceQuery = @myServiceQuery + @myOR + @myLike + @myENDLike;
				   FETCH NEXT FROM myCursor INTO @myLike
		    END   
		    CLOSE myCursor   
		    DEALLOCATE myCursor
			SET @myServiceQuery =@myServiceQuery + ' )';
    
END