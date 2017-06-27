USE [log4service]
go

SELECT *  FROM [dbo].[STInputFileInfo] order by id;
select * from STInputFileDuplicity;
select *  from CATProcessStatus
--select top 20 * from STLogImport;
select count(*) logimport from  STLogImport;
select top 20 * from STLogImport;
--select count(*), batchid from stlogimport group by batchid order by batchid;
select count(*) logservice from CATLogsOfService;
--select  count(*), batchid from CATLogsOfService group by batchid order by batchid;
select count(*)unknown  from CATUnknownService;
--select  count(*), batchid from CATUnknownService group by batchid order by batchid;

/*
delete  FROM [dbo].[STInputFileInfo];
delete from  STInputFileDuplicity;
truncate table STLogImport;
delete from STLogImport;
truncate table CATUnknownService;
delete from CATUnknownService;
truncate table CATLogsOfService;
delete from CATLogsOfService;
delete from CATProcessStatus;
*/
