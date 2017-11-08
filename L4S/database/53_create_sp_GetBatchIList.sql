USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetBatchList]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_GetBatchList]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].sp_GetBatchList
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetBatchList]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].sp_GetBatchList 
	-- Add the parameters for the stored procedure here
	@myInput int = 0,
	@batchLimit int = 4,
	@myBatchList varchar(max) ='' out
AS
DECLARE
       @myBatch varchar(max),
	   @first int = 0
BEGIN
     
    SET NOCOUNT ON;

	--Read batch from STLogImport
	        SET @myBatchList ='( ';
	        --list of batch from STLogImport
		    IF (@myInput = 0) DECLARE myCursor CURSOR FOR SELECT DISTINCT BatchID FROM [dbo].[STLogImport];
			IF (@myInput = 1) DECLARE myCursor CURSOR FOR SELECT DISTINCT BatchID FROM [dbo].[CATUnknownService] WHERE CustomerID is null;
			IF (@myInput = 2) DECLARE myCursor CURSOR FOR SELECT DISTINCT BatchID FROM [dbo].[CATLogsOfService] WHERE CustomerID is null;
		    OPEN myCursor
		    FETCH NEXT FROM myCursor INTO @myBatch
		    WHILE @@FETCH_STATUS = 0   
		    BEGIN  
			       IF @first = 0  
				    BEGIN
				        SET @myBatchList = @myBatchList +  @myBatch;
						SET @first =1 ;
					END
				   ELSE SET @myBatchList = @myBatchList + ',' + @myBatch;
				   FETCH NEXT FROM myCursor INTO @myBatch
		    END   
		    CLOSE myCursor   
		    DEALLOCATE myCursor
			IF (LEN(@myBatchList)=1) -- if not new batch set default  
			BEGIN
			 SET @myBatchList = @myBatchList + '0';
			END
			SET @myBatchList = @myBatchList + ' )';
END