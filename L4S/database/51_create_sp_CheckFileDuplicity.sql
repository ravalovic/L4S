USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_CheckFileDuplicity]    Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_CheckFileDuplicity]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_CheckFileDuplicity]
END

GO

/****** Object:  StoredProcedure [dbo].[sp_CheckFileDuplicity]    Script Date: 14. 6. 2017 12:40:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CheckFileDuplicity]
	-- Add the parameters for the stored procedure here
	@FileCheckSum varchar(50),
	@OriFileCheckSum varchar(50),
	@RetVal int output
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET NOCOUNT ON;

SELECT @RetVal = count(*) FROM dbo.STInputFileInfo WHERE OriginalFileChecksum = @OriFileCheckSum or Checksum = @FileCheckSum and TCActive <> 99;

return @RetVal;
END;

GO

