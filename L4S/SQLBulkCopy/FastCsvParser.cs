using System;

namespace SQLBulkCopy
{
    class FastCsvReader : BaseCsvReader
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected CsvParser theParser;

        public void CloseParser()
        {
            theParser.CloseParser();
        }
        public FastCsvReader(string aFileName, MyAPConfig configSettings)
        {
            theParser = new CsvParser(aFileName);
            
            theFileFields = theParser.ReadFields(configSettings.InputFieldSepartor);

            getTableFields(configSettings);

            // check header line with table
            foreach (Field f in theTableFields)
            {
                int i = 0;
                foreach (string ff in theFileFields)
                {
                    if (ff.ToLower() == f.Name.ToLower())
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