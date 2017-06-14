USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_stage_fileInfo]    Script Date: 14. 6. 2017 9:26:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_stage_fileInfo]
	-- Add the parameters for the stored procedure here
	@FileName varchar(100),
	@FileCheckSum varchar(50),
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

 INSERT INTO [dbo].[Stage_InputFileInfo] ([FileName],[Checksum], [LoaderBatchID], [LinesInFile], [LoadedRecord])
     VALUES(@FileName, @FileCheckSum, @BatchID, @LinesInFile,@loadedRecord);
ELSE
SELECT @oriID = ID, @oriFN = fileName, @oriINDT = insertDateTime 
 from [Stage_InputFileInfo] t where t.checksum = @FileCheckSum;

INSERT INTO [dbo].[Stage_InputFileDuplicity] ([Id], [FileName],[LinesInFile],[Checksum], [LoadDateTime], [OriFilename],[LoaderBatchID])
     VALUES(@oriID, @FileName, @LinesInFile, @FileCheckSum, @oriINDT, @oriFN, @BatchID);

END;

