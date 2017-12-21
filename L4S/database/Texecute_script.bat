set  SQLRUN="c:\Program Files\Microsoft SQL Server\Client SDK\ODBC\110\Tools\Binn\SQLCMD.EXE"

set arg1=%1
%SQLRUN% -S "winbz.bluez.biz" -d log4service -U l4sowner -P "0wNer,123" -i %arg1%

rem for %%A in (.\??_create_index*.sql) do (%SQLRUN% -S "winbz.bluez.biz" -U l4sowner -P "0wNer,123" -i %%A)

pause
