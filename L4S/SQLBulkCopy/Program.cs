using System;
using System.Data.SqlClient;
using System.Linq;


// Configure log4net using the .config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
// This will cause log4net to look for a configuration file

// Based on code from https://busybulkcopy.codeplex.com/

namespace SQLBulkCopy
{
    //class Options
    //{
    //    [Option('F', "Filename", Required = true,
    //      HelpText = @"Path\FileName")]
    //    public string FileName { get; set; }

    //    [Option('S', "Server", Required = true,
    //      HelpText = "Server name if not default port use: serverName, Port")]
    //    public string ServerName { get; set; }

    //    [Option('T', "Table name", Required = true,
    //      HelpText = "Format: database.schema.table")]
    //    public string TableName { get; set; }

    //    [Option('I', "Integrated security", DefaultValue = true,
    //     HelpText = "If we use Integrated security for MSSQL")]
    //    public bool Integrated { get; set; }

    //    [Option('U', "User",
    //      HelpText = "User for db connect")]
    //    public string User { get; set; }

    //    [Option('P', "Password",
    //      HelpText = "Password ")]
    //    public string Password { get; set; }

    //    [Option('S', "Save parser mode", DefaultValue = true,
    //      HelpText = "SAVE parser mode (more checkin and setup) if flase then FAST parser mode use ")]
    //    public bool Mode { get; set; }

    //    [Option('D', "Delimiter", DefaultValue = ",",
    //      HelpText = "Field delimiter, you can set it only in safe mode ")]
    //    public string Delimiter { get; set; }


    //    [ParserState]
    //    public IParserState LastParserState { get; set; }

    //    [HelpOption]
    //    public string GetUsage()
    //    {
    //        return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
    //    }

    //}

    class Program
    {
        // Create a logger for use in this class
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static int Main(string[] args)
        {


            //var options = new Options();
            //if (Parser.Default.ParseArguments(args, options))
            //{
            //    while (options != null)
            //    {
            //        Console.WriteLine("Filename: {0}", options.InputFile);
            //    }
            //    // Values are available here
            //    //if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
            // Microsoft.Test.CommandLineParser
            //}


            string myFile;
            string myTable;
            string myDatabase;
            string myServer;
            string mySchema;
            string myOption;
            string myUser;
            string myPass;
            string myDelimiter;
            //aServer = @"bluez.bzde.net,11433";
            //aDatabase = @"log4service";
            //string aUser = @"sa";
            //string aPass = @"MSsql2014.";

            if (args.Count() == 2)
            {
                try
                {
                    myFile = getConfig(args, 0);
                    //myTable = getConfig(args, 1, 3);
                    //myDatabase = getConfig(args, 1, 1);
                    //myServer = getConfig(args, 1, 0);
                    //mySchema = getConfig(args, 1, 2);
                    myServer = @"bluez.bzde.net,11433";
                    myDatabase = @"log4service";
                    myTable = @"Stage_TestTable";
                    myUser = @"sa";
                    myPass = @"MSsql2014.";
                    mySchema = @"dbo";
                    myDelimiter = @"|";
                    myOption = "safe";
                }
                catch (Exception ex)
                {
                    log.Error(@"invalid arguments usage: bcp_rfc4180.exe ""path\to\file.csv"" server.database.schema.table_to_insert_to fast_or_safe" + ex.Message);
                    return -1;
                }
            }
            else if (args.Count() == 3)
            {
                try
                {
                    myFile = getConfig(args, 0);
                    //myTable = getConfig(args, 1, 3);
                    //myDatabase = getConfig(args, 1, 1);
                    //myServer = getConfig(args, 1, 0);
                    //mySchema = getConfig(args, 1, 2);
                    myOption = getConfig(args, 2);
                    myServer = @"bluez.bzde.net,11433";
                    myDatabase = @"log4service";
                    myTable = @"Stage_TestTable";
                    myUser = @"sa";
                    myPass = @"MSsql2014.";
                    mySchema = @"dbo";
                    myDelimiter = @"|";

                }
                catch (Exception ex)
                {
                    log.Error(@"invalid arguments usage: bcp_rfc4180.exe ""path\to\file.csv"" server.database.schema.table_to_insert_to fast_or_safe" + ex.Message);
                    return -1;

                }
            }
            else
            {
                log.Info(@"usage: BusyBulkCopy.exe ""path\to\file.csv"" server.database.schema.table_to_insert_to [fast_or_safe]");
                return -1;
            }

            System.Diagnostics.Stopwatch myStopWatch = System.Diagnostics.Stopwatch.StartNew();
            myStopWatch.Start();

            try
            {
                if (myOption.ToLower() == "fast")
                {
                    FastCsvReader myReader = new FastCsvReader(myFile, myDelimiter, myTable, myDatabase, myServer, mySchema, myUser, myPass);
                    bulkCopy(myTable, myServer, myDatabase, mySchema, myReader, myUser, myPass);
                }
                else
                {
                    SafeCsvReader myReader = new SafeCsvReader(myFile, myDelimiter, myTable, myDatabase, myServer, mySchema, myUser, myPass);
                    bulkCopy(myTable, myServer, myDatabase, mySchema, myReader, myUser, myPass);
                }


                myStopWatch.Stop();
                log.Info("imported in " + myStopWatch.ElapsedMilliseconds / 1000);
                return 0;
            }

            catch (Exception ex)
            {
                log.Error(ex.Message);
                return -1;
            }
        } //main

        private static string getConfig(string[] args, int anArgument)
        {
            return args[anArgument];
        }
        private static string getConfig(string[] args, int anArgument, int aPosition)
        {
            return args[anArgument].Split(".".ToCharArray()[0])[aPosition];
        }

        private static void bulkCopy(string aTable, string aServer, string aDatabase, string aSchema, BaseCsvReader acsvReader, string aUser, string aPass)
        {
           
            using (SqlConnection myConnection =
                //new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Packet Size=32000;", aServer, aDatabase)))
                new SqlConnection(string.Format("Data Source={0};Initial Catalog={1};User ID={2}; Password={3} ;Packet Size=32000;", aServer, aDatabase,aUser,aPass)))
            {
                myConnection.Open();
                using (SqlBulkCopy myBulkCopy = new SqlBulkCopy(myConnection))//, SqlBulkCopyOptions.TableLock, null))
                {
                    myBulkCopy.BulkCopyTimeout = 60;
                    myBulkCopy.BatchSize = 10000;
                    myBulkCopy.DestinationTableName = aSchema + "." + aTable;
                    myBulkCopy.WriteToServer(acsvReader);
                    //bulkCopy.WriteToServer(aDataTable.CreateDataReader());
                }
                myConnection.Close();
            }
        }
    }
}