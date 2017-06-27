USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_STCheckFileDuplicity]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_CATGetServiceQuery]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].sp_CATGetServiceQuery
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_CATRunDataProcessing]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].sp_CATGetServiceQuery
	-- Add the parameters for the stored procedure here
	@myServiceID int = 0,
	@myBatchList varchar(max),
	@myServiceQuery varchar(max) ='' out
AS
DECLARE
       @myLike varchar(max),
	   @first int = 0,
	   @myWhere varchar(max) = ' RequestedURL like ''',
       @myOR varchar(max) = ' or RequestedURL like ''',
       @myENDLike varchar(max) = ''''
BEGIN
     
    SET NOCOUNT ON;
	--Get service parameters
		   SET @myServiceQuery = '
				INSERT INTO [dbo].[CATLogsOfService]([BatchID], [RecordID], [ServiceID], [UserID], [DateOfRequest], [RequestedURL], [RequestStatus], [BytesSent], [RequestTime], [UserIPAddress])
			    SELECT [BatchID], [RecordID], ' + cast(@myServiceID as varchar) + ', [UserID], dateadd(hour,convert(int,substring([DateOfRequest],len([DateOfRequest])-5,4)) ,convert(datetime, substring([DateOfRequest],0,12)+'' ''+ substring([DateOfRequest],13,8),104))
					   ,[RequestedURL], [RequestStatus], [BytesSent], [RequestTime], [UserIPAddress] FROM [dbo].[STLogImport] WHERE BatchID IN '+@myBatchList+' AND (';
			DECLARE myCursor CURSOR FOR SELECT PatternLike FROM [dbo].[CATServicePatterns] WHERE FKServiceID = @myServiceID;
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