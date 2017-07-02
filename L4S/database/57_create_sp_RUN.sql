USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_RUN]   Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_RUN]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_RUN]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_RUN]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_RUN] 
@mydebug int = 0
AS
DECLARE	
 @return_value int,
 @wasChange int,
 @startArchive int,
 @batchList varchar(max)

BEGIN
print 'Start Processing: '+ cast(getdate() as varchar);

EXEC	@return_value = [dbo].[sp_ClearData]
		@mydebug = @mydebug;

SELECT @wasChange = count(*) from dbo.CATChangeDetect where Status = 0;
IF (@wasChange > 0)
	BEGIN
	    EXEC	@return_value = [dbo].[sp_DataProcessing]
				@mydebug = @mydebug,
				@tableInput = 0;
		
		EXEC	@return_value = [dbo].[sp_DataProcessing]
				@mydebug = @mydebug,
				@tableInput = 1,
				@batchList = @batchList OUTPUT;
		

		UPDATE dbo.CATChangeDetect 
		 SET Status = 1
		 , TCLastUpdate = GETDATE()
		 WHERE Status = 0;
       
	END
ELSE
	BEGIN
	    EXEC	@return_value = [dbo].[sp_DataProcessing]
				@mydebug = @mydebug,
				@tableInput = 0,
				@batchList = @batchList OUTPUT;
	    UPDATE dbo.CATChangeDetect 
		 SET Status = 1
		 , TCLastUpdate = GETDATE()
		 WHERE Status = 0;
		
	END
if (@mydebug = 1 ) print 'Start Daily DataLoading: '+ cast(getdate() as varchar);

EXEC	@return_value = [dbo].[sp_DailyLoad]
        @mydebug = @mydebug,
		@myBatchList = @batchList;

if (@mydebug = 1 ) print 'Start Monthly DataLoading: '+ cast(getdate() as varchar);

EXEC	@return_value = [dbo].[sp_MonthlyLoad] 
        @mydebug = @mydebug,
		@myBatchList = @batchList;
	IF ( day(getdate()) = 3)
		BEGIN
		print 'Start Archiving: '+ cast(getdate() as varchar);
		EXEC	@return_value = [dbo].[sp_ArchiveOldData] 
		END
if (@mydebug = 1 ) print 'Stop: '+cast(getdate() as varchar);
END



GO
