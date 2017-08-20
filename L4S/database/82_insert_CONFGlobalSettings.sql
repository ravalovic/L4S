use log4service;
USE [log4service]
GO
DBCC CHECKIDENT ('CONFGeneralSettings', RESEED,0);
delete from [CONFGeneralSettings];

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue]
		   ,[Note])
     VALUES
           ('MetricUnit'
           ,'MByte',
		   'Merná jednotka účtovania');
GO

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('ArchivingDay'
           ,'5', 'Deň v mesiaci kedy sa spúšťa archivačný proces');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('LastArchiveRUN'
           ,'0', 'Deň posledného behu archivačného procesu');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('ArchiveDetailDataMonth'
           ,'-1', 'Archivácia detailných logov ak -1 záznamy sa mažú, inak archivuje staršie ako zadaný počet dní');
GO

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('ArchiveCumulativeDataMonth'
           ,'12', 'Archivácia denných a mesačných kumulatívov pre výpočet faktúr ak -1 záznamy sa mažú, inak archivuje staršie ako zadaný počet mesiacov');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('DPH'
           ,'0.2', 'Hodnota DPH v aktuálnom účtovnom období ako desatinné číslo');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('InvoiceCreationDay'
           ,'3', 'Deň v mesiaci kedy sa vykoná výpočet fakturačných dát');
GO

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('LastInvoiceGenerate'
           ,'0', 'Deň posledného behu fakturačného procesu');
GO

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('DueDateDays'
           ,'15', 'Počet dní pre splatnosť faktúry od dátumu generovania faktúr');
GO

INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('GAPAnalyzeDays'
           ,'15', 'Počet dní v ktorom je sledované prijatie vstupných záznamov');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('UnknownServiceStoreDays'
           ,'60', 'Počet dní počas ktorých sa uchovávajú záznamy neidentifkovaných služieb');
GO
INSERT INTO [dbo].[CONFGeneralSettings]
           ([ParamName]
           ,[ParamValue],[Note])
     VALUES
           ('ProcessDataStoreDays'
           ,'60', 'Počet dní počas ktorých sa uchovávajú logy spracovania.');
GO