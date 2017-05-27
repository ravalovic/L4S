using System;
using log4net;



namespace SQLBulkCopy
{
    class FastCsvReader : BaseCsvReader
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected CsvParser theParser;

        public FastCsvReader(string aFileName, string aDelimiter, string aTable, string aDatabase, string aServer, string aSchema, string aUser, string aPass)
        {
            //theParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(aFileName);
            theParser = new CsvParser(aFileName);
            //theParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            //theParser.SetDelimiters(aDelimiter);

            theFileFields = theParser.ReadFields(aDelimiter);

            getTableFields(aTable, aDatabase, aServer, aSchema, aUser, aPass);


            foreach (Field f in theTableFields)
            {
                int i = 0;
                foreach (string ff in theFileFields)
                {
                    if (ff == f.Name)
                    {
                        f.FileFieldPosition = i;
                    }
                    i++;
                }
            }
            rownum = 0;



        }

        public bool Read(string aDelimiter)
        {
            //while (!theParser.EndOfData)
            while (theParser.Read())
            {
                try
                {
                    theValues = theParser.ReadFields(aDelimiter);
                }
                catch (Exception ex)
                {
                    log.Warn("Skipped line " + ex.Message);
                }
                rownum++;

                return true;
            }
            return false;

        }


    }
}