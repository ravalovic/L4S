SET  SRV=localdb
SET  DB=LocalDBL4s
set  SQLRUN="c:\Program Files (x86)\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\SQLCMD.EXE"
%SQLRUN% -S "(localdb)\LocalDBL4s" -Q [log4service].[dbo].[sp_CATLoadLogsOfServices]