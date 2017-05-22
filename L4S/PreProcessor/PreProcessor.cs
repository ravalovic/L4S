using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Security.Principal;
using SingleInstance;

// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

namespace PreProcessor
{

    class PreProcessor
    {
        /// <summary>
        /// Initialization variable from App.config
        /// </summary>
        protected static string drive = ConfigurationManager.AppSettings["drive"];
        protected static string inputDir = ConfigurationManager.AppSettings["inputDir"];
        protected static string outputDir = ConfigurationManager.AppSettings["outputDir"];
        protected static string workDir = ConfigurationManager.AppSettings["workDir"];
        protected static string ouputFileMask = ConfigurationManager.AppSettings["ouputFileMask"];
        protected static string inputFileMask = ConfigurationManager.AppSettings["inputFileMask"];
        protected static string unifiedMap = ConfigurationManager.AppSettings["unifiedMap"];
        protected static string inputFieldSeparator = ConfigurationManager.AppSettings["inputFieldSeparator"];
        protected static string outputFieldSeparator = ConfigurationManager.AppSettings["outputFieldSeparator"];

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
               
                //Read files from NetCollector
                string[] iFiles = Directory.GetFiles(drive + inputDir, inputFileMask);
                if (iFiles.Any())
                {
                    log.Info(@"Get new files from: " + drive + inputDir);
                    //Move file from NetCollector to PreProcessor Work directory
                    int i = 0;
                    foreach (var file in iFiles)
                    {
                        ManageFile(Action.Move, file, drive + workDir);
                        log.Info(String.Format("New file {0} - {1}: ", i, file));
                        i++;
                    }
                }

                // Check if some processed file  exist if yes move it to final dir
                iFiles = Directory.GetFiles(drive + workDir, inputFileMask).Where(n => n.Contains("PreProcessOK")).ToArray();
                if (iFiles.Any())
                {
                    log.Info(@"Move old processed files from: " + drive + workDir + " to "+ drive+ outputDir);
                    //Move file from NetCollector to PreProcessor Work directory
                    int i = 0;
                    foreach (var file in iFiles)
                    {
                        ManageFile(Action.Move, file, drive + outputDir);
                        log.Info(String.Format("New file {0} - {1}: ", i, file));
                        i++;
                    }
                }
                //Read files from work exept Processed files
                iFiles = Directory.GetFiles(drive + workDir, inputFileMask).Where(n => !n.Contains("PreProcessOK")).ToArray();

                if (iFiles.Any())
                {
                    //Read Regexp patterns
                    string[] patterns = ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("pattern")).
                                                             Select(key => ConfigurationManager.AppSettings[key]).ToArray();
                    int i = 0;
                    log.Info(@"Processing files from: " + drive + workDir);
                    foreach (var file in iFiles)
                    {
                        if (File.Exists(file))
                        {
                            ProcessFile(file, patterns);
                            log.Info(String.Format("Processed file {0} - {1}: ", i, file));
                            i++;
                        }
                    }
                }
                else
                {
                    log.Warn("No files for processing.");
                }
                //Console.ReadKey();
            }
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

        /// <summary>
        /// Open file check if file is match line with all regexp.
        /// If line match then line is formated and write to file PreProcessOK_MMDDYYYYHH24MISS.csv
        /// NotMatched lines are ignored 
        /// </summary>
        /// <param name="iFile"></param>
        protected static void ProcessFile(string iFile, string[] regPatterns)
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
                FileInfo oFile = new FileInfo(drive + workDir + ouputFileMask + dateMask + "_" + fname);
                StreamWriter sw = oFile.CreateText();
                string line = String.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    if (IsValidByReg(regPatterns, line))
                    {
                        var unifiedLine = MakeLine(line);
                        sw.WriteLine(unifiedLine);
                    }
                }
                sr.Close();
                sw.Close();
                ManageFile(Action.Delete, iFile, "");
                ManageFile(Action.Move, oFile.FullName, drive + outputDir);
            }
            catch (IOException e)
            {
                log.Fatal(e.Message);
            }

        }
        /// <summary>
        /// Create new unified line, mapping input to output stage table format
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        protected static string MakeLine(string line)
        {
            char separator = ',';
            string[] mapper = unifiedMap.Split(separator);
            string[] splittedLine = line.Split(char.Parse(inputFieldSeparator));
            string[] newLineArray = new string[mapper.Count()];
            int i = 0;
            int index = 0;
            foreach (var map in mapper)
            {
                if (Int32.TryParse(map, out index))
                {
                    if (index <= splittedLine.Count() - 1)
                    {
                        newLineArray[i] = splittedLine[index];
                    }
                }
                i++;
            }
            return string.Join(outputFieldSeparator, newLineArray);
        }

        /// <summary>
        /// Check if line is match by pattern
        /// </summary>
        /// <param name="pattern"></param>
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
