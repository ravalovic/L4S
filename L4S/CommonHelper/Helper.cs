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
        /// <param name="patterns"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public static bool IsValidByReg(string[] patterns, string line)
        {
            bool result = false;
            foreach (var p in patterns)
            {
                var regex = new Regex(p);
                result = regex.IsMatch(line);
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
        /// <param name="file"></param>
        /// <param name="destDir"></param>
        /// <param name="destExt"></param>
        public static void ManageFile(Action action, string file, string destDir = "", string destExt = "")
        {
            string dateMask = DateTime.Now.ToString("ddMMyyyyHHmmss");
            // if destination exist no action performed
            if ((File.Exists(file) && !File.Exists(destDir + Path.GetFileName(file) + destExt)) || action == Action.Delete)
            {
                switch (action)
                {
                    case Action.Move:
                        File.Move(file, destDir + Path.GetFileName(file) + destExt);
                        break;
                    case Action.Copy:
                        File.Copy(file, destDir + Path.GetFileName(file), true);
                        break;
                    case Action.Delete:
                        File.Delete(file);
                        break;
                    case Action.Zip:
                        var zipName = destDir + Path.GetFileName(file) + "_" + dateMask + ".zip";
                        using (ZipArchive arch = ZipFile.Open(zipName, ZipArchiveMode.Create))
                        {
                            arch.CreateEntryFromFile(file, Path.GetFileName(file), CompressionLevel.Optimal);
                        }
                        break;
                }
            }
        }
    }
}
