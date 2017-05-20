using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreProcessor
{
   
    class Options
    {
        [Option('i', "input file", DefaultValue = "", HelpText = "Specify input file")]
        public int InputFile { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
    class Program
    {
        internal static string drive = ConfigurationManager.AppSettings["drive"];
        internal static string inputDir = ConfigurationManager.AppSettings["inputDir"];
        internal static string outputDir = ConfigurationManager.AppSettings["outputDir"];
        internal static string workDir = ConfigurationManager.AppSettings["workDir"];
        internal static string backupDir = ConfigurationManager.AppSettings["backupDir"];
        internal static string fileMask = ConfigurationManager.AppSettings["fileMask"];

        internal static bool IsValidByReg(string pattern, string line )
        {
            return pattern == line;
        }

        static void Main(string[] args)
        {

            Console.WriteLine(@"input dir: {0}", inputDir);
            string[] iFiles = Directory.GetFiles(drive+inputDir, fileMask);
            string[] patterns = ConfigurationManager.AppSettings.AllKeys.Where(key => key.StartsWith("pattern")).Select(key => ConfigurationManager.AppSettings[key]).ToArray();
            int i = 1;
            foreach (var s in iFiles)
            {
                Console.Write(@"File {0}: {1}  ", i++, s);
                Console.WriteLine(IsValidByReg(s, s));
            }
            i = 1;
            foreach (var p in patterns)
            {
                Console.WriteLine(@"Pattern  {0}: {1}", i++, p);
            }

            Console.ReadKey();  
        }
    }
}
