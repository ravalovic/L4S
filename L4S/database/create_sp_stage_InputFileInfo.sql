USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_stage_InputFileInfo]    Script Date: 14. 6. 2017 12:39:27 ******/
DROP PROCEDURE [dbo].[sp_stage_InputFileInfo]
GO

/****** Object:  StoredProcedure [dbo].[sp_stage_InputFileInfo]    Script Date: 14. 6. 2017 12:39:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_stage_InputFileInfo]
	-- Add the parameters for the stored procedure here
	@FileName varchar(100),
	@FileCheckSum varchar(50),
	@OriginalFileCheckSum varchar(50),
	@BatchID int,
	@LinesInFile int,
	@Action int
AS
declare 
@oriID int,
@oriFN varchar(100),
@oriINDT datetime,
@loadedRecord int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT @loadedRecord = count(*) FROM dbo.Stage_LogImport WHERE BatchID = @BatchID;
IF (@Action = 0)

 INSERT INTO [dbo].[Stage_InputFileInfo] ([FileName],[Checksum],[OriginalFileChecksum], [LoaderBatchID], [LinesInFile], [LoadedRecord])
     VALUES(@FileName, @FileCheckSum, @OriginalFileChecksum, @BatchID, @LinesInFile,@loadedRecord);
ELSE
Begin
  SELECT @oriID = ID, @oriFN = fileName, @oriINDT = insertDateTime 
  FROM [Stage_InputFileInfo] t where t.checksum = @FileCheckSum or t.OriginalFileChecksum = @OriginalFileCheckSum ;
  IF (@oriID is null) SET @oriID = 0; 
  INSERT INTO [dbo].[Stage_InputFileDuplicity] ([OriginalId], [FileName],[LinesInFile],[Checksum], [LoadDateTime], [OriFilename],[OriginalFileChecksum],[LoaderBatchID])
  VALUES(@oriID, @FileName, @LinesInFile, @FileCheckSum, @oriINDT, @oriFN,@OriginalFileChecksum, @BatchID);
end;
END;

GO

 