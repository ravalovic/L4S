using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
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
    public class MyAPConfig
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string TransferMethod { get; set; }
        public string RemoteServer { get; set; }
        public string ShareName { get; set; }
        public string RemoteDir { get; set; }
        public string RemoteFileMask { get; set; }
        public bool AllowRenameRemote { get; set; }
        public string RenameRemoteExtension { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
        public string Drive { get; set; }
        public string OutputDir { get; set; }
        public string WorkDir { get; set; }
        public string BackupDir { get; set; }

        public MyAPConfig()
        {
            var configManager = new AppConfigManager();
            TransferMethod = configManager.ReadSetting("transferMethod");
            RemoteServer = configManager.ReadSetting("remoteServer");
            ShareName = configManager.ReadSetting("shareName");
            RemoteDir = configManager.ReadSetting("remoteDir");
            RemoteFileMask = configManager.ReadSetting("remoteFileMask");
            bool allowRename;
            bool.TryParse(configManager.ReadSetting("allowRenameRemote"), out allowRename);
            AllowRenameRemote = allowRename;
            RenameRemoteExtension = configManager.ReadSetting("renameRemoteExtension");
            bool integraSecurity;
            bool.TryParse(configManager.ReadSetting("integratedSecurity"), out integraSecurity);
            IntegratedSecurity = integraSecurity;
            Login = configManager.ReadSetting("login");
            Password = configManager.ReadSetting("password");
            Domain = configManager.ReadSetting("domain");
            Drive = configManager.ReadSetting("drive");
            OutputDir = configManager.ReadSetting("outputDir");
            WorkDir = configManager.ReadSetting("workDir");
            BackupDir = configManager.ReadSetting("backupDir");

        }

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
                MyAPConfig appSettings = new MyAPConfig();

                log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                log.InfoFormat("Transfer method:  {0}", appSettings.TransferMethod);
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
                                        log.Error("User is not connected check credential");
                                    }
                                }
                            }
                            break;
                        case CollectionMethod.FTP:
                            if (FTPTransfer(appSettings))
                            {
                                BackupNewFiles(appSettings);
                                MoveToFinal(appSettings);
                            }
                            break;
                    }

                }
            } //using singleinstance
        }
        /// <summary>
        /// NetShare transfer method
        /// </summary>
        /// <param name="settingsConfig"></param>
        /// <returns></returns>
        protected static bool NetShareTransfer(MyAPConfig settingsConfig)
        {
            bool result = false;
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            //Read files from NetCollector
            string sourceDir = string.Format(@"\\" + settingsConfig.RemoteServer + @"\" + settingsConfig.ShareName + settingsConfig.RemoteDir);
            string[] iFiles = Directory.GetFiles(sourceDir, settingsConfig.RemoteFileMask);
            if (iFiles.Any())
            {
                log.Info(@"Get new files from: " + sourceDir);
                //Copy file from remote server to NetCollector Work directory
                int i = 0;
                foreach (var file in iFiles)
                {
                    ManageFile(Action.Copy, file, settingsConfig.Drive + settingsConfig.WorkDir, string.Empty);
                    log.Info(String.Format("New file {0} - {1} ", i, file));
                    if (settingsConfig.AllowRenameRemote)
                    {
                        ManageFile(Action.Move, file, sourceDir, @"_" + dateMask + settingsConfig.RenameRemoteExtension);
                        log.Info(String.Format("Renaming to file {0} - {1} ", i, file + settingsConfig.RenameRemoteExtension));
                    }
                    i++;
                }
                result = true;
            }
            else
            {
                log.Warn("No files for collection from source: " + sourceDir + settingsConfig.RemoteFileMask);
            }
            return result;
        }

        /// <summary>
        /// FTP transfer method
        /// </summary>
        /// <param name="settingsConfig"></param>
        /// <returns></returns>
        protected static bool FTPTransfer(MyAPConfig settingsConfig)
        {
            bool result = false;
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");

            using (Ftp ftp = new Ftp())
            {
                ftp.Connect(settingsConfig.RemoteServer);  // or ConnectSSL for SSL 
                ftp.Login(settingsConfig.Login, settingsConfig.Password);
                var options = new RemoteSearchOptions();
                ftp.ChangeFolder(settingsConfig.RemoteDir);
                options.UseWildcardMatch(settingsConfig.RemoteDir, settingsConfig.RemoteFileMask, true);
                var iFilesRemote = ftp.Search(options);
                if (iFilesRemote.Any())
                {
                    foreach (var file in iFilesRemote)
                    {
                        ftp.Download(settingsConfig.RemoteDir + file.FtpItem.Name, settingsConfig.Drive + settingsConfig.WorkDir + file.FtpItem.Name);
                        if (settingsConfig.AllowRenameRemote)
                        {
                            ftp.Rename(settingsConfig.RemoteDir + file.FtpItem.Name, settingsConfig.RemoteDir + Path.GetFileName(file.FtpItem.Name) + @"_" + dateMask + settingsConfig.RenameRemoteExtension);
                        }
                    }
                    result = true;
                }
                else
                {
                    log.Warn("No files for collection from source: " + settingsConfig.RemoteServer + settingsConfig.RemoteDir + settingsConfig.RemoteFileMask);
                }
                ftp.Close();

            }
            return result;

        }
        /// <summary>
        /// Backup new file to Backup directory in zip format
        /// </summary>
        /// <param name="settingsConfig"></param>
        protected static void BackupNewFiles(MyAPConfig settingsConfig)
        {
            //throw new NotImplementedException("NotImplemented yet");
            var iFiles = Directory.GetFiles(settingsConfig.Drive + settingsConfig.WorkDir, settingsConfig.RemoteFileMask);
            if (iFiles.Any())
            {
                int i = 0;
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    ManageFile(Action.Zip, file, settingsConfig.Drive + settingsConfig.BackupDir, string.Empty);
                    log.Info(String.Format("Backup file {0} - {1} ", i, file));
                    i++;
                }
            }
            else
            {
                log.Warn("No files for backup from: " + settingsConfig.Drive + settingsConfig.BackupDir);
            }
        }
        /// <summary>
        /// Moving transferred file from work to final directory
        /// </summary>
        /// <param name="settingsConfig"></param>
        protected static void MoveToFinal(MyAPConfig settingsConfig)
        {
            //throw new NotImplementedException("NotImplemented yet");
            var iFiles = Directory.GetFiles(settingsConfig.Drive + settingsConfig.WorkDir, settingsConfig.RemoteFileMask);
            if (iFiles.Any())
            {
                int i = 0;
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    ManageFile(Action.Move, file, settingsConfig.Drive + settingsConfig.OutputDir, string.Empty);
                    log.Info(String.Format("Move file {0}: {1} to {2}", i, file, settingsConfig.OutputDir));
                    i++;
                }
            }
            else
            {
                log.Warn("No files for finallly move  from: " + settingsConfig.Drive + settingsConfig.WorkDir);
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
