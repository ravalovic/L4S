using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using CommonHelper;
using System.Collections.Generic;


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace NetCollector
{
  class NetCollector
    {
       
        //Parameters in .config
         //transferMethod
         //remoteServer 
         //remoteDir
         //remoteFileMask
         //allowRenameRemote 
         //renameRemoteExtension 
         //shareName 
         //integratedSecurity 
         //login 
         //password 
         //domain 
         //drive 
         //outputDir 
         //workDir 
         //backupDir = ConfigurationManager.AppSettings["backupDir"];


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
       
        /// <summary>
        /// Initialization variable from App.config
        /// </summary>
        public static Dictionary<string, string> MySettings()
        {
            Dictionary<string, string> AppSettings = new Dictionary<string, string>();
            foreach (var stringkey in ConfigurationManager.AppSettings.AllKeys)
            {
                AppSettings[stringkey] = ConfigurationManager.AppSettings[stringkey];
            }
            return AppSettings;
        }

        static void Main()
        {
            //MySettings();
            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
                var appSettings = MySettings();

                bool allowRename;
                bool intSec;
                Boolean.TryParse(appSettings["allowRenameRemote"], out allowRename);
                Boolean.TryParse(appSettings["integratedSecurity"], out intSec);
                log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                CollectionMethod method;
                if (Enum.TryParse(appSettings["transferMethod"], out method))
                {
                    switch (method)
                    {
                        case CollectionMethod.NetShare:
                            if (intSec) //if user which execute has integration security with remote server
                            {
                                if (NetShareTransfer(appSettings["remoteServer"], appSettings["shareName"], appSettings["remoteDir"], appSettings["remoteFileMask"],
                                    appSettings["drive"], appSettings["workDir"], allowRename, appSettings["renameRemoteExtension"]))
                                {
                                    BackupNewFiles(appSettings["drive"], appSettings["workDir"], appSettings["backupDir"], appSettings["remoteFileMask"]);
                                }
                            }
                            else
                            {
                                using (UserImpersonation user = new UserImpersonation(appSettings["login"], appSettings["domain"], appSettings["password"]))
                                {
                                    if (user.ImpersonateValidUser())
                                    {
                                        if (NetShareTransfer(appSettings["remoteServer"], appSettings["shareName"], appSettings["remoteDir"], appSettings["remoteFileMask"],
                                            appSettings["drive"], appSettings["workDir"], allowRename, appSettings["renameRemoteExtension"]))
                                        {
                                            BackupNewFiles(appSettings["drive"], appSettings["workDir"], appSettings["backupDir"], appSettings["remoteFileMask"]);
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
                            if (FTPTransfer(appSettings["remoteServer"], appSettings["remoteDir"], appSettings["remoteFileMask"], appSettings["workDir"]))
                            {
                                BackupNewFiles(appSettings["drive"], appSettings["workDir"], appSettings["backupDir"], appSettings["remoteFileMask"]);
                            }
                            break;
                    }
                    MoveToFinal(appSettings["drive"], appSettings["workDir"], appSettings["outputDir"], appSettings["remoteFileMask"]);
                }
            } //using singleinstance
        }

        protected static bool NetShareTransfer(string remoteServer, string shareName, string remoteDir, string remoteFileMask, 
                                               string drive, string workDir, bool allow, string renameRemoteExtension)
        {
            bool result = false;
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
                        ManageFile(Action.Move, file, sourceDir, renameRemoteExtension);
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


        protected static bool FTPTransfer(string remServer, string remDir, string remFileMask, string wDir)
        {
            throw new NotImplementedException("Unsupported transfer");
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
