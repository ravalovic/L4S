USE [log4service]
go

SELECT *  FROM [dbo].[Stage_InputFileInfo];
select * from Stage_InputFileDuplicity;
select * from Stage_LogImport;

delete  FROM [dbo].[Stage_InputFileInfo];
delete from Stage_InputFileDuplicity;
delete from Stage_LogImport;
