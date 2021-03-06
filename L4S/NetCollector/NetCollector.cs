﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using CommonHelper;
using Limilabs.FTP.Client;
using System.Configuration;
using System.Collections.Generic;
// ReSharper disable LocalVariableHidesMember


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace NetCollector
{
    //Public class for read parameters from .config file
    public class MyApConfig
    {
        string _node;
        public string RemoteNode { get; set; }
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
        public bool SingleFileMode { get; set; }
        public bool UnzipInputFile { get; set; }

        public MyApConfig(string node)
        {
            _node = node;
            RemoteNode = _node;
            string cs = ConfigurationManager.ConnectionStrings[node].ConnectionString;
            Dictionary<string, string> connStringParts = cs.Split(';')
                        .Select(t => t.Split(new char[] { '=' }, 2))
                        .ToDictionary(t => t[0].Trim().ToUpper(), t => t[1].Trim(), StringComparer.InvariantCultureIgnoreCase);
            var configManager = new AppConfigManager();
            //remote params
            TransferMethod = ConfigurationManager.ConnectionStrings[node].ProviderName;
            if (connStringParts.ContainsKey("remoteServer".ToUpper())){ RemoteServer = connStringParts["remoteServer".ToUpper()]; }
            if (connStringParts.ContainsKey("shareName".ToUpper())){ ShareName = connStringParts["shareName".ToUpper()];}
            if (connStringParts.ContainsKey("remoteServer".ToUpper())){ RemoteDir = connStringParts["remoteDir".ToUpper()];}
            if (connStringParts.ContainsKey("remoteDir".ToUpper())){ RemoteFileName = connStringParts["remoteFileName".ToUpper()];}
            if (connStringParts.ContainsKey("allowRenameRemote".ToUpper())) {bool.TryParse(connStringParts["allowRenameRemote".ToUpper()], out var allowRenameRemote);
                                                                                AllowRenameRemote = allowRenameRemote;}
            if (connStringParts.ContainsKey("renameRemoteExtension".ToUpper())) {RenameRemoteExtension = connStringParts["renameRemoteExtension".ToUpper()];}
            // local autorization params
            if (connStringParts.ContainsKey("integratedSecurity".ToUpper())){ bool.TryParse(connStringParts["integratedSecurity".ToUpper()], out var integratedSecurity);
                                                                                IntegratedSecurity = integratedSecurity;}
            if (connStringParts.ContainsKey("login".ToUpper())){ Login = connStringParts["login".ToUpper()];}
            if (connStringParts.ContainsKey("password".ToUpper())){ Password = connStringParts["password".ToUpper()];}
            if (connStringParts.ContainsKey("domain".ToUpper())){ Domain = connStringParts["domain".ToUpper()];}
            // set up datetime mask
            int whichDay = 0;
            if (connStringParts.ContainsKey("whichDay".ToUpper())){ int.TryParse(connStringParts["whichDay".ToUpper()], out whichDay);}
            if (connStringParts.ContainsKey("dateMask".ToUpper()))
            {
                DateMask = DateTime.Now.AddDays(whichDay).ToString(connStringParts["dateMask".ToUpper()]);
            }
            else
            {
                DateMask = DateTime.Now.ToString("yyyyMMdd");
            }
            // local file system params
            OutputDir = configManager.ReadSetting("outputDir");
            WorkDir = configManager.ReadSetting("workDir");
            BackupDir = configManager.ReadSetting("backupDir");
            bool.TryParse(configManager.ReadSetting("singleFileMode"), out var oneFile);
            SingleFileMode = oneFile;
            
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

        /// <summary>
        /// NetShare transfer method
        /// </summary>
        /// <param name="settingsConfig"></param>
        /// <returns></returns>
        protected static bool NetShareTransfer(MyApConfig settingsConfig)
        {
            var myStopWatch = Stopwatch.StartNew();
            bool result = false;
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            //Read files from NetCollector
            string sourceDir = string.Format(@"\\" + settingsConfig.RemoteServer + @"\" + settingsConfig.ShareName + settingsConfig.RemoteDir);
            string[] iFiles = Directory.GetFiles(sourceDir, settingsConfig.RemoteFileName);
            var toTake = iFiles.Length;
            if (settingsConfig.SingleFileMode) { toTake = 1; }
            if (iFiles.Any())
            {
                Log.Info(@"Get new files from: " + sourceDir);
                //Copy file from remote server to NetCollector Work directory
                
                foreach (var file in iFiles.Take(toTake))
                {
                    myStopWatch.Restart();
                    Helper.ManageFile(Helper.Action.Copy, file, settingsConfig.WorkDir);
                    Log.Info(String.Format("New file {0}", file));
                    if (settingsConfig.AllowRenameRemote)
                    {
                        Helper.ManageFile(Helper.Action.Move, file, sourceDir, @"_" + dateMask + settingsConfig.RenameRemoteExtension);
                        Log.Info(String.Format("Renaming to file {0} ", file + settingsConfig.RenameRemoteExtension));
                    }
                    myStopWatch.Stop();
                    Log.Info("Transferred in " + myStopWatch.RunTime());
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
                var myStopWatch = Stopwatch.StartNew();
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
                        myStopWatch.Restart();
                        ftp.Download(settingsConfig.RemoteDir + file.FtpItem.Name, settingsConfig.WorkDir + file.FtpItem.Name);
                        if (settingsConfig.AllowRenameRemote)
                        {
                            ftp.Rename(settingsConfig.RemoteDir + file.FtpItem.Name, settingsConfig.RemoteDir + Path.GetFileName(file.FtpItem.Name) + @"_" + dateMask + settingsConfig.RenameRemoteExtension);
                        }
                        myStopWatch.Stop();
                        Log.Info("Transferred in " + myStopWatch.RunTime());
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
                    var extension = Path.GetExtension(file);
                    if (extension.Contains("zip"))
                    {
                        Helper.ManageFile(Helper.Action.Unzip, file, settingsConfig.OutputDir);
                        Log.Info(String.Format("Unzip file {0} to {1}", file, settingsConfig.OutputDir));
                    }
                    else
                    {
                        Helper.ManageFile(Helper.Action.Move, file, settingsConfig.OutputDir);
                        Log.Info(String.Format("Move file {0} to {1}", file, settingsConfig.OutputDir));
                    }
                }

            }
            else
            {
                Log.Warn("No files for finallly move  from: " + settingsConfig.WorkDir);
            }
        }

        static void Main()
        {
            Log.InfoFormat(Helper.Version("NetCollector"));
            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {

                List<MyApConfig> appSettingsList = new List<MyApConfig>();

                var connStrings = ConfigurationManager.ConnectionStrings;
                if (connStrings != null)
                {
                    foreach (ConnectionStringSettings cs in connStrings)
                    {
                        MyApConfig fillApConfig = new MyApConfig(cs.Name);
                        appSettingsList.Add(fillApConfig);
                    }
                }


                foreach (var appSettings in appSettingsList)
                {
                    string missing;
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

                }
            } //using singleinstance
        }
    }
}
