Rem **** Delete downloaded files form L4S ****
del C:\NGinx\nginx-1.9.5\logs\*.dwn
del C:\NGinx\nginx-1.9.5\logs\access_07.log
del C:\NGinx\nginx-1.9.5\logs\error_07.log
del C:\NGinx\nginx-1.9.5\logs\host.access_07.log

rename C:\NGinx\nginx-1.9.5\logs\access_06.log access_07.log
rename C:\NGinx\nginx-1.9.5\logs\error_06.log error_07.log
rename C:\NGinx\nginx-1.9.5\logs\host.access_06.log host.access_07.log

rename C:\NGinx\nginx-1.9.5\logs\access_05.log access_06.log
rename C:\NGinx\nginx-1.9.5\logs\error_05.log error_06.log
rename C:\NGinx\nginx-1.9.5\logs\host.access_05.log host.access_06.log

rename C:\NGinx\nginx-1.9.5\logs\access_04.log access_05.log
rename C:\NGinx\nginx-1.9.5\logs\error_04.log error_05.log
rename C:\NGinx\nginx-1.9.5\logs\host.access_04.log host.access_05.log

rename C:\NGinx\nginx-1.9.5\logs\access_03.log access_04.log
rename C:\NGinx\nginx-1.9.5\logs\error_03.log error_04.log
rename C:\NGinx\nginx-1.9.5\logs\host.access_03.log host.access_04.log

rename C:\NGinx\nginx-1.9.5\logs\access_02.log access_03.log
rename C:\NGinx\nginx-1.9.5\logs\error_02.log error_03.log
rename C:\NGinx\nginx-1.9.5\logs\host.access_02.log host.access_03.log

rename C:\NGinx\nginx-1.9.5\logs\access_01.log access_02.log
rename C:\NGinx\nginx-1.9.5\logs\error_01.log error_02.log
rename C:\NGinx\nginx-1.9.5\logs\host.access_01.log host.access_02.log

rename C:\NGinx\nginx-1.9.5\logs\access.log access_01.log
rename C:\NGinx\nginx-1.9.5\logs\error.log error_01.log
rename C:\NGinx\nginx-1.9.5\logs\host.access.log host.access_01.log
cd C:\NGinx\nginx-1.9.5
nginx -s reload
rem **** Copy of host.access.log for L4S ****
REM **** User must right to powershell ****

for /f "usebackq" %%x in (`powershell "Get-Date -format yyyyMMddHHmmss"`) do set dtm=%%x 
copy "C:\NGinx\nginx-1.9.5\logs\host.access_01.log" "C:\NGinx\nginx-1.9.5\logs\host.access_%dtm%.log"
