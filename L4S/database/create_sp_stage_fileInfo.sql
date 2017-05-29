USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_stage_fileInfo]    Script Date: 30.05.2017 0:11:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_stage_fileInfo]
	-- Add the parameters for the stored procedure here
	@FileName nvarchar(50),
	@FileCheckSum nvarchar(50)
AS
BEGIN TRY
INSERT INTO [dbo].[Stage_InputFileInfo] ([fileName],[checksum])
     VALUES(@FileName, @FileCheckSum);
END TRY
BEGIN CATCH
INSERT INTO [dbo].[Stage_InputFileDuplicity] ([fileName],[checksum])
     VALUES(@FileName, @FileCheckSum);
END CATCH;
