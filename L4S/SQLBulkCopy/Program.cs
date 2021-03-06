using System;
using System.Data.SqlClient;
using System.Linq;
using CommonHelper;
using System.IO;
using System.Security.Principal;
using System.Data;
using System.Diagnostics;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

// Based on code from https://busybulkcopy.codeplex.com/

namespace SQLBulkCopy
{
    public class MyApConfig
    {
        /// <summary>ConfigurationManager.AppSettings
        /// Initialization variable from App.config
        /// </summary>
        public string InputDir { get; set; }
        public string OutputDir { get; set; }
        public string WorkDir { get; set; }
        public string InputFileName { get; set; }
        public string InputFileMask { get; set; }
        public string InputFieldSepartor { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
        public string Table { get; set; }
        public string FileInfoInsert { get; set; }
        public string CheckFileDuplicity { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string LoaderMode { get; set; }
        public int BatchSize { get; set; }
        public int BatchTimeout { get; set; }
        public bool SingleFileMode { get; set; }


        public MyApConfig()
        {
            var configManager = new AppConfigManager();
            InputDir = configManager.ReadSetting("inputDir");
            OutputDir = configManager.ReadSetting("outputDir");
            WorkDir = configManager.ReadSetting("workDir");
            InputFileName = configManager.ReadSetting("inputFileName");
            InputFileMask = configManager.ReadSetting("inputFileMask");
            InputFieldSepartor = configManager.ReadSetting("inputFieldSeparator");
            Server = configManager.ReadSetting("server");

            int serverPort;
            int.TryParse(configManager.ReadSetting("serverPort"), out serverPort);
            if (serverPort != 0 && serverPort != 1433) { Server = Server + "," + serverPort; }

            Database = configManager.ReadSetting("database");
            Schema = configManager.ReadSetting("schema");
            Table = configManager.ReadSetting("table");
            FileInfoInsert = configManager.ReadSetting("fileInfoInsert");
            CheckFileDuplicity = configManager.ReadSetting("checkFileDuplicity");
            bool integratedSecurity;
            bool.TryParse(configManager.ReadSetting("integratedSecurity"), out integratedSecurity);
            IntegratedSecurity = integratedSecurity;
            DbUser = configManager.ReadSetting("dbuser");
            DbPassword = configManager.ReadSetting("dbpassword");
            LoaderMode = configManager.ReadSetting("loaderMode").ToLower();
            bool oneFile;
            bool.TryParse(configManager.ReadSetting("singleFileMode"), out oneFile);
            SingleFileMode = oneFile;
            int batchSize;
            int.TryParse(configManager.ReadSetting("batchSize"), out batchSize);
            BatchSize = batchSize != 0 ? batchSize : 10000;
            int batchTimeout;
            int.TryParse(configManager.ReadSetting("batchTimeout"), out batchTimeout);
            BatchTimeout = batchTimeout != 0 ? batchTimeout : 60;

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

    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static void CreateIfMissing(MyApConfig appApConfig)
        {
            Directory.CreateDirectory(appApConfig.InputDir);
            Directory.CreateDirectory(appApConfig.OutputDir);
            Directory.CreateDirectory(appApConfig.WorkDir);
        }

        static void Main()
        {
            Log.InfoFormat(Helper.Version("SQLBulkCopy"));
            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
                string missing;
                var appSettings = new MyApConfig();

                if (appSettings.CheckParams(appSettings, out missing))
                {
                    Log.Error(missing);
                    Environment.Exit(0);
                }

                CreateIfMissing(appSettings);
                Log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                MoveProcessedFile(appSettings); // from PreProcessor Output to Work
                var myStopWatch = Stopwatch.StartNew();
                
                var iFiles = Directory.GetFiles(appSettings.WorkDir, appSettings.InputFileName);
                var toTake = iFiles.Length;
                if (appSettings.SingleFileMode) {toTake = 1;}
                if (iFiles.Any())
                {
                    foreach (var iFile in iFiles.Take(toTake))
                    {
                        int action = 0; // 0 - new file, 1 - duplicity file 
                        try
                        {
                            myStopWatch.Restart();
                            string checkSum = Helper.CalculateCheckSum(iFile);
                            int linesInFile = Helper.CountFileLines(iFile) - 1; //because header
                            int batchId;
                            Int32.TryParse(Helper.GetFromName(iFile, Helper.ParameterFromName.BatchId), out  batchId);
                            string originalFileChecksum = Helper.GetFromName(iFile,Helper.ParameterFromName.OriginalFileChecksum);
                            string originalFileName = Helper.GetFromName(iFile, Helper.ParameterFromName.OriginalFileName);
                            myStopWatch.Start();
                            if (appSettings.LoaderMode.ToLower() == "fast")
                            {
                                Log.Info("Loading file: " + iFile + " Loader mode: " + "fast");

                                if (CheckFileDuplicity(checkSum, originalFileChecksum, appSettings))
                                {
                                    FastCsvReader myReader = new FastCsvReader(iFile, appSettings);
                                    BulkCopy(myReader, appSettings);
                                    myReader.CloseParser();
                                    myReader.Close();
                                    Log.Info("New file " + iFile + " loaded with " + linesInFile + " lines. File Checksum " + checkSum);
                                }
                                else
                                {
                                    Log.Info("Duplicate file " + iFile + " . Skipping... File Checksum " + checkSum);
                                    action = 1;
                                }

                            }
                            else
                            {
                                Log.Info("Loading file: " + iFile + " Loader mode: " + "safe");

                                if (CheckFileDuplicity(checkSum, originalFileChecksum, appSettings))
                                {
                                    SafeCsvReader myReader = new SafeCsvReader(iFile, appSettings);
                                    BulkCopy(myReader, appSettings);
                                    myReader.CloseParser();
                                    myReader.Close();
                                    Log.Info("New file " + iFile + " loaded with " + linesInFile + " lines. File Checksum " + checkSum);
                                }
                                else
                                {
                                    Log.Info("Duplicate file " + iFile + ". Skipping... File Checksum " + checkSum);
                                    action = 1;
                                }

                            }
                            WritFileInfo(iFile, checkSum, originalFileName, originalFileChecksum, batchId, linesInFile, action, appSettings);
                            Log.Info("Create backup of file:" + iFile);
                            Helper.ManageFile(Helper.Action.Zip, iFile, appSettings.OutputDir);
                            Log.Info("Delete processed file:" + iFile);
                            Helper.ManageFile(Helper.Action.Delete, iFile);

                            myStopWatch.Stop();
                            Log.Info("imported in " + myStopWatch.RunTime());
                            

                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                        }
                    }
                }
                else
                {
                    Log.Info("No new files for processing");
                }
            }

        } //main

        private static void WritFileInfo(string myFile, string myChecksum, string myOriginalFileName, string myOriginalFileChecksum, int myBatchId, int myLinesInFile, int myAction, MyApConfig configSettings)
        {
            using (SqlConnection myConnection =
                configSettings.IntegratedSecurity ?
                    new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Packet Size=32000;", configSettings.Server, configSettings.Database))
                    :
                    new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3} ;Packet Size=32000;", configSettings.Server, configSettings.Database, configSettings.DbUser, configSettings.DbPassword)))
            {
                using (SqlCommand myCmd = new SqlCommand())
                {
                    myCmd.Connection = myConnection;
                    myCmd.CommandTimeout = 600;
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.CommandText = configSettings.Schema + "." + configSettings.FileInfoInsert;
                    myCmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = myFile;
                    myCmd.Parameters.Add("@FileCheckSum", SqlDbType.VarChar).Value = myChecksum;
                    myCmd.Parameters.Add("@OriginalFilename", SqlDbType.VarChar).Value = myOriginalFileName;
                    myCmd.Parameters.Add("@OriginalFileCheckSum", SqlDbType.VarChar).Value = myOriginalFileChecksum;
                    myCmd.Parameters.Add("@BatchID", SqlDbType.Int).Value = myBatchId;
                    myCmd.Parameters.Add("@LinesInFile", SqlDbType.Int).Value = myLinesInFile;
                    myCmd.Parameters.Add("@Action", SqlDbType.Int).Value = myAction;
                    myConnection.Open();
                    myCmd.ExecuteNonQuery();
                    myCmd.Dispose();

                }
                myConnection.Close();
            }
       }

        private static bool CheckFileDuplicity(string myChecksum, string myOriginalChecksum, MyApConfig configSettings)
        {
            bool retval = false;
            using (SqlConnection myConnection =
                configSettings.IntegratedSecurity ?
                    new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Packet Size=32000;", configSettings.Server, configSettings.Database))
                    :
                    new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3} ;Packet Size=32000;", configSettings.Server, configSettings.Database, configSettings.DbUser, configSettings.DbPassword)))
            {

                using (SqlCommand myCmd = new SqlCommand())
                {
                    myCmd.Connection = myConnection;
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.CommandText = configSettings.Schema + "." + configSettings.CheckFileDuplicity;
                    myCmd.Parameters.Add("@FileCheckSum", SqlDbType.VarChar).Value = myChecksum;
                    myCmd.Parameters.Add("@OriFileCheckSum", SqlDbType.VarChar).Value = myOriginalChecksum;
                    myCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                    myConnection.Open();
                    myCmd.ExecuteNonQuery();
                    int retCode = (int)myCmd.Parameters["@RetVal"].Value;
                    if (retCode == 0)
                    {
                        retval = true;
                    }
                    myCmd.Dispose();
                }
                myConnection.Close();
            }
            return retval;
        }

        private static void BulkCopy(BaseCsvReader acsvReader, MyApConfig configSettings)
        {

            using (SqlConnection myConnection =
                configSettings.IntegratedSecurity ?
                new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Packet Size=32000;", configSettings.Server, configSettings.Database))
                :
                new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3} ;Packet Size=32000;", configSettings.Server, configSettings.Database, configSettings.DbUser, configSettings.DbPassword)))
            {
                myConnection.Open();
                using (SqlBulkCopy myBulkCopy = new SqlBulkCopy(myConnection))
                {
                    myBulkCopy.BulkCopyTimeout = configSettings.BatchTimeout;
                    myBulkCopy.BatchSize = configSettings.BatchSize;
                    myBulkCopy.DestinationTableName = configSettings.Schema + "." + configSettings.Table;
                    myBulkCopy.WriteToServer(acsvReader);
                    myBulkCopy.Close();

                }
                myConnection.Close();
            }
        }

        /// <summary>
        /// Move old processed file from work - emergency case only
        /// </summary>
        /// <param name="configSettings"></param>
        protected static void MoveProcessedFile(MyApConfig configSettings)
        {
            // Check if some processed file  exist if yes move it to final dir
            var iFiles = Directory.GetFiles(configSettings.InputDir, configSettings.InputFileName).Where(n => n.Contains(configSettings.InputFileMask)).ToArray();
            if (iFiles.Any())
            {
                Log.Info(@"Move PreProcessed files from: " + configSettings.InputDir + " to " + configSettings.WorkDir);
                //Move file from NetCollector to PreProcessor Work directory
                int i = 0;
                foreach (var file in iFiles)
                {
                    Helper.ManageFile(Helper.Action.Move, file, configSettings.WorkDir);
                    Log.Info(string.Format("New file {0} - {1}: ", i, file));
                    i++;
                }
            }
        }
    }
}