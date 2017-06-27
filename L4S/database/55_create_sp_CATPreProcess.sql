USE [log4service]
GO

/****** Object:  StoredProcedure [dbo].[sp_CATPreProcess]   Script Date: 14. 6. 2017 12:40:24 ******/
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'[dbo].[sp_CATPreProcess]') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE [dbo].[sp_CATPreProcess]
END

GO

USE [log4service]
GO
/****** Object:  StoredProcedure [dbo].[sp_CATPreProcess]  Script Date: 24.06.2017 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_CATPreProcess] 
AS

BEGIN
     DELETE FROM STLogImport
	  WHERE  
	      BytesSent = 0
       or RequestStatus = 404
	   or RequestedURL like '%content%' 
	   or RequestedURL like '%/scripts/%' 
	   or RequestedURL like 'GET /Portal HTTP%' 
	   or RequestedURL like 'GET /GisPortal HTTP%'
END