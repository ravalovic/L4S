select s.* from STLogImport s , CATLogsOfService c
where s.BatchID = c.BatchID
and s.recordID not in (select l.recordID from CATLogsOfService l where s.batchID = l.batchID)