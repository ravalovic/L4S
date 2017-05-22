using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Principal;
using SingleInstance;

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
        protected static string user = ConfigurationManager.AppSettings["user"];
        protected static string passwd = ConfigurationManager.AppSettings["passwd"];
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
            Delete
        };

        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {

            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
                log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                CollectionMethod method;
                if (Enum.TryParse(transferMethod, out method)) { 
                switch (method) {
                    case CollectionMethod.NetShare:
                        NetShareTransfer(remoteServer, remoteDir, remoteFileMask);
                        break;
                    case CollectionMethod.FTP:
                        FTPTransfer(remoteServer, remoteDir, remoteFileMask);
                        break;
                }
                    
                    BackupNewFiles(workDir, backupDir);
                    MoveToFinal(workDir, outputDir);
                }
                Console.ReadKey();
            } //using singleinstance
         }

        protected static void NetShareTransfer(string remoteServer, string remoteDir, string remoteFileMask)
        {

        }

        protected static void FTPTransfer(string remoteServer, string remoteDir, string remoteFileMask)
        {
            throw new NotImplementedException("Unsupported transfer");
        }

        protected static void BackupNewFiles(string workDir, string backupDir)
        {
            throw new NotImplementedException("NotImplemented yet");
        }
        protected static void MoveToFinal(string workDir, string outputDir)
        {
            throw new NotImplementedException("NotImplemented yet");
        }

        /// <summary>
        /// Move, copy file to specified dir or delete file
        /// </summary>
        /// <param name="iFiles"></param>
        /// <param name="destDir"></param>
        protected static void ManageFile(Action action, string file, string destDir)
        {
            if ((File.Exists(file) && !File.Exists(destDir + Path.GetFileName(file))) || action == Action.Delete)
            {
                switch (action)
                {
                    case Action.Move:
                        File.Move(file, destDir + Path.GetFileName(file));
                        break;
                    case Action.Copy:
                        File.Copy(file, destDir + Path.GetFileName(file));
                        break;
                    case Action.Delete:
                        File.Delete(file);
                        break;
                }

            }
        }
    }
}
