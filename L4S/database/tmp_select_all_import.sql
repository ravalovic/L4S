USE [log4service]
go

SELECT *  FROM [dbo].[STInputFileInfo];
select * from STInputFileDuplicity;
select top 20 * from STLogImport;
select count(*) from  STLogImport;

--delete  FROM [dbo].[STInputFileInfo];
--delete from  STInputFileDuplicity;
--delete from STLogImport;

