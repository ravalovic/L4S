using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


namespace SQLBulkCopy
{
    class CsvParser
    {
        //string theData;
        StreamReader theReader;
        public CsvParser(string aFileName)
        {
            theReader = new StreamReader(aFileName, detectEncodingFromByteOrderMarks: true);
        }
        public bool Read()
        {
            if (theReader.Peek() >= 0) { return true; }
            else { return false; }
        }
        public string[] ReadFields(string aDelimiter)
        {
            string myLine =
                theReader.ReadLine();
            //Debug.Assert(myLine != null, "myLine != null");
            int l = myLine.Length;
            List<string> myRow = new List<string>();
            int i = 0;
            while (i < l)
            {
                //quoted
                if (myLine[i] == '"')
                {
                    // skip quote
                    i++;
                    int start = i;

                    while (true)
                    {
                        if (i == l) { break; }

                        if (myLine[i] == '"')
                        {
                            //if (i == start) { break; }
                            if (myLine[i + 1] != '"' & myLine[i - 1] != '"')
                            { break; }
                        }
                        i++;

                    }
                    if (i == start)
                    { myRow.Add(""); }
                    else
                    {
                        myRow.Add(myLine.Substring(start, i - start).Replace("\"\"", "\""));
                    }
                    i++;
                }
                else
                {
                    int start = i;
                    while (i < l && myLine[i] != aDelimiter[0])
                        i++;
                    //add the value
                    myRow.Add(myLine.Substring(start, i - start));
                }
                i++;
            }

            return myRow.ToArray();

        }
    }
}