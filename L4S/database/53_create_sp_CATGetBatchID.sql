USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_STCheckFileDuplicity]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_CATGetBatchID]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].sp_CATGetBatchID
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_CATRunDataProcessing]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].sp_CATGetBatchID 
	-- Add the parameters for the stored procedure here
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
		    DECLARE myCursor CURSOR FOR SELECT DISTINCT BatchID FROM [dbo].STLogImport;
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
			SET @myBatchList =@myBatchList + ' )';
END