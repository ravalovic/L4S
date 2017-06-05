USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_stage_fileInfo]    Script Date: 5. 6. 2017 14:48:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_stage_fileInfo]
	-- Add the parameters for the stored procedure here
	@FileName varchar(100),
	@FileCheckSum varchar(50),
	@BatchID int,
	@RetVal int output
AS
declare 
@oriID int,
@oriFN varchar(100),
@oriINDT datetime
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;
INSERT INTO [dbo].[Stage_InputFileInfo] ([fileName],[checksum], [loaderBatchID])
     VALUES(@FileName, @FileCheckSum, @BatchID);
	 SET @RetVal = 0;
	  return @RetVal;
END TRY
BEGIN CATCH

SELECT @oriID = ID, @oriFN = fileName, @oriINDT = insertDateTime 
 from [Stage_InputFileInfo] t where t.checksum = @FileCheckSum;

INSERT INTO [dbo].[Stage_InputFileDuplicity] ([id], [fileName],[checksum], [loadDateTime], [oriFilename],[loaderBatchID])
     VALUES(@oriID, @FileName, @FileCheckSum, @oriINDT, @oriFN, @BatchID);
	 SET @RetVal = -1;
	 return @RetVal;
END CATCH;

