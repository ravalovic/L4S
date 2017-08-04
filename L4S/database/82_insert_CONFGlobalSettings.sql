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
           ('ArchivingDay'
           ,'5');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('LastArchiveRUN'
           ,'0');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('ArchiveDetailDataMonth'
           ,'-1');
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
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('InvoiceCreationDay'
           ,'3');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('DueDateDays'
           ,'15');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('LastInvoiceGenerate'
           ,'0');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('GAPAnalyzeDays'
           ,'15');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue])
     VALUES
           ('UnknownServiceStoreDays'
           ,'60');
GO