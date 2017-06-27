select count(*) from STLogImport;
select count(*) from CATLogsOfService;

select s.batchid, s.recordid, count(distinct s.recordid) from STLogImport s 
left join CATLogsOfService c on (s.batchid = c.batchid)
group by s.batchid, s.recordid;

select s.* from STLogImport s 
left join CATLogsOfService c on (s.batchid = c.batchid);



