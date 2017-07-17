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
@mydebug int = 0

AS
DECLARE
@rowcount int
BEGIN
DELETE FROM STLogImport
WHERE
      requeststatus = '400' 
  or requeststatus = '401'
  or requeststatus = '403'
  or requeststatus = '404'
  or requeststatus = '405'
  or requeststatus = '406'
  or bytessent ='0'
  or [RequestedURL] like'%/[Tt]hemes/%'
  or [RequestedURL] like'%/[Ss]cripts/%'
  or [RequestedURL] like'%/[Ii]mages/%'
  or [RequestedURL] like'%GET /gisportal/api/Configuration HTTP/1.1%'
  or [RequestedURL] like'%POST /eskn/services HTTP/1.1%'
  or [RequestedURL] like'%GET /eskn/admin/publicKey?f=json HTTP/1.1%'
  or [RequestedURL] like'%GET /eskn/rest/info?f=json HTTP/1.1%'
  or [RequestedURL] like'%POST /eskn/services/NR/kn_wmts_norm_wm/MapServer HTTP/1.1%'
  or [RequestedURL] like'%POST /eskn/services/NR/kn_wmts_norm_sjtsk/MapServer HTTP/1.1%'
  or [RequestedURL] like'%POST /eskn/services/NR/kn_wmts_norm_sjtsk/MapServer HTTP/1.1%'
  or [RequestedURL] like'%POST /eskn/services/NR/kn_wmts_norm_sjtsk/MapServer HTTP/1.1%'
  or [RequestedURL] like'%POST /eskn/services/NR/kn_wms_norm/MapServer HTTP/1.1%'
  or [RequestedURL] like'%GET /eskn/rest/services/NR HTTP/1.1%'
  or [RequestedURL] like'%GET /eskn/rest/services/NR/kn_wmts_orto_wm/MapServer/layers?f=json HTTP/1.1%'
  or [RequestedURL] like'%GET /eskn/rest/services/NR/kn_wmts_orto_wm/MapServer?f=json HTTP/1.1%' 
  or [RequestedURL] like'%GET /eskn/rest/services/NR/kn_wmts_norm_wm/MapServer?f=json HTTP/1.1%'
  or [RequestedURL] like'%GET /eskn/rest/services/NR/kn_wmts_norm_wm/MapServer/layers?f=json HTTP/1.1%'
  or [RequestedURL] like '%/VRM/%'

  SELECT @rowcount = @@ROWCOUNT;
  if (@mydebug = 1 ) print 'Clean ' + cast(@rowcount as varchar) +' lines ';
 -- create date and numeric field 
	 UPDATE STLogImport 
	  SET  BytesSent = replace(replace(BytesSent,'.',''),',','')
	      ,DatDate = dateadd(hour,convert(int,substring([DateOfRequest],len([DateOfRequest])-5,4)) ,convert(datetime, substring([DateOfRequest],0,12)+' '+ substring([DateOfRequest],13,8),104));
	  
END