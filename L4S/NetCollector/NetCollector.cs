using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace NetCollector
{
    class NetCollector
    {
        /// <summary>
        /// Initialization variable from App.config
        /// </summary>

        protected static string transferMethod = ConfigurationManager.AppSettings["transferMethod"];
        protected static string remoteServer = ConfigurationManager.AppSettings["remoteServer"];
        protected static string remoteDir = ConfigurationManager.AppSettings["remoteDir"];
        protected static string remoteFileMask = ConfigurationManager.AppSettings["remoteFileMask"];
        protected static string allowRenameRemote = ConfigurationManager.AppSettings["allowRenameRemote"];
        protected static string renameRemoteExtension = ConfigurationManager.AppSettings["renameRemoteExtension"];
        protected static string shareName = ConfigurationManager.AppSettings["shareName"];
        protected static string integratedSecurity =  ConfigurationManager.AppSettings["allowRenameRemote"];
        protected static string login = ConfigurationManager.AppSettings["login"];
        protected static string password = ConfigurationManager.AppSettings["password"];
        protected static string domain = ConfigurationManager.AppSettings["domain"];
        protected static string drive = ConfigurationManager.AppSettings["drive"];
        protected static string outputDir = ConfigurationManager.AppSettings["outputDir"];
        protected static string workDir = ConfigurationManager.AppSettings["workDir"];
        protected static string backupDir = ConfigurationManager.AppSettings["backupDir"];

        protected enum CollectionMethod
        {
            NetShare,
            FTP,
            HTTP,
            SSH
        };
        protected enum Action
        {
            Copy,
            Move,
            Delete,
            Zip
        };

        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main()
        {

            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
               bool allowRename;
               Boolean.TryParse(allowRenameRemote, out allowRename);
                log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                CollectionMethod method;
                if (Enum.TryParse(transferMethod, out method))
                {
                    switch (method)
                    {
                        case CollectionMethod.NetShare:
                            if (NetShareTransfer(remoteServer, remoteDir, remoteFileMask, workDir, allowRename, renameRemoteExtension))
                            { 
                                BackupNewFiles(drive, workDir, backupDir, remoteFileMask);
                            }
                            break;
                        case CollectionMethod.FTP:
                            if (FTPTransfer(remoteServer, remoteDir, remoteFileMask, workDir))
                            {
                                BackupNewFiles(drive, workDir, backupDir, remoteFileMask);
                            }
                            break;
                    }
                    MoveToFinal(drive, workDir, outputDir, remoteFileMask);
                }
               } //using singleinstance
        }

        protected static bool NetShareTransfer(string remServer, string remDir, string remFileMask, string wDir, bool allow, string renExt)
        {
            bool result = false;
            using (UserImpersonation user = new UserImpersonation(login, domain, password))
            {
                if (user.ImpersonateValidUser())
                {


                    //Read files from NetCollector
                    string sourceDir = string.Format(@"\\" + remServer + @"\" + shareName + remDir);
                    string[] iFiles = Directory.GetFiles(sourceDir, remFileMask);
                    if (iFiles.Any())
                    {
                        log.Info(@"Get new files from: " + sourceDir);
                        //Copy file from remote server to NetCollector Work directory
                        int i = 0;
                        foreach (var file in iFiles)
                        {
                            ManageFile(Action.Copy, file, drive + wDir, string.Empty);
                            log.Info(String.Format("New file {0} - {1} ", i, file));
                            if (allow)
                            {
                                ManageFile(Action.Move, file, sourceDir, renExt);
                                log.Info(String.Format("Renaming to file {0} - {1} ", i, file + renExt));
                            }
                            i++;
                        }
                        result = true;
                    }
                    else
                    {
                        log.Warn("No files for collection from source: " + sourceDir + remFileMask);
                    }

                }
                else
                {
                    log.Error("User is not connected check credential");
                }
                return result;
            }
        }

        protected static bool FTPTransfer(string remServer, string remDir, string remFileMask, string wDir)
        {
            throw new NotImplementedException("Unsupported transfer");
        }

        protected static void BackupNewFiles(string drv, string wDir, string backDir, string remFileMask)
        {
            //throw new NotImplementedException("NotImplemented yet");
            var iFiles = Directory.GetFiles(drv + wDir, remFileMask);
            if (iFiles.Any())
            {
                int i = 0;
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    ManageFile(Action.Zip, file, drv + backDir, string.Empty);
                    log.Info(String.Format("Backup file {0} - {1} ", i, file));
                    i++;
                }
            }
            else
            {
                log.Warn("No files for backup from: " + drv + backDir);
            }
        }
        protected static void MoveToFinal(string drv, string wDir, string outDir, string remFileMask)
        {
            //throw new NotImplementedException("NotImplemented yet");
            var iFiles = Directory.GetFiles(drv + wDir, remFileMask);
            if (iFiles.Any())
            {
                int i = 0;
                foreach (var file in iFiles)
                {
                    //Backup file from workDir to backup directory
                    ManageFile(Action.Move, file, drv + outDir, string.Empty);
                    log.Info(String.Format("Move file {0}: {1} to {2}", i, file, outDir));
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
            if ((File.Exists(file) && !File.Exists(destDir + Path.GetFileName(file)+destExt)) || action == Action.Delete)
            {
                switch (action)
                {
                    case Action.Move:
                        File.Move(file, destDir + Path.GetFileName(file)+destExt);
                        break;
                    case Action.Copy:
                        File.Copy(file, destDir + Path.GetFileName(file),true);
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
