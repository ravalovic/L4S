using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using CommonHelper;
using System.Collections.Generic;
using Limilabs.FTP.Client;


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace NetCollector
{
    //Public class for read parameters from .config file
    public class MyAPConfig
    { 
        public const string TransferMethod = "transferMethod";
        public const string RemoteServer = "remoteServer";
        public const string ShareName = "shareName";
        public const string RemoteDir = "remoteDir";
        public const string RemoteFileMask = "remoteFileMask";
        public const string AllowRenameRemote = "allowRenameRemote";
        public const string RenameRemoteExtension = "renameRemoteExtension";
        public const string IntegratedSecurity = "integratedSecurity";
        public const string Login = "login";
        public const string Password = "password";
        public const string Domain = "domain";
        public const string Drive = "drive";
        public const string OutputDir = "outputDir";
        public const string WorkDir = "workDir";
        public const string BackupDir= "backupDir";

        public MyAPConfig()
        {
            appParams = new Dictionary<string, string>();
            foreach (var stringkey in ConfigurationManager.AppSettings.AllKeys)
            {
                appParams[stringkey] = ConfigurationManager.AppSettings[stringkey];
            }
        }
        public Dictionary<string, string> appParams { get; set; }
    }
    class NetCollector
    {
        protected enum CollectionMethod
        {
            NetShare,
            FTP,
            HTTP,
            SSH
        }
        protected enum Action
        {
            Copy,
            Move,
            Delete,
            Zip
        }

        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Initialization variable from App.config
        /// </summary>

        static void Main()
        {
            
            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
                var appSettings = new MyAPConfig().appParams;

                bool allowRename;
                bool intSec;
                Boolean.TryParse(appSettings[MyAPConfig.AllowRenameRemote], out allowRename);
                Boolean.TryParse(appSettings[MyAPConfig.IntegratedSecurity], out intSec);
                log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                log.InfoFormat("Transfer method:  {0}", appSettings[MyAPConfig.TransferMethod]);
                CollectionMethod method;
                if (Enum.TryParse(appSettings[MyAPConfig.TransferMethod], out method))
                {
                    switch (method)
                    {
                        case CollectionMethod.NetShare:
                            if (intSec) //if user which execute has integration security with remote server
                            {
                                if (NetShareTransfer(appSettings[MyAPConfig.RemoteServer], appSettings[MyAPConfig.ShareName], appSettings[MyAPConfig.RemoteDir], appSettings[MyAPConfig.RemoteFileMask],
                                    appSettings[MyAPConfig.Drive], appSettings[MyAPConfig.WorkDir], allowRename, appSettings[MyAPConfig.RenameRemoteExtension]))
                                {
                                    BackupNewFiles(appSettings[MyAPConfig.Drive], appSettings[MyAPConfig.WorkDir], appSettings[MyAPConfig.BackupDir], appSettings[MyAPConfig.RemoteFileMask]);
                                }
                            }
                            else
                            {
                                using (UserImpersonation user = new UserImpersonation(appSettings[MyAPConfig.Login], appSettings[MyAPConfig.Domain], appSettings[MyAPConfig.Password]))
                                {
                                    if (user.ImpersonateValidUser())
                                    {
                                        if (NetShareTransfer(appSettings[MyAPConfig.RemoteServer], appSettings[MyAPConfig.ShareName], appSettings[MyAPConfig.RemoteDir], appSettings[MyAPConfig.RemoteFileMask],
                                            appSettings[MyAPConfig.Drive], appSettings[MyAPConfig.WorkDir], allowRename, appSettings[MyAPConfig.RenameRemoteExtension]))
                                        {
                                            BackupNewFiles(appSettings[MyAPConfig.Drive], appSettings[MyAPConfig.WorkDir], appSettings[MyAPConfig.BackupDir], appSettings[MyAPConfig.RemoteFileMask]);
                                        }
                                    }
                                    else
                                    {
                                        log.Error("User is not connected check credential");
                                    }
                                }
                            }
                            break;
                        case CollectionMethod.FTP:
                            if (FTPTransfer(appSettings[MyAPConfig.RemoteServer], appSettings[MyAPConfig.RemoteDir], appSettings[MyAPConfig.Login], appSettings[MyAPConfig.Password], appSettings[MyAPConfig.RemoteFileMask], appSettings[MyAPConfig.Drive], appSettings[MyAPConfig.WorkDir], allowRename, appSettings[MyAPConfig.RenameRemoteExtension]))
                            {
                                BackupNewFiles(appSettings[MyAPConfig.Drive], appSettings[MyAPConfig.WorkDir], appSettings[MyAPConfig.BackupDir], appSettings[MyAPConfig.RemoteFileMask]);
                            }
                            break;
                    }
                    MoveToFinal(appSettings[MyAPConfig.Drive], appSettings[MyAPConfig.WorkDir], appSettings[MyAPConfig.OutputDir], appSettings[MyAPConfig.RemoteFileMask]);
                }
            } //using singleinstance
        }

        protected static bool NetShareTransfer(string remoteServer, string shareName, string remoteDir, string remoteFileMask, 
                                               string drive, string workDir, bool allow, string renameRemoteExtension)
        {
            bool result = false;
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            //Read files from NetCollector
            string sourceDir = string.Format(@"\\" + remoteServer + @"\" + shareName + remoteDir);
            string[] iFiles = Directory.GetFiles(sourceDir, remoteFileMask);
            if (iFiles.Any())
            {
                log.Info(@"Get new files from: " + sourceDir);
                //Copy file from remote server to NetCollector Work directory
                int i = 0;
                foreach (var file in iFiles)
                {
                    ManageFile(Action.Copy, file, drive + workDir, string.Empty);
                    log.Info(String.Format("New file {0} - {1} ", i, file));
                    if (allow)
                    {
                        ManageFile(Action.Move, file, sourceDir, @"_" + dateMask+renameRemoteExtension);
                        log.Info(String.Format("Renaming to file {0} - {1} ", i, file + renameRemoteExtension));
                    }
                    i++;
                }
                result = true;
            }
            else
            {
                log.Warn("No files for collection from source: " + sourceDir + remoteFileMask);
            }



            return result;
        }


        protected static bool FTPTransfer(string remoteServer,  string remoteDir, string user, string password, string remoteFileMask,
                                           string drive, string workDir, bool allow, string renameRemoteExtension)
        {
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            using (Ftp ftp = new Ftp())
            {
                ftp.Connect(remoteServer);  // or ConnectSSL for SSL 
                ftp.Login(user, password);
                //Directory.CreateDirectory(@"c:\reports");
                ftp.DownloadFiles(remoteDir, drive+workDir);
                    new RemoteSearchOptions(remoteFileMask, true);
                if (allow)
                {
                    string[] iFiles = Directory.GetFiles(drive + workDir, remoteFileMask);
                    foreach (var file in iFiles)
                    {
                        ftp.Rename(remoteDir + Path.GetFileName(file), remoteDir + Path.GetFileName(file)+@"_"+ dateMask + renameRemoteExtension);
                    }

                }
                ftp.Close();
           }
            return true;

        }

        protected static void BackupNewFiles(string drive, string workDir, string backupDir, string remoteFileMask)
        {
            //throw new NotImplementedException("NotImplemented yet");
            var iFiles = Directory.GetFiles(drive + workDir, remoteFileMask);
            if (iFiles.Any())
            {
                int i = 0;
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    ManageFile(Action.Zip, file, drive + backupDir, string.Empty);
                    log.Info(String.Format("Backup file {0} - {1} ", i, file));
                    i++;
                }
            }
            else
            {
                log.Warn("No files for backup from: " + drive + backupDir);
            }
        }
        protected static void MoveToFinal(string drive, string workDir, string outputDir, string remoteFileMask)
        {
            //throw new NotImplementedException("NotImplemented yet");
            var iFiles = Directory.GetFiles(drive + workDir, remoteFileMask);
            if (iFiles.Any())
            {
                int i = 0;
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    ManageFile(Action.Move, file, drive + outputDir, string.Empty);
                    log.Info(String.Format("Move file {0}: {1} to {2}", i, file, outputDir));
                    i++;
                }
            }
            else
            {
                log.Warn("No files for finallly move  from: " + drive + workDir);
            }
        }

        /// <summary>
        /// Move, copy, delete, zip  file to specified dir
        /// </summary>
        /// <param name="action"></param>
        /// <param name="file"></param>
        /// <param name="destDir"></param>
        /// /// <param name="destExt"></param>
        protected static void ManageFile(Action action, string file, string destDir, string destExt)
        {
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            // if destination exist no action performed
            if ((File.Exists(file) && !File.Exists(destDir + Path.GetFileName(file) + destExt)) || action == Action.Delete)
            {
                switch (action)
                {
                    case Action.Move:
                        File.Move(file, destDir + Path.GetFileName(file) + destExt);
                        break;
                    case Action.Copy:
                        File.Copy(file, destDir + Path.GetFileName(file), true);
                        break;
                    case Action.Delete:
                        File.Delete(file);
                        break;
                    case Action.Zip:
                        var zipName = destDir + Path.GetFileName(file) + "_" + dateMask + ".zip";
                        using (ZipArchive arch = ZipFile.Open(zipName, ZipArchiveMode.Create))
                        {
                            arch.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                        }
                        break;
                }
            }
        }
    }
}
