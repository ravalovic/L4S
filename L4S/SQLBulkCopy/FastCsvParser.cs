using System;

namespace SQLBulkCopy
{
    class FastCsvReader : BaseCsvReader
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected CsvParser TheParser;

        public void CloseParser()
        {
            TheParser.CloseParser();
        }
        public FastCsvReader(string aFileName, MyApConfig configSettings)
        {
            TheParser = new CsvParser(aFileName);
            TheParser.Delimiter = configSettings.InputFieldSepartor[0]; //char 
            
            TheFileFields = TheParser.ReadFields();

            GetTableFields(configSettings);

            // check header line with table
            foreach (Field f in TheTableFields)
            {
                int i = 0;
                foreach (string ff in TheFileFields)
                {
                    if (ff.ToLower() == f.Name.ToLower())
                    {
                        f.FileFieldPosition = i;
                    }
                    i++;
                }
            }
            Rownum = 0;
            
        }

         public override bool Read()
        {
            //while (!TheParser.EndOfData)
            while (TheParser.Read())
            {
                try
                {
                    TheValues = TheParser.ReadFields();
                }
                catch (Exception ex)
                {
                    Log.Warn("Skipped line " + ex.Message);
                }
                Rownum++;

                return true;
            }
            return false;

        }
    }
}