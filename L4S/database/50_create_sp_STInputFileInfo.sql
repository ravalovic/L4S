USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_STInputFileInfo]    Script Date: 24.06.2017 23:29:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[sp_STInputFileInfo]
	-- Add the parameters for the stored procedure here
	@FileName varchar(100),
	@FileCheckSum varchar(50),
	@OriginalFilename varchar(50),
	@OriginalFileCheckSum varchar(50),
	@BatchID int,
	@LinesInFile int,
	@Action int
AS
declare 
@oriID int,
@oriINDT datetime,
@loadedRecord int
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT @loadedRecord = count(*) FROM dbo.STLogImport WHERE BatchID = @BatchID;
IF (@Action = 0)

 INSERT INTO [dbo].[STInputFileInfo] ([FileName],[Checksum],[OriFilename],[OriginalFileChecksum], [LoaderBatchID], [LinesInFile], [LoadedRecord])
     VALUES(@FileName, @FileCheckSum, @OriginalFilename, @OriginalFileChecksum, @BatchID, @LinesInFile,@loadedRecord);
ELSE
Begin
  SELECT @oriID = ID,  @oriINDT = insertDateTime 
  FROM [STInputFileInfo] t where t.checksum = @FileCheckSum or t.OriginalFileChecksum = @OriginalFileCheckSum ;
  IF (@oriID is null) SET @oriID = 0; 
  INSERT INTO [dbo].[STInputFileDuplicity] ([OriginalId], [FileName],[LinesInFile],[Checksum], [LoadDateTime], [OriFilename],[OriginalFileChecksum],[LoaderBatchID])
     VALUES(@oriID, @FileName, @LinesInFile, @FileCheckSum, @oriINDT, @OriginalFilename,@OriginalFileChecksum, @BatchID);
  INSERT INTO [dbo].[CATProcessStatus]([BatchID], [BatchRecordNum], [ActualStepName], [ActualStepID], [ActualStepStatus])
     VALUES (@BatchID, @loadedRecord, 'SQLBulkLoader' ,1,1);
end;
END;

