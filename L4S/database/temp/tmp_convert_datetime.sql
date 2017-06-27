--select distinct substring(l.Requested_URL,0,20) from Stage_LogImport l 
--where l.Requested_URL like '%GET /odata%' or l.Requested_URL like '%GET /EsknBo%' or l.Requested_URL like '%GET /eskn/rest/%' 
--order by 1;

select top 100 * from STLogImport l;
select top 100  substring(l.DateOfRequest,0,12)+' '+substring(l.DateOfRequest,13,8)  SDatetime, convert(int,substring(l.DateOfRequest,len(l.DateOfRequest)-5,4)) addhour  from   STLogImport l
select top 100 dateadd(hour,convert(int,substring(l.DateOfRequest,len(l.DateOfRequest)-5,4)) ,convert(datetime, substring(l.DateOfRequest,0,12)+' '+substring(l.DateOfRequest,13,8),104))  DDatetime from   STLogImport l;