# backup L4S enviroment (c) BlueZ, s.r.o 2018
# version: 1.0
$DBServer = "winbz";
# L4S PATH
$backL4S = "C:\backup\application";
$backDB = "C:\backup\database";
$PROD_L4SbinPath="c:\L4S\bin\";
$TEST_L4SbinPath="c:\L4Stest\bin\";
$PROD_WebApp="c:\inetpub\wwwroot\vosk";
$TEST_WebApp="c:\inetpub\wwwroot\vosk_test"


#variable
$date = Get-Date -Format yyyyMMddHHmmss
$CompressDbBackup = $true;
$DayOfRun = 6;
$Days = 1;
$dayOfWeek = [Int] (Get-Date).DayOfWeek;

#Archive names 
$PROD_L4SbinArchiveName="PROD_L4Sbin_archive_$($date).zip";
$TEST_L4SbinArchiveName="TEST_L4Sbin_archive_$($date).zip";
$PROD_VOSKarchiveName="PROD_VOSKweb_archive_$($date).zip";
$TEST_VOSKarchiveName="TEST_VOSKweb_archive_$($date).zip";
$DB_ArchiveNameBAK="DB_FULL_archive_$($date).zip";
$DB_ArchiveNameINC="DB_INC_archive_$($date).zip";



# ******* MAIN ******
# Test if backup dir Exist if not create
if( -Not (Test-Path -Path $backL4S ) )
{
    New-Item -ItemType directory -Path $backL4S;
} 

if( -Not (Test-Path -Path $backDB ) )
{
    New-Item -ItemType directory -Path $backDB;
} 

# Backup Application
$files = Get-ChildItem -Path "$($backL4S)\*.zip"
if (( $dayOfWeek -eq $DayOfRun ) -or ( $files.count -eq 0 )){
		# Prod
		$archiveName="$($backL4S)\$($PROD_L4SbinArchiveName)";
		Compress-Archive -Path $PROD_L4SbinPath -DestinationPath $archiveName;
		$archiveName="$($backL4S)\$($PROD_VOSKarchiveName)";
		Compress-Archive -Path $PROD_WebApp -DestinationPath $archiveName;
		#Test
		$archiveName="$($backL4S)\$($TEST_L4SbinArchiveName)";
		Compress-Archive -Path $TEST_L4SbinPath -DestinationPath $archiveName;
		$archiveName="$($backL4S)\$($TEST_VOSKarchiveName)";
		Compress-Archive -Path $TEST_WebApp -DestinationPath $archiveName;
		
		#delete old backups 
		$Now = Get-Date
		$Extension = "*.*"
		#----- define LastWriteTime parameter based on $Days ---#
		$LastWrite = $Now.AddDays(-$Days)
		#----- get files from App backup #
		$Files = Get-Childitem $backL4S -Include $Extension -Recurse | Where {$_.LastWriteTime -le "$LastWrite"}
		foreach ($File in $Files) 
			{
			if ($File -ne $NULL)
				{
				write-host "Deleting File $File" -ForegroundColor "DarkRed"
				Remove-Item $File.FullName | out-null
				}
			else
				{
				Write-Host "No more files to delete!" -foregroundcolor "Green"
				}
			}
}
# Full Backup Database # in saturday full backup #check if exist some backup

$files = Get-ChildItem -Path "$($backDB)\*.[bz][ai][kp]"
if (( $dayOfWeek -eq $DayOfRun ) -or ( $files.count -eq 0 )) {
	#system db
	$dbname = 'master'
	Backup-SqlDatabase -ServerInstance $DBServer -Database $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).bak"
	$dbname = 'model'
	Backup-SqlDatabase -ServerInstance $DBServer  -Database $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).bak"
	$dbname = 'msdb'
	Backup-SqlDatabase -ServerInstance $DBServer  -Database $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).bak"
	#applicationdb
	$dbname = 'log4service'
	Backup-SqlDatabase -ServerInstance $DBServer  -Database $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).bak"
	$dbname = 'log4test'
	Backup-SqlDatabase -ServerInstance $DBServer  -Database $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).bak"
	
	#----- get files from DB backup #	
	$Now = Get-Date
	$Extension = "*.*"
	$LastWrite = $Now.AddDays(-$Days)
	$Files = Get-Childitem $backDB -Include $Extension -Recurse | Where {$_.LastWriteTime -le "$LastWrite"}
	foreach ($File in $Files) 
		{
		if ($File -ne $NULL)
			{
			write-host "Deleting File $File" -ForegroundColor "DarkRed"
			Remove-Item $File.FullName | out-null
			}
		else
			{
			Write-Host "No more files to delete!" -foregroundcolor "Green"
			}
		}
} else {
	#differntial backup of application database
	$dbname = 'log4service'
	Backup-SqlDatabase -ServerInstance $DBServer  -Incremental $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).inc"
	$dbname = 'log4test'
	Backup-SqlDatabase -ServerInstance $DBServer  -Incremental $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).inc"
}

if ($CompressDbBackup){
	#Create zip from db backup 
	$archiveName="$($backDB)\$($DB_ArchiveNameBAK)";
	$backDBfile="$($backDB)\*.bak";
	$files = Get-ChildItem -Path $backDBfile;
	if ( $files.count -gt 0 ) {Compress-Archive -Path $backDBfile -DestinationPath $archiveName;}
	
	$archiveName="$($backDB)\$($DB_ArchiveNameINC)";
	$backDBfile="$($backDB)\*.inc";
	$files = Get-ChildItem -Path $backDBfile;
	if ( $files.count -gt 0 ) {Compress-Archive -Path $backDBfile -DestinationPath $archiveName;}

	$backDBfile="$($backDB)\*.[bi][an][kc]";
	$files = Get-ChildItem -Path $backDBfile;
	
	foreach ($file in $files) 
			{
				write-host "Deleting File $file" -ForegroundColor "DarkRed"
				Remove-Item $file.FullName | out-null
			}
}		
Exit(0);