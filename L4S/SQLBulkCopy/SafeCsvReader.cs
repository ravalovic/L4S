using System;
using System.Text;
using System.IO;


namespace SQLBulkCopy
{

    class SafeCsvReader : BaseCsvReader
    {
        protected Microsoft.VisualBasic.FileIO.TextFieldParser TheParser;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void CloseParser()
        {
            TheParser.Close();
        }
        public SafeCsvReader(string aFileName, MyApConfig configSettings)
        {

            Encoding myEncoding;

            using (var r = new StreamReader(aFileName, detectEncodingFromByteOrderMarks: true))
            {
                myEncoding = r.CurrentEncoding;
            }


            TheParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(aFileName, myEncoding);

            TheParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            TheParser.SetDelimiters(configSettings.InputFieldSepartor);

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
            while (!TheParser.EndOfData)
            {
                try
                {
                    TheValues = TheParser.ReadFields();
                }
                catch (Exception ex)
                {
                    Log.Warn("WARNING: skipped line " + ex.Message);
                }
                Rownum++;

                return true;
            }
            return false;

        }
    }

}