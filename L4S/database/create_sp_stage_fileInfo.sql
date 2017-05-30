USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_stage_fileInfo]    Script Date: 30.05.2017 21:50:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_stage_fileInfo]
	-- Add the parameters for the stored procedure here
	@FileName varchar(100),
	@FileCheckSum varchar(50),
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
INSERT INTO [dbo].[Stage_InputFileInfo] ([fileName],[checksum])
     VALUES(@FileName, @FileCheckSum);
	 SET @RetVal = 0;
	  return @RetVal;
END TRY
BEGIN CATCH

SELECT @oriID = ID, @oriFN = fileName, @oriINDT = insertDateTime 
 from [Stage_InputFileInfo] t where t.checksum = @FileCheckSum;

INSERT INTO [dbo].[Stage_InputFileDuplicity] ([id], [fileName],[checksum], [loadDateTime], [oriFilename])
     VALUES(@oriID, @FileName, @FileCheckSum, @oriINDT, @oriFN);
	 SET @RetVal = -1;
	 return @RetVal;
END CATCH;

