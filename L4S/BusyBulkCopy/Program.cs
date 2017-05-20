using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusyBulkCopy
{
    class Program
    {
        static int Main(string[] args)
        {
            string myFile;
            string myTable;
            string myDatabase;
            string myServer;
            string mySchema;
            string myOption;

            if (args.Count() == 2)
            {
                try
                {
                    myFile = getConfig(args, 0);
                    myTable = getConfig(args, 1, 3);
                    myDatabase = getConfig(args, 1, 1);
                    myServer = getConfig(args, 1, 0);
                    mySchema = getConfig(args, 1, 2);
                    myOption = "safe";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"invalid arguments
usage: bcp_rfc4180.exe ""path\to\file.csv"" server.database.schema.table_to_insert_to fast_or_safe
" + ex.Message);
                    return -1;
                }
            }
            else if (args.Count() == 3)
            {
                try
                {
                    myFile = getConfig(args, 0);
                    myTable = getConfig(args, 1, 3);
                    myDatabase = getConfig(args, 1, 1);
                    myServer = getConfig(args, 1, 0);
                    mySchema = getConfig(args, 1, 2);
                    myOption = getConfig(args, 2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@"invalid arguments
usage: bcp_rfc4180.exe ""path\to\file.csv"" server.database.schema.table_to_insert_to fast_or_safe
" + ex.Message);
                    return -1;

                }
            }
            else
            {
                Console.WriteLine(@"usage: BusyBulkCopy.exe ""path\to\file.csv"" server.database.schema.table_to_insert_to [fast_or_safe]");
                return -1;
            }
            System.Diagnostics.Stopwatch myStopWatch = System.Diagnostics.Stopwatch.StartNew();
            myStopWatch.Start();

            try
            {
                if (myOption.ToLower() == "fast")
                {
                    FastCsvReader myReader = new FastCsvReader(myFile, myTable, myDatabase, myServer, mySchema);
                    bulkCopy(myTable, myServer, myDatabase, mySchema, myReader);
                }
                else
                {
                    SafeCsvReader myReader = new SafeCsvReader(myFile, myTable, myDatabase, myServer, mySchema);
                    bulkCopy(myTable, myServer, myDatabase, mySchema, myReader);
                }


                myStopWatch.Stop();
                Console.WriteLine("imported in " + myStopWatch.ElapsedMilliseconds / 1000);
                return 0;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        private static string getConfig(string[] args, int anArgument)
        {
            return args[anArgument];
        }
        private static string getConfig(string[] args, int anArgument, int aPosition)
        {
            return args[anArgument].Split(".".ToCharArray()[0])[aPosition];
        }

        private static void bulkCopy(string aTable, string aServer, string aDatabase, string aSchema, BaseCsvReader acsvReader)
        {

            using (SqlConnection myConnection = new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;Packet Size=32000;", aServer, aDatabase)))
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