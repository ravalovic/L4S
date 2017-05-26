using System;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Principal;
using CommonHelper;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace PreProcessor
{
    public class MyAPConfig
    {
        /// <summary>ConfigurationManager.AppSettings
        /// Initialization variable from App.config
        /// </summary>
        public string Drive { get; set; }

        public string InputDir { get; set; }
        public string OutputDir { get; set; }
        public string WorkDir { get; set; }
        public string OutputFileMask { get; set; }
        public string InputFileMask { get; set; }
        public string UnifiedMap { get; set; }
        public string InputFieldSeparator { get; set; }
        public string OutputFieldSeparator { get; set; }
        public string[] Patterns { get; set; }
        public string HeaderLine { get; set; }
        public long BatchID { get; set; }

        public MyAPConfig()
        {
            var configManager = new AppConfigManager();
            Drive = configManager.ReadSetting("drive");
            InputDir = configManager.ReadSetting("inputDir");
            OutputDir = configManager.ReadSetting("outputDir");
            WorkDir = configManager.ReadSetting("workDir");
            OutputFileMask = configManager.ReadSetting("outputFileMask");
            InputFileMask = configManager.ReadSetting("inputFileMask");
            UnifiedMap = configManager.ReadSetting("unifiedMap");
            InputFieldSeparator = configManager.ReadSetting("inputFieldSeparator");
            OutputFieldSeparator = configManager.ReadSetting("outputFieldSeparator");
            Patterns = ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("pattern")).Select(configManager.ReadSetting).ToArray();
            //Patterns = ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("pattern")).Select(key => ConfigurationManager.AppSettings[key]).ToArray();
            HeaderLine = configManager.ReadSetting("headerLine");
            // Get last value of Batch ID => increase => write back to config
            long bid;
            long.TryParse(configManager.ReadSetting("batchID"), out bid);
            BatchID = bid;
            bid++;
            configManager.AddUpdateAppSettings("batchID", bid.ToString());
        }
    }

    public class PreProcessor
    {
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
                var appSettings = new MyAPConfig();
                log.InfoFormat("Running as {0}", WindowsIdentity.GetCurrent().Name);
                GetFromNetCollector(appSettings);
                MoveProcessedFile(appSettings);
                ProcessAllFiles(appSettings);
            }
        }
        protected static void ProcessAllFiles(MyAPConfig configSettings)
        {
            //Read files from work exept Processed files
            var iFiles = Directory.GetFiles(configSettings.Drive + configSettings.WorkDir, configSettings.InputFileMask).Where(n => !n.Contains("PreProcessOK")).ToArray();

            if (iFiles.Any())
            {
                int i = 0;
                log.Info(@"Processing files from: " + configSettings.Drive + configSettings.WorkDir);
                foreach (var file in iFiles)
                {
                    if (File.Exists(file))
                    {
                        CreateOutputFile(file, configSettings);
                        log.Info(string.Format("Processed file {0} - {1}: ", i, file));
                        i++;
                    }
                }
            }
            else
            {
                log.Warn("No files for processing.");
            }
        }
        /// <summary>
        /// Move old processed file from work - emergency case only
        /// </summary>
        /// <param name="configSettings"></param>
        protected static void MoveProcessedFile(MyAPConfig configSettings)
        {
            // Check if some processed file  exist if yes move it to final dir
            var iFiles = Directory.GetFiles(configSettings.Drive + configSettings.WorkDir, configSettings.InputFileMask).Where(n => n.Contains("PreProcessOK")).ToArray();
            if (iFiles.Any())
            {
                log.Info(@"Move old processed files from: " + configSettings.Drive + configSettings.WorkDir + " to " + configSettings.Drive + configSettings.OutputDir);
                //Move file from NetCollector to PreProcessor Work directory
                int i = 0;
                foreach (var file in iFiles)
                {
                    ManageFile(Action.Move, file, configSettings.Drive + configSettings.OutputDir);
                    log.Info(string.Format("New file {0} - {1}: ", i, file));
                    i++;
                }
            }
        }
        /// <summary>
        /// Get files from NetCollector and prepare for processing
        /// </summary>
        /// <param name="configSettings"></param>
        protected static void GetFromNetCollector(MyAPConfig configSettings)
        {
            //Read files from NetCollector
            string[] iFiles = Directory.GetFiles(configSettings.Drive + configSettings.InputDir, configSettings.InputFileMask);
            if (iFiles.Any())
            {
                log.Info(@"Get new files from: " + configSettings.Drive + configSettings.InputDir);
                //Move file from NetCollector to PreProcessor Work directory
                int i = 0;
                foreach (var file in iFiles)
                {
                    ManageFile(Action.Move, file, configSettings.Drive + configSettings.WorkDir);
                    log.Info(String.Format("New file {0} - {1}: ", i, file));
                    i++;
                }
            }
        }


        /// <summary>
        /// Move, copy file to specified dir or delete file
        /// </summary>
        ///  <param name="action"></param>
        /// <param name="file"></param>
        /// <param name="destDir"></param>
        protected static void ManageFile(Action action, string file, string destDir)
        {
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
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

        /// <summary>
        /// Open file check if file is match line with all regexp.
        /// If line match then line is formated and write to file PreProcessOK_MMDDYYYYHH24MISS.csv
        /// NotMatched lines are ignored 
        /// <param name="iFile"></param>
        /// </summary>

        protected static void CreateOutputFile(string iFile, MyAPConfig configSettings)
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
                FileInfo oFile = new FileInfo(configSettings.Drive + configSettings.WorkDir + configSettings.OutputFileMask + dateMask + "_" + fname);
                StreamWriter sw = oFile.CreateText();
                //Start with header line
                sw.WriteLine(configSettings.HeaderLine);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (IsValidByReg(configSettings.Patterns, line))
                    {
                        var unifiedLine = MakeLine(configSettings.BatchID, oFile.Name, line, configSettings.UnifiedMap, configSettings.InputFieldSeparator, configSettings.OutputFieldSeparator);
                        sw.WriteLine(unifiedLine);
                    }
                }
                sr.Close();
                sw.Close();
                ManageFile(Action.Delete, iFile, "");
                ManageFile(Action.Move, oFile.FullName, configSettings.Drive + configSettings.OutputDir);
            }
            catch (IOException e)
            {
                log.Fatal(e.Message);
            }

        }

        /// <summary>
        /// Create new unified line, mapping input to output stage table format
        /// </summary>
        /// <param name="batchID"></param>
        /// <param name="fileName"></param>
       /// <param name="line"></param>
        /// <param name="unifiedMap"></param>
        /// <param name="inputFieldSeparator"></param>
        /// <param name="outputFieldSeparator"></param>
        /// <returns></returns>
        protected static string MakeLine(long batchID, string fileName, string line, string unifiedMap, string inputFieldSeparator, string outputFieldSeparator)
        {
            char separator = ',';
            string[] mapper = unifiedMap.Split(separator);
            string[] splittedLine = line.Split(char.Parse(inputFieldSeparator));
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
            return   batchID + outputFieldSeparator + fileName + outputFieldSeparator + string.Join(outputFieldSeparator, newLineArray);
        }

        /// <summary>
        /// Check if line is match by pattern
        /// </summary>
        /// <param name="patterns"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        protected static bool IsValidByReg(string[] patterns, string line)
        {
            bool result = false;
            foreach (var p in patterns)
            {
                var regex = new Regex(p);
                result = regex.IsMatch(line);
                if (result) break;
            }
            return result;
        }
    }
}
