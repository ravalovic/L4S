using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;


namespace CommonHelper
{
    public static class Helper
    {
        public static string CalculateCheckSum(string myFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(myFile))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
        public static int CountFileLines(string myFile)
        {
            return File.ReadLines(myFile).Count();
        }

        /// <summary>
        /// Check if line is match by pattern
        /// </summary>
        /// <param name="myPatterns"></param>
        /// <param name="myLine"></param>
        /// <returns></returns>
        public static bool IsValidByReg(string[] myPatterns, string myLine)
        {
            bool result = false;
            foreach (var p in myPatterns)
            {
                var regex = new Regex(p);
                result = regex.IsMatch(myLine);
                if (result) break;
            }
            return result;
        }

        public enum Action
        {
            Copy,
            Move,
            Delete,
            Zip
        };

        /// <summary>
        /// Move, copy file to specified dir or delete file
        /// </summary>
        ///  <param name="action"></param>
        /// <param name="myFile"></param>
        /// <param name="myDestDir"></param>
        /// <param name="myDestExt"></param>
        public static void ManageFile(Action action, string myFile, string myDestDir = "", string myDestExt = "")
        {
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            // if destination exist no action performed
            if ((File.Exists(myFile) && !File.Exists(myDestDir + Path.GetFileName(myFile) + myDestExt)) || action == Action.Delete)
            {
                switch (action)
                {
                    case Action.Move:
                        File.Move(myFile, myDestDir + Path.GetFileName(myFile) + myDestExt);
                        break;
                    case Action.Copy:
                        File.Copy(myFile, myDestDir + Path.GetFileName(myFile), true);
                        break;
                    case Action.Delete:
                        File.Delete(myFile);
                        break;
                    case Action.Zip:
                        var zipName = myDestDir + Path.GetFileName(myFile) + "_" + dateMask + ".zip";
                        using (ZipArchive arch = ZipFile.Open(zipName, ZipArchiveMode.Create))
                        {
                            arch.CreateEntryFromFile(myFile, Path.GetFileName(myFile), CompressionLevel.Optimal);
                            arch.Dispose();
                        }
                        
                        break;
                }
            }
        }

        public static int GetBatchIdFromName(string myFile)
        {
            var fileName = Path.GetFileName(myFile);
           
            if (fileName != null)
            {
                var batchId = fileName.Split('_');
                int bId;
                if (Int32.TryParse(batchId[1], out bId))
                { 
                return bId;
                }
            }
            return 0;
        } 
    }
}
