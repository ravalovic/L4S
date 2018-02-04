# backup L4S enviroment (c) BlueZ, s.r.o 2018
# version: 1.0
#SET up variables
    $myRunDirectory = $PSScriptRoot;
	[xml]$config = Get-Content "$myRunDirectory\backup.config";
	$backupDirectory = $config.configuration.commonSettings.backupDirectory;
	[int]$DayOfRun = $config.configuration.commonSettings.dayOfRun;
	[int]$Days = $config.configuration.commonSettings.delOlderAs;
	$compress = $config.configuration.dbSettings.compressDBbackup;
	if($compress.ToUpper() -eq "YES") { $CompressDbBackup=$true } else { $CompressDbBackup=$false }
	$backL4S = $backupDirectory+"\application";
	$backDB = $backupDirectory+"\database";
	$PROD_L4SbinPath=$config.configuration.appSettings.PROD_L4SPath;
	$TEST_L4SbinPath=$config.configuration.appSettings.TEST_L4SPath;
	$PROD_WebApp=$config.configuration.appSettings.PROD_WebApp;
	$TEST_WebApp=$config.configuration.appSettings.TEST_WebApp;
	$DBServer=$config.configuration.dbSettings.databaseServer;
	$databasesArray = $config.GetElementsByTagName("database");
	#variable
	$date = Get-Date -Format yyyyMMddHHmmss;
	$dayOfWeek = [Int] (Get-Date).DayOfWeek;
	#Archive names 
	$PROD_L4SbinArchiveName="PROD_L4Sbin_archive_$($date).zip";
	$TEST_L4SbinArchiveName="TEST_L4Sbin_archive_$($date).zip";
	$PROD_VOSKarchiveName="PROD_VOSKweb_archive_$($date).zip";
	$TEST_VOSKarchiveName="TEST_VOSKweb_archive_$($date).zip";
	$DB_ArchiveNameBAK="DB_FULL_archive_$($date).zip";
	$DB_ArchiveNameINC="DB_INC_archive_$($date).zip";

#Region Functions
function CheckBackupDirectory(){
	# Test if backup dir Exist if not create
	if( -Not (Test-Path -Path $backL4S ) )
	{
	    New-Item -ItemType directory -Path $backL4S;
	} 
	
	if( -Not (Test-Path -Path $backDB ) )
	{
	    New-Item -ItemType directory -Path $backDB;
	} 
}

function BackupApplication(){
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
		$Extension = "*.zip"
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
}
function DatabaseFullBackup(){
	foreach ($db in $databasesArray){
      $dbname = $db.InnerXML;
	  Backup-SqlDatabase -ServerInstance $DBServer -Database $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).bak"
	}
	#----- get files from DB backup #	
	$Now = Get-Date
	$Extension = "*.[bz][ai][kp]"
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
}

function DatabaseIncBackup(){
 #differntial backup of application database
	foreach ($db in $databasesArray){
      $dbname = $db.InnerXML;
	  if ($dbname.ToUpper() -ne "MASTER" -and $dbname.ToUpper() -ne "MSDB" -and $dbname.ToUpper() -ne "MODEL") {
		 Backup-SqlDatabase -ServerInstance $DBServer -Incremental $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).inc"
	  }
	}
}

function CompressDbBackup(){
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
#end region

# ******* MAIN ******
CheckBackupDirectory
BackupApplication
# Full Backup Database # in saturday full backup #check if exist some backup
$files = Get-ChildItem -Path "$($backDB)\*.[bz][ai][kp]"
if (( $dayOfWeek -eq $DayOfRun ) -or ( $files.count -eq 0 )) {
	DatabaseFullBackup
} else {
	DatabaseIncBackup
}
if ($CompressDbBackup){	CompressDbBackup }		
Exit(0);