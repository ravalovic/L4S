USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_stage_CheckFileDuplicity]    Script Date: 14. 6. 2017 12:40:24 ******/
DROP PROCEDURE [dbo].[sp_stage_CheckFileDuplicity]
GO

/****** Object:  StoredProcedure [dbo].[sp_stage_CheckFileDuplicity]    Script Date: 14. 6. 2017 12:40:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_stage_CheckFileDuplicity]
	-- Add the parameters for the stored procedure here
	@FileCheckSum varchar(50),
	@OriFileCheckSum varchar(50),
	@RetVal int output
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT @RetVal = count(*) FROM dbo.Stage_InputFileInfo WHERE OriginalFileChecksum = @OriFileCheckSum or Checksum = @FileCheckSum;

return @RetVal;
END;

GO

USE [log4service]
GO
grant execute on sp_stage_InputFileInfo to loader;

GO
