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
            Zip, 
            Unzip,
            Rename
        };

        public static string Version(string appName)
        {
            return appName+": "+@"Verzia 1.5.0 zo dňa 16.10.2017";
        }
        public enum ParameterFromName
        {
            BatchId,
            OriginalFileChecksum,
            OriginalFileName
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
                    case Action.Unzip:
                        try
                        {
                            ZipFile.ExtractToDirectory(myFile, myDestDir);
                            File.Delete(myFile);
                        }
                        catch (Exception e)
                        {
                            //destination file exist then delete archive
                            File.Delete(myFile);
                        }
                        
                        break;
                    case Action.Rename:
                        File.Move(myFile, myDestDir + myDestExt+ "_" + Path.GetFileName(myFile) );
                        break;
                }
            }
        }

        public static string GetFromName(string myFile, ParameterFromName myParamEnum)
        {
            //new FileInfo(configSettings.WorkDir + configSettings.OutputFileMask+"_"+ configSettings.BatchId + "_" + dateMask + "_" + oriCheckSum+ "_" + fname);
            var fileName = Path.GetFileName(myFile);

            if (fileName != null)
            {
                var retVal = string.Empty;
                var array = fileName.Split('_');
                switch (myParamEnum)
                {
                    case ParameterFromName.BatchId:
                        retVal = array[1];
                        break;
                    case ParameterFromName.OriginalFileChecksum:
                        retVal = array[3];
                        break;
                    case ParameterFromName.OriginalFileName:
                        if (array.Length > 4)
                        {
                            for (int i = 4; i < array.Length; i++)
                            {
                                if (i == 4)
                                {
                                    retVal = array[i];
                                }
                                else
                                {
                                    retVal = retVal + "_" + array[i];
                                }
                            }
                        }
                        else
                        {
                            retVal = array[4];
                        }
                        break;
                }
                return retVal;
            }
            return string.Empty;
        }

    }
}
