using System;
using System.Data.SqlClient;
using System.Linq;
using CommonHelper;
using System.IO;
using System.Security.Cryptography;


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

// Based on code from https://busybulkcopy.codeplex.com/

namespace SQLBulkCopy
{
    public class MyAPConfig
    {
        /// <summary>ConfigurationManager.AppSettings
        /// Initialization variable from App.config
        /// </summary>
        public string InputDir { get; set; }
        public string OutputDir { get; set; }
        public string InputFileName { get; set; }
        public string InputFieldSepartor { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
        public string Table { get; set; }
        public string FileInfoInsert { get; set; }
        public bool IntegratedSecurity { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }
        public string LoaderMode { get; set; }
        public int BatchSize { get; set; }
        public int BatchTimeout { get; set; }


        public MyAPConfig()
        {
            var configManager = new AppConfigManager();
            InputDir = configManager.ReadSetting("inputDir");
            OutputDir = configManager.ReadSetting("outputDir");
            InputFileName = configManager.ReadSetting("inputFileName");
            InputFieldSepartor = configManager.ReadSetting("inputFieldSeparator");
            Server = configManager.ReadSetting("server");

            int serverPort;
            int.TryParse(configManager.ReadSetting("serverPort"), out serverPort);
            if (serverPort != 0 && serverPort != 1433) { Server = Server + "," + serverPort; }
  
            Database = configManager.ReadSetting("database");
            Schema = configManager.ReadSetting("schema");
            Table = configManager.ReadSetting("table");
            FileInfoInsert = configManager.ReadSetting("fileInfoInsert");
            bool integratedSecurity;
            bool.TryParse(configManager.ReadSetting("integratedSecurity"), out integratedSecurity);
            IntegratedSecurity = integratedSecurity;
            DBUser = configManager.ReadSetting("dbuser");
            DBPassword = configManager.ReadSetting("dbpassword");
            LoaderMode = configManager.ReadSetting("loaderMode").ToLower();

            int batchSize;
            int.TryParse(configManager.ReadSetting("batchSize"), out batchSize);
            BatchSize = batchSize != 0 ? batchSize : 10000;
            int batchTimeout;
            int.TryParse(configManager.ReadSetting("batchTimeout"), out batchTimeout);
            BatchTimeout = batchTimeout != 0 ? batchTimeout : 60;

        }

        public bool CheckParams(object MyConfig, out string missingParams)
        {
            bool isInComplete = false;
            missingParams = string.Empty;
            string mp = string.Empty;
            foreach (var propertyInfo in MyConfig.GetType().GetProperties())
            {
                string value = propertyInfo.GetValue(MyConfig).ToString();
                if (String.IsNullOrEmpty(value))
                {
                    mp = mp + " " + propertyInfo.GetValue(MyConfig).ToString();
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
    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static void CreateIfMissing(MyAPConfig appApConfig)
        {
            Directory.CreateDirectory(appApConfig.InputDir);
            Directory.CreateDirectory(appApConfig.OutputDir);
        }
        static void Main()
        {
            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
                string missing;
                var appSettings = new MyAPConfig();
                if (appSettings.CheckParams(appSettings, out missing))
                {
                    log.Error(missing);
                    Environment.Exit(0);
                }
                CreateIfMissing(appSettings);
                System.Diagnostics.Stopwatch myStopWatch = System.Diagnostics.Stopwatch.StartNew();

                var iFiles = Directory.GetFiles(appSettings.InputDir, appSettings.InputFileName);
                if (iFiles.Any()) {
                    foreach (var iFile in iFiles)
                    {
                try
                {
                    myStopWatch.Start();
                    if (appSettings.LoaderMode.ToLower() == "fast")
                    {
                        FastCsvReader myReader = new FastCsvReader(iFile, appSettings);
                        bulkCopy(myReader, appSettings);
                        WritFileInfo(iFile, appSettings);
                    }
                    else
                    {
                        SafeCsvReader myReader = new SafeCsvReader(iFile, appSettings);
                        bulkCopy(myReader, appSettings);
                        WritFileInfo(iFile, appSettings);
                            }
                    myStopWatch.Stop();
                    log.Info("imported in " + myStopWatch.ElapsedMilliseconds / 1000);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                }
                    }
                }
            }
        } //main

        private static void WritFileInfo(string myFile, MyAPConfig configSettings)
        {
            using (SqlConnection myConnection =
                configSettings.IntegratedSecurity ?
                    new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Packet Size=32000;", configSettings.Server, configSettings.Database))
                    :
                    new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3} ;Packet Size=32000;", configSettings.Server, configSettings.Database, configSettings.DBUser, configSettings.DBPassword)))
            {
                
                //using (SqlCommand myCMD = new SqlCommand(configSettings.Schema+"."+configSettings.FileInfoInsert, myConnection))
                //{
                //    myCMD.CommandType = CommandType.StoredProcedure;
                //    myCMD.Parameters.Add("@FileName",SqlDbType.VarChar).Value = myFile;
                //    myCMD.Parameters.Add("@FileCheckSum", SqlDbType.VarChar).Value = CalculateCheckSum(myFile);
                //    myConnection.Open();
                //    myCMD.ExecuteNonQuery();
                //}
                myConnection.Close();
            }
        }

        private static string CalculateCheckSum(string myFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(myFile))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }

        private static void bulkCopy(BaseCsvReader acsvReader, MyAPConfig configSettings)
        {
           
            using (SqlConnection myConnection = 
                configSettings.IntegratedSecurity ? 
                new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Packet Size=32000;", configSettings.Server, configSettings.Database))
                : 
                new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3} ;Packet Size=32000;", configSettings.Server, configSettings.Database, configSettings.DBUser, configSettings.DBPassword)))
            {
                myConnection.Open();
                using (SqlBulkCopy myBulkCopy = new SqlBulkCopy(myConnection))
                {
                    myBulkCopy.BulkCopyTimeout = configSettings.BatchTimeout;
                    myBulkCopy.BatchSize = configSettings.BatchSize;
                    myBulkCopy.DestinationTableName = configSettings.Schema + "." + configSettings.Table;
                    myBulkCopy.WriteToServer(acsvReader);
                    
                }
                myConnection.Close();
            }
        }
    }
}