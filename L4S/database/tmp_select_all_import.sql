USE [log4service]
go

SELECT *  FROM [dbo].[STInputFileInfo] order by id;
select * from STInputFileDuplicity;
select top 20 * from STLogImport;
select count(*) from  STLogImport;
select count(*), batchid from stlogimport group by batchid order by batchid;

/*
delete  FROM [dbo].[STInputFileInfo];
delete from  STInputFileDuplicity;
truncate table STLogImport;
delete from STLogImport;
*/
