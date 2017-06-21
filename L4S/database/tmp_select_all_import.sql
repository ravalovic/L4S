USE [log4service]
go

SELECT *  FROM [dbo].[STInputFileInfo];
select * from STInputFileDuplicity;
select top 20 * from STLogImport;
select count(*) from  STLogImport;
select count(*), batchid from stlogimport group by batchid;

--delete  FROM [dbo].[STInputFileInfo];
--delete from  STInputFileDuplicity;
--delete from STLogImport;

