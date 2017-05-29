using System;
using System.Text;
using System.IO;


namespace SQLBulkCopy
{

    class SafeCsvReader : BaseCsvReader
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Microsoft.VisualBasic.FileIO.TextFieldParser theParser;

        public SafeCsvReader(string aFileName, MyAPConfig configSettings)
        {

            Encoding myEncoding;

            using (var r = new StreamReader(aFileName, detectEncodingFromByteOrderMarks: true))
            {
                myEncoding = r.CurrentEncoding;
            }


            theParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(aFileName, myEncoding);

            theParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            theParser.SetDelimiters(configSettings.InputFieldSepartor);

            theFileFields = theParser.ReadFields();

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

        public override bool Read()
        {
            while (!theParser.EndOfData)
            {
                try
                {
                    theValues = theParser.ReadFields();
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