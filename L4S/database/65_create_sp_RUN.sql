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
@mydebug int = 0,
@batchLimit int = 4
AS
DECLARE	
 @return_value int,
 @wasChange int,
 @startArchive int,
 @batchList varchar(max)

BEGIN
		print 'Start Processing: '+ cast(getdate() as varchar);

		print 'Start [sp_ClearData]: '+ cast(getdate() as varchar);
		EXEC	@return_value = [dbo].[sp_ClearData]
				@mydebug = @mydebug;

		print 'Start [sp_DataProcessing]: '+ cast(getdate() as varchar);
		SELECT @wasChange = count(*) from dbo.CATChangeDetect where Status = 0;
		IF (@wasChange > 0)
			BEGIN
			    EXEC	@return_value = [dbo].[sp_DataProcessing]
						@mydebug = @mydebug,
						@tableInput = 0,
						@batchLimit = @batchLimit,
						@batchList = @batchList OUTPUT;
				
				EXEC	@return_value = [dbo].[sp_DataProcessing]
						@mydebug = @mydebug,
						@tableInput = 1,
						@batchLimit = @batchLimit,
						@batchList = @batchList OUTPUT;
						
				EXEC	@return_value = [dbo].[sp_DataProcessing]
						@mydebug = @mydebug,
						@tableInput = 2,
						@batchLimit = @batchLimit,
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
						@batchLimit = @batchLimit,
						@batchList = @batchList OUTPUT;
			    UPDATE dbo.CATChangeDetect 
				 SET Status = 1
				 , TCLastUpdate = GETDATE()
				 WHERE Status = 0;
			END

		if (@mydebug = 1 ) print 'Start [sp_DailyLoad]: '+ cast(getdate() as varchar);
		EXEC	@return_value = [dbo].[sp_DailyLoad]
		        @mydebug = @mydebug,
				@myBatchList = @batchList;
		
		if (@mydebug = 1 ) print 'Start [sp_MonthlyLoad]: '+ cast(getdate() as varchar);
		EXEC	@return_value = [dbo].[sp_MonthlyLoad] 
		        @mydebug = @mydebug,
				@myBatchList = @batchList;
		
		if (@mydebug = 1 ) print 'Start [sp_CreateInvoice]: '+ cast(getdate() as varchar);
		EXEC	@return_value = [dbo].[sp_CreateInvoice] 
		        @mydebug = @mydebug;
		
		if (@mydebug = 1 ) print 'Start [sp_ArchiveOldData]: '+ cast(getdate() as varchar);
		EXEC	@return_value = [dbo].[sp_ArchiveOldData] 
				@mydebug = @mydebug;
		if (@mydebug = 1 ) print 'Stop: '+cast(getdate() as varchar);

		if (@mydebug = 1 ) print 'Start [sp_GAPAnalyze]: '+ cast(getdate() as varchar);
		EXEC	@return_value = [dbo].[sp_GAPAnalyze] 
				@mydebug = @mydebug;
		if (@mydebug = 1 ) print 'Stop: '+cast(getdate() as varchar);
END

GO
