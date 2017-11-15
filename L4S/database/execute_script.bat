@echo off
echo Subor na spustenie sql scriptu
echo Pouzitie: execute_script.bat meno%scriptu.sql
set  SQLRUN="c:\Program Files\Microsoft SQL Server\100\Tools\Binn\SQLCMD.EXE"

set arg1=%1
%SQLRUN% -S "vugkvosk" -i %arg1%

pause
