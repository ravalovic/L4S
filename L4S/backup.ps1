# backup L4S enviroment (c) BlueZ, s.r.o 2018
# version: 1.0
#SET up variables
    #script directory 
    $myRunDirectory = $PSScriptRoot;
    $myConfigFile = $myRunDirectory+"\backup.config"
    #Parameters form backup.config - xml file
	if( -Not (Test-Path -Path $myConfigFile ) )
	{
	    write-host "Config file  $myConfigFile missing. Create it!" -ForegroundColor "DarkRed";
		exit 1;
	} 
	[xml]$config = Get-Content "$myRunDirectory\backup.config";

    #commonSettings Section
	$backupDirectory = $config.configuration.commonSettings.backupDirectory;
    $backL4S = $backupDirectory+"\application";
	$backDB = $backupDirectory+"\database";
	[int]$DayOfRun = $config.configuration.commonSettings.dayOfRun;
	[int]$Days = $config.configuration.commonSettings.delOlderAs;
	$compress = $config.configuration.dbSettings.compressDBbackup;
	if($compress.ToUpper() -eq "YES") { $CompressDbBackup=$true } else { $CompressDbBackup=$false }

    #dbSettings section 
    $dbSettingsSection = $config.GetElementsByTagName("dbSettings");
    $databasesArray = $dbSettingsSection.GetElementsByTagName("database");
	$DBServer=$config.configuration.dbSettings.databaseServer;

    #appSettings section 
    $appSettings = $config.GetElementsByTagName("appSettings");
    $applicationValues = $appSettings.GetElementsByTagName("add");
    
	#variable
	$date = Get-Date -Format yyyyMMddHHmmss;
	$dayOfWeek = [Int] (Get-Date).DayOfWeek;
	
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
	foreach ($app in $applicationValues)
	{
         $appName = $app.Key +"_archive_$($date).zip";
         $appPath = $app.Value;
         $archiveName="$($backL4S)\$($appName)";
         if( Test-Path -Path $appPath ) 
	      {
            Compress-Archive -Path $appPath -DestinationPath $archiveName;
          }
	} 
}
function DatabaseFullBackup(){
	foreach ($db in $databasesArray){
      $dbname = $db.InnerXML;
	  Backup-SqlDatabase -ServerInstance $DBServer -Database $dbname -BackupFile "$($backDB)\$($dbname)_db_$($date).bak"
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
    $DB_ArchiveNameBAK="DB_FULL_archive_$($date).zip";
	$DB_ArchiveNameINC="DB_INC_archive_$($date).zip";
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
function DeleteOldFiles([string]$delPath){
    #----- get files from DB backup #	
	$Now = Get-Date;
	$Extension = "*.[bz][ai][kp]";
	$LastWrite = $Now.AddDays(-$Days)
	$Files = Get-Childitem $delPath -Include $Extension -Recurse | Where {$_.LastWriteTime -le "$LastWrite"}
	foreach ($File in $Files) 
		{
		if ($File -ne $NULL)
			{
			write-host "Deleting File $File" -ForegroundColor "DarkRed";
			Remove-Item $File.FullName | out-null;
			}
		else
			{
			Write-Host "No more files to delete!" -foregroundcolor "Green";
			}
		}
}
#end region

# ******* MAIN ******
# Full Backup Database # in saturday full backup #check if exist some backup
	
    CheckBackupDirectory;
	$filesDB = Get-ChildItem -Path "$($backDB)\*.[bz][ai][kp]";
	$filesAPP = Get-ChildItem -Path "$($backL4S)\*.zip";
	
	if (( $dayOfWeek -eq $DayOfRun ) -or (( $filesAPP.count -eq 0 ) -and ( $filesDB.count -eq 0 ))) {
		BackupApplication;
		DatabaseFullBackup;
		if ($CompressDbBackup){	CompressDbBackup }
		DeleteOldFiles $backL4S;
		DeleteOldFiles $backDB;
	} else {
		DatabaseIncBackup;
	}
		
    exit 0;