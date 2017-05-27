using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using log4net;

namespace SQLBulkCopy
{

    class SafeCsvReader : BaseCsvReader
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected Microsoft.VisualBasic.FileIO.TextFieldParser theParser;

        public SafeCsvReader(string aFileName, string aDelimiter, string aTable, string aDatabase, string aServer, string aSchema, string aUser, string aPass)
        {

            Encoding myEncoding;

            using (var r = new StreamReader(aFileName, detectEncodingFromByteOrderMarks: true))
            {
                myEncoding = r.CurrentEncoding;
            }


            theParser = new Microsoft.VisualBasic.FileIO.TextFieldParser(aFileName, myEncoding);

            theParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
            theParser.SetDelimiters(aDelimiter);

            theFileFields = theParser.ReadFields();

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