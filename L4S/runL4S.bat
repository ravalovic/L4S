rem ******* L4S starting script (c) 2017 BlueZ, s.r.o *******
set  SQLRUN="c:\Program Files\Microsoft SQL Server\100\Tools\Binn\SQLCMD.EXE"
cd c:\L4S\bin\
.\NetCollector\NetCollector.exe
.\PreProcessor\PreProcessor.exe
.\SQLBulkCopy\SQLBulkCopy.exe
rem *** Timestamp for log file ***
for /f "tokens=2-8 delims=.:/ " %%a in ("%date% %time: =0%") do set DateStamp=%%c-%%b-%%a

for /f "tokens=2-8 delims=.:/ " %%a in ("%date% %time: =0%") do set DateNtime=%%c-%%a-%%b %%d:%%e:%%f.%%g
@echo %DateNtime% ************* DB Processing Start ************* >> ..\logs\sp_run_%DateStamp%.log

%SQLRUN% -S "vugkvosk" -Q "EXEC [log4service].[dbo].[sp_RUN] $(mydebug)" -v mydebug = 1 >> ..\logs\sp_run_%DateStamp%.log

for /f "tokens=2-8 delims=.:/ " %%a in ("%date% %time: =0%") do set DateNtime=%%c-%%a-%%b %%d:%%e:%%f.%%g
@echo %DateNtime% ************* DB Processing Stop ************* >> ..\logs\sp_run_%DateStamp%.log

rem *************   DELETE FILE OLDER THAN minage in days *********************
if not exist "c:\L4S\fordelete\" mkdir c:\L4S\fordelete\
robocopy c:\l4s\logs c:\l4s\fordelete /mov /minage:45 >> ..\logs\delete_%DateStamp%.log
rem **** Sorce dowloaded data ****
rem robocopy c:\L4S\data\NetCollector\Backup\ c:\l4s\fordelete /mov /minage:400 >> ..\logs\delete_%DateStamp%.log

rem **** Processed data should be delete ****
robocopy c:\L4S\data\PreProcessor\Wrong\ c:\l4s\fordelete /mov /minage:60 >> ..\logs\delete_%DateStamp%.log
robocopy c:\L4S\data\SQLBulkCopy\Processed\ c:\l4s\fordelete /mov /minage:60 >> ..\logs\delete_%DateStamp%.log
del c:\L4S\fordelete\*.* /q 