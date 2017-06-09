using System;
using System.Diagnostics;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Principal;
using CommonHelper;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace PreProcessor
{
    public class MyApConfig
    {
        /// <summary>ConfigurationManager.AppSettings
        /// Initialization variable from App.config
        /// </summary>
        public string InputDir { get; set; }
        public string OutputDir { get; set; }
        public string WorkDir { get; set; }
        public string OutputFileMask { get; set; }
        public string InputFileName { get; set; }
        public string OutputMapping { get; set; }
        public string InputFieldSeparator { get; set; }
        public string OutputFieldSeparator { get; set; }
        public string[] Patterns { get; set; }
        public string HeaderLine { get; set; }
        public long BatchId { get; set; }
        
        public MyApConfig()
        {
            var configManager = new AppConfigManager();
            InputDir = configManager.ReadSetting("inputDir");
            OutputDir = configManager.ReadSetting("outputDir");
            WorkDir = configManager.ReadSetting("workDir");
            OutputFileMask = configManager.ReadSetting("outputFileMask");
            InputFileName = configManager.ReadSetting("inputFileName");
            OutputMapping = configManager.ReadSetting("OutputMapping");
            InputFieldSeparator = configManager.ReadSetting("inputFieldSeparator");
            OutputFieldSeparator = configManager.ReadSetting("outputFieldSeparator");
            Patterns = ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("pattern")).Select(configManager.ReadSetting).ToArray();
            //Patterns = ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("pattern")).Select(key => ConfigurationManager.AppSettings[key]).ToArray();
            HeaderLine = configManager.ReadSetting("fixField") +OutputFieldSeparator+ configManager.ReadSetting("inputDataField");
            
            // Get last value of Batch ID 
            long batchId;
            long.TryParse(configManager.ReadSetting("batchID"), out batchId);
            BatchId = batchId ;
        }

        public void UpdateBatchId(long batchId)
        {
            var configManager = new AppConfigManager();
            configManager.AddUpdateAppSettings("batchID", batchId.ToString());
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
                return  sw.ElapsedMilliseconds + "ms";
            }
        }

    }
    public class PreProcessor
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static void CreateIfMissing(MyApConfig appApConfig)
        {
            Directory.CreateDirectory(appApConfig.InputDir);
            Directory.CreateDirectory(appApConfig.WorkDir);
            Directory.CreateDirectory(appApConfig.OutputDir);
        }
        static void Main()
        {
            using (new SingleGlobalInstance(1000)) //1000ms timeout on global lock
            {
                var myStopWatch = Stopwatch.StartNew();
                string missing;
                var appSettings = new MyApConfig();
                if (appSettings.CheckParams(appSettings, out missing))
                {
                    Log.Error(missing);
                    Environment.Exit(0);
                }
                CreateIfMissing(appSettings);
                Log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                GetFromNetCollector(appSettings);
                MoveProcessedFile(appSettings);
                ProcessAllFiles(appSettings);
                myStopWatch.Stop();
                Log.Info("Processed in " + myStopWatch.RunTime());
            }
        }
        protected static void ProcessAllFiles(MyApConfig configSettings)
        {
            //Read files from work exept Processed files
            var iFiles = Directory.GetFiles(configSettings.WorkDir, configSettings.InputFileName).Where(n => !n.Contains("PreProcessOK")).ToArray();

            if (iFiles.Any())
            {
                Log.Info(@"Processing files from: " + configSettings.WorkDir);
                foreach (var file in iFiles)
                {
                    if (File.Exists(file))
                    {
                        CreateOutputFile(file, configSettings);
                        configSettings.UpdateBatchId(configSettings.BatchId++);
                        Log.Info(string.Format("Processed file {0} ", file));
                       
                    }
                }
                configSettings.UpdateBatchId(configSettings.BatchId++);
            }
            else
            {
                Log.Warn("No files for processing.");
            }
        }
        /// <summary>
        /// Move old processed file from work - emergency case only
        /// </summary>
        /// <param name="configSettings"></param>
        protected static void MoveProcessedFile(MyApConfig configSettings)
        {
            // Check if some processed file  exist if yes move it to final dir
            var iFiles = Directory.GetFiles(configSettings.WorkDir, configSettings.InputFileName).Where(n => n.Contains("PreProcessOK")).ToArray();
            if (iFiles.Any())
            {
                Log.Info(@"Move old processed files from: " + configSettings.WorkDir + " to " + configSettings.OutputDir);
                //Move file from NetCollector to PreProcessor Work directory
                
                foreach (var file in iFiles)
                {
                    Helper.ManageFile(Helper.Action.Move, file, configSettings.OutputDir);
                    Log.Info(string.Format("New file {0}",file));
              }
            }
        }
        /// <summary>
        /// Get files from NetCollector and prepare for processing
        /// </summary>
        /// <param name="configSettings"></param>
        protected static void GetFromNetCollector(MyApConfig configSettings)
        {
            //Read files from NetCollector
            string[] iFiles = Directory.GetFiles(configSettings.InputDir, configSettings.InputFileName);
            if (iFiles.Any())
            {
                Log.Info(@"Get new files from: " + configSettings.InputDir);
                //Move file from NetCollector to PreProcessor Work directory
                foreach (var file in iFiles)
                {
                    Helper.ManageFile(Helper.Action.Move, file, configSettings.WorkDir);
                    Log.Info(String.Format("New file {0} ", file));
                 }
            }
            else
            {
                Log.Info("No new files for processing");
            }
        }

        
        /// <summary>
        /// Open file check if file is match line with all regexp.
        /// If line match then line is formated and write to file PreProcessOK_MMDDYYYYHH24MISS.csv
        /// NotMatched lines are ignored 
        /// <param name="iFile"></param>
        /// </summary>

        protected static void CreateOutputFile(string iFile, MyApConfig configSettings)
        {
            //Open file
            StreamReader sr = File.OpenText(iFile);
            //Define datetime mask part
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");

            //Open output file
            try
            {
                //extract file name
                string fname = Path.GetFileName(iFile);
                string oriCheckSum = Helper.CalculateCheckSum(iFile);
                FileInfo oFile = new FileInfo(configSettings.WorkDir + configSettings.OutputFileMask+"_"+ configSettings.BatchId + "_" + dateMask + "_" + fname);
                StreamWriter sw = oFile.CreateText();
                //Start with header line
                sw.WriteLine(configSettings.HeaderLine);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (Helper.IsValidByReg(configSettings.Patterns, line) && !string.IsNullOrWhiteSpace(line))
                    {
                        var unifiedLine = MakeLine(configSettings, iFile, oriCheckSum, oFile.Name, line);
                        sw.WriteLine(unifiedLine);
                    }
                }
                sr.Close();
                sw.Close();
                Helper.ManageFile(Helper.Action.Delete, iFile);
                Helper.ManageFile(Helper.Action.Move, oFile.FullName, configSettings.OutputDir);
                
            }
            catch (IOException e)
            {
                Log.Fatal(e.Message);
            }

        }

        /// <summary>
        /// Create new unified line, mapping input to output stage table format
        /// </summary>
        /// <param name="myConfig"></param>
        /// <param name="origFileName"></param>
        /// <param name="checkSum"></param>
        /// <param name="fileName"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        protected static string MakeLine(MyApConfig myConfig, string origFileName, string checkSum, string fileName, string line)
        {
            char separator = ',';
            string[] mapper = myConfig.OutputMapping.Split(separator);
            string[] splittedLine = line.Split(char.Parse(myConfig.InputFieldSeparator));
            string[] newLineArray = new string[mapper.Count()];
            int i = 0;
            foreach (var map in mapper)
            {
                int index;
                if (Int32.TryParse(map, out index))
                {
                    if (index <= splittedLine.Count() - 1)
                    {
                        newLineArray[i] = splittedLine[index];
                    }
                }
                i++;
            }
            return myConfig.BatchId + myConfig.OutputFieldSeparator + 
                     origFileName + myConfig.OutputFieldSeparator + 
                     checkSum + myConfig.OutputFieldSeparator +
                     fileName + myConfig.OutputFieldSeparator +
                     string.Join(myConfig.OutputFieldSeparator, newLineArray);
        }
    }
}
