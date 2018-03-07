using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Importer.ImporterFolder
{
    public static class OperationLogger
    {
        public static string loggerFilePath;
        private static string FilePath(string folderName, string fileName)
        {
            string dateTimeUtcNowDateToString = DateTime.Now.ToString("yyyyMMdd");
            return String.Format("{0}/{1}-{2}{3}", folderName, fileName, dateTimeUtcNowDateToString, ".txt");
        }

        private const string STR_OPERATION_TYPE = "Rename Asset ID";
        private static void WriteWithTime(string log, string fileName)
        {
            //string folderName = DateTime.UtcNow.Date.ToString("yyyyMMdd");
            string folderName = DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            if (!File.Exists(FilePath(folderName, fileName)))
            {
                // Create a file to write to.
                loggerFilePath = FilePath(folderName, fileName);
                using (StreamWriter sw = File.CreateText(loggerFilePath))
                {
                    sw.WriteLine(log);
                }
            }
            else
            {
                // This text is always added, making the file longer over time
                // if it is not deleted.
                loggerFilePath = FilePath(folderName, fileName);
                using (StreamWriter sw = File.AppendText(loggerFilePath))
                {
                    sw.WriteLine(log);
                }
            }

        }

        public static void WriteLogToResult(string log, string fileName = STR_OPERATION_TYPE)
        {
            WriteWithTime(String.Format("{0}--{1}", DateTime.Now.ToString("yyyyMMdd-hh:mm:ss"), log), fileName);
        }
    }

}
