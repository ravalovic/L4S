USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_WriteInputFileInfo]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_WriteInputFileInfo]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_WriteInputFileInfo]
END

GO

/****** Object:  StoredProcedure [dbo].[sp_WriteInputFileInfo]    Script Date: 14. 6. 2017 12:40:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_WriteInputFileInfo]
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
	 BEGIN
	 INSERT INTO [dbo].[STInputFileInfo] ([FileName],[Checksum],[OriFilename],[OriginalFileChecksum], [LoaderBatchID], [LinesInFile], [LoadedRecord])
	     VALUES(@FileName, @FileCheckSum, @OriginalFilename, @OriginalFileChecksum, @BatchID, @LinesInFile,@loadedRecord);
	 insert into [dbo].CATProcessStatus ([StepName], [BatchID], [BatchRecordNum])
     values ('SQLLoader', @BatchID, @loadedRecord);
	 END
	ELSE
	BEGIN
	  SELECT @oriID = ID,  @oriINDT = insertDateTime 
	  FROM [STInputFileInfo] t WHERE t.checksum = @FileCheckSum or t.OriginalFileChecksum = @OriginalFileCheckSum and TCActive <> 99 ; 
	  IF (@oriID is null) SET @oriID = 0; 
	  INSERT INTO [dbo].[STInputFileDuplicity] ([OriginalId], [FileName],[LinesInFile],[Checksum], [LoadDateTime], [OriFilename],[OriginalFileChecksum],[LoaderBatchID])
	     VALUES(@oriID, @FileName, @LinesInFile, @FileCheckSum, @oriINDT, @OriginalFilename,@OriginalFileChecksum, @BatchID);
	END;
END;

