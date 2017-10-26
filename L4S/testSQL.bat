rem Test set  SQLRUN="c:\Program Files\Microsoft SQL Server\Client SDK\ODBC\110\Tools\Binn\SQLCMD.EXE"
set  SQLRUN="c:\Program Files\Microsoft SQL Server\100\Tools\Binn\SQLCMD.EXE"
%SQLRUN% -S "vugkvosk" -Q "select @@version "
