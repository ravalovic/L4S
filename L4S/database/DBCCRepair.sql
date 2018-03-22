USE master;
GO
ALTER DATABASE log4service
SET SINGLE_USER
WITH ROLLBACK IMMEDIATE;
GO
DBCC checkdb (log4service, repair_rebuild)
go
ALTER DATABASE log4service
SET MULTI_USER;
GO