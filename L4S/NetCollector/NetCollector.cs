using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using CommonHelper;
using Limilabs.FTP.Client;
// ReSharper disable LocalVariableHidesMember


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace NetCollector
{
    //Public class for read parameters from .config file
    public class MyApConfig
    {
       
        public string TransferMethod { get; set; }
        public string RemoteServer { get; set; }
        public string ShareName { get; set; }
        public string RemoteDir { get; set; }
        public string RemoteFileName { get; set; }
        public bool AllowRenameRemote { get; set; }
        public string RenameRemoteExtension { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string OutputDir { get; set; }
        public string WorkDir { get; set; }
        public string BackupDir { get; set; }
        public string DateMask { get; set; }

        public MyApConfig()
        {
            var configManager = new AppConfigManager();
            //remote params
            TransferMethod = configManager.ReadSetting("transferMethod");
            RemoteServer = configManager.ReadSetting("remoteServer");
            ShareName = configManager.ReadSetting("shareName");
            RemoteDir = configManager.ReadSetting("remoteDir");
            RemoteFileName = configManager.ReadSetting("remoteFileName");
            bool allowRenameRemote;
            bool.TryParse(configManager.ReadSetting("allowRenameRemote"), out allowRenameRemote);
            AllowRenameRemote = allowRenameRemote;
            RenameRemoteExtension = configManager.ReadSetting("renameRemoteExtension");
            // local autorization params
            bool integratedSecurity;
            bool.TryParse(configManager.ReadSetting("integratedSecurity"), out integratedSecurity);
            IntegratedSecurity = integratedSecurity;
            Login = configManager.ReadSetting("login");
            Password = configManager.ReadSetting("password");
            Domain = configManager.ReadSetting("domain");

            // local file system params
            OutputDir = configManager.ReadSetting("outputDir");
            WorkDir = configManager.ReadSetting("workDir");
            BackupDir = configManager.ReadSetting("backupDir");
            
            // set up datetime mask
            int whichDay;
            int.TryParse(configManager.ReadSetting("whichDay"), out whichDay);
            DateMask= DateTime.Now.AddDays(whichDay).ToString(configManager.ReadSetting("dateMask"));
         }
        public bool CheckParams(object myConfig, out string missingParams)
        {
            bool isInComplete = false;
            missingParams = string.Empty;
            string mp = string.Empty;
            foreach (var propertyInfo in myConfig.GetType().GetProperties())
            {
                string value = propertyInfo.GetValue(myConfig).ToString();
                if (String.IsNullOrEmpty(value))
                {
                    mp = mp + " " + propertyInfo.Name;
                    isInComplete = true;
                }
            }
            if (isInComplete)
            {
                missingParams = @"Missing paremeters in config file: " + mp;
            }
            return isInComplete;
        }

    }

    public static class StopWatchExtension
    {
        public static string RunTime(this Stopwatch sw)
        {
            if (sw.ElapsedMilliseconds > 1000)
                return sw.ElapsedMilliseconds / 1000 + " s";
            else
            {
                return sw.ElapsedMilliseconds + "ms";
            }
        }
    }

    class NetCollector
    {
        protected enum CollectionMethod
        {
            NetShare,
            Ftp,
            Http,
            Ssh
        }
       
        // Create a logger for use in this class
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initialization variable from App.config
        /// </summary>
        private static void CreateIfMissing(MyApConfig appApConfig)
        {
           Directory.CreateDirectory(appApConfig.BackupDir);
           Directory.CreateDirectory(appApConfig.WorkDir);
           Directory.CreateDirectory(appApConfig.OutputDir);
        }
        static void Main()
        {

            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
                var myStopWatch = Stopwatch.StartNew();
                string missing;
                MyApConfig appSettings = new MyApConfig();
                if (appSettings.CheckParams(appSettings, out missing))
                {
                    Log.Error(missing);
                    Environment.Exit(0);
                }
                CreateIfMissing(appSettings);

                Log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                Log.InfoFormat("Transfer method:  {0}", appSettings.TransferMethod);
                CollectionMethod method;
                if (Enum.TryParse(appSettings.TransferMethod, out method))
                {
                    switch (method)
                    {
                        case CollectionMethod.NetShare:
                            if (appSettings.IntegratedSecurity) //if user which execute has integration security with remote server
                            {
                                if (NetShareTransfer(appSettings))
                                {
                                    BackupNewFiles(appSettings);
                                    MoveToFinal(appSettings);
                                }
                            }
                            else
                            {
                                using (UserImpersonation user = new UserImpersonation(appSettings.Login, appSettings.Domain, appSettings.Password))
                                {
                                    if (user.ImpersonateValidUser())
                                    {
                                        if (NetShareTransfer(appSettings))
                                        {
                                            BackupNewFiles(appSettings);
                                            MoveToFinal(appSettings);
                                        }
                                    }
                                    else
                                    {
                                        Log.Error("User is not connected check credential");
                                    }
                                }
                            }
                            break;
                        case CollectionMethod.Ftp:
                            if (FtpTransfer(appSettings))
                            {
                                BackupNewFiles(appSettings);
                                MoveToFinal(appSettings);
                            }
                            break;
                    }

                }
                myStopWatch.Stop();
                Log.Info("Transferred in " + myStopWatch.RunTime());
            } //using singleinstance
        }
        /// <summary>
        /// NetShare transfer method
        /// </summary>
        /// <param name="settingsConfig"></param>
        /// <returns></returns>
        protected static bool NetShareTransfer(MyApConfig settingsConfig)
        {
            bool result = false;
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            //Read files from NetCollector
            string sourceDir = string.Format(@"\\" + settingsConfig.RemoteServer + @"\" + settingsConfig.ShareName + settingsConfig.RemoteDir);
            string[] iFiles = Directory.GetFiles(sourceDir, settingsConfig.RemoteFileName);
            if (iFiles.Any())
            {
                Log.Info(@"Get new files from: " + sourceDir);
                //Copy file from remote server to NetCollector Work directory
               
                foreach (var file in iFiles)
                {
                    Helper.ManageFile(Helper.Action.Copy, file, settingsConfig.WorkDir);
                    Log.Info(String.Format("New file {0}", file));
                    if (settingsConfig.AllowRenameRemote)
                    {
                        Helper.ManageFile(Helper.Action.Move, file, sourceDir, @"_" + dateMask + settingsConfig.RenameRemoteExtension);
                        Log.Info(String.Format("Renaming to file {0} ", file + settingsConfig.RenameRemoteExtension));
                    }
                  
                }
                result = true;
            }
            else
            {
                Log.Warn("No files for collection from source: " + sourceDir + settingsConfig.RemoteFileName);
            }
            return result;
        }

        /// <summary>
        /// FTP transfer method
        /// </summary>
        /// <param name="settingsConfig"></param>
        /// <returns></returns>
        protected static bool FtpTransfer(MyApConfig settingsConfig)
        {
            bool result = false;
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
           
            using (Ftp ftp = new Ftp())
            {
                ftp.Connect(settingsConfig.RemoteServer);  // or ConnectSSL for SSL 
                ftp.Login(settingsConfig.Login, settingsConfig.Password);
                var options = new RemoteSearchOptions();
                ftp.ChangeFolder(settingsConfig.RemoteDir);
                options.UseWildcardMatch(settingsConfig.RemoteDir, settingsConfig.RemoteFileName, true);
                var iFilesRemote = ftp.Search(options);
                if (iFilesRemote.Any())
                {
                    foreach (var file in iFilesRemote)
                    {
                        ftp.Download(settingsConfig.RemoteDir + file.FtpItem.Name, settingsConfig.WorkDir + file.FtpItem.Name);
                        if (settingsConfig.AllowRenameRemote)
                        {
                            ftp.Rename(settingsConfig.RemoteDir + file.FtpItem.Name, settingsConfig.RemoteDir + Path.GetFileName(file.FtpItem.Name) + @"_" + dateMask + settingsConfig.RenameRemoteExtension);
                        }
                    }
                    result = true;
                }
                else
                {
                    Log.Warn("No files for collection from source: " + settingsConfig.RemoteServer + settingsConfig.RemoteDir + settingsConfig.RemoteFileName);
                }
               ftp.Close(); 
            }
            
            return result;

        }
        /// <summary>
        /// Backup new file to Backup directory in zip format
        /// </summary>
        /// <param name="settingsConfig"></param>
        protected static void BackupNewFiles(MyApConfig settingsConfig)
        {
            //throw new NotImplementedException("NotImplemented yet");
            var iFiles = Directory.GetFiles(settingsConfig.WorkDir, settingsConfig.RemoteFileName);
            if (iFiles.Any())
            {
                
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    Helper.ManageFile(Helper.Action.Zip, file, settingsConfig.BackupDir);
                    Log.Info(String.Format("Backup file {0}", file));
                   
                }
            }
            else
            {
                Log.Warn("No files for backup from: " + settingsConfig.BackupDir);
            }
        }
        /// <summary>
        /// Moving transferred file from work to final directory
        /// </summary>
        /// <param name="settingsConfig"></param>
        protected static void MoveToFinal(MyApConfig settingsConfig)
        {
            
            var iFiles = Directory.GetFiles(settingsConfig.WorkDir, settingsConfig.RemoteFileName);
            if (iFiles.Any())
            {
               
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    Helper.ManageFile(Helper.Action.Move, file, settingsConfig.OutputDir);
                    Log.Info(String.Format("Move file {0} to {1}", file, settingsConfig.OutputDir));
                    
                }
            }
            else
            {
                Log.Warn("No files for finallly move  from: " + settingsConfig.WorkDir);
            }
        }
     
    }
}
