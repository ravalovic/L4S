USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_ClearData]   Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_ClearData]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_ClearData]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_ClearData]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ClearData] 
 @deleted int  out
AS
BEGIN
     DELETE FROM STLogImport
	  WHERE  
	      BytesSent = 0
       or RequestStatus = 404
	   or RequestedURL like '%content%' 
	   or RequestedURL like '%/scripts/%' 
	   or RequestedURL like 'GET /Portal HTTP%' 
	   or RequestedURL like 'GET /GisPortal HTTP%';
	   SET @deleted = @@ROWCOUNT;
	 UPDATE STLogImport 
	  SET  BytesSent = replace(replace(BytesSent,'.',''),',','')
	      ,DatDate = dateadd(hour,convert(int,substring([DateOfRequest],len([DateOfRequest])-5,4)) ,convert(datetime, substring([DateOfRequest],0,12)+' '+ substring([DateOfRequest],13,8),104));
END