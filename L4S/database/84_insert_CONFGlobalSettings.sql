use log4service;
USE [log4service]
GO
DBCC CHECKIDENT ('CONFGeneralSettings', RESEED,0);
delete from [CONFGeneralSettings];

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('MetricUnit'
           ,'MByte');
GO

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('ArchiveDetailDataMonth'
           ,'6');
GO

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('ArchiveCumulativeDataMonth'
           ,'12');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('DPH'
           ,'0.2');
GO