SET  SRV=localdb
SET  DB=LocalDBL4s
set  SQLRUN="c:\Program Files (x86)\Microsoft SQL Server\Client SDK\ODBC\130\Tools\Binn\SQLCMD.EXE"

.\NetCollector\bin\Debug\NetCollector.exe
.\PreProcessor\bin\Debug\PreProcessor.exe
.\SQLBulkCopy\bin\Debug\SQLBulkCopy.exe
rem %SQLRUN% -S "(localdb)\LocalDBL4s" -Q [log4service].[dbo].[sp_RUN]
%SQLRUN% -S "neon.webglobe.sk,42287" -U log4service -P Idqu36y5 -Q [log4service].[dbo].[sp_RUN]
pause