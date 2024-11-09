using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleBackup
{
    /// <summary>
    /// Used for logging text to the console and external log file, as well as reading the external log file. Also holds any accumulated errors until cleared
    /// </summary>
    public static class Log
    {
        private static readonly string fileName = "log.txt";
        private static readonly int maxLogFileSizeKB = 30;
        public static event EventHandler OnLogUpdated;
        private static List<string> sessionErrors = new List<string>();

        public static void WriteLine(string text, bool isError = false, bool addTimeStamp = true)
        {
            string directory = GetDirectory();
            if (directory != null)
            {
                try
                {
                    List<string> lines = new List<string>();
                    string fullPath = Path.Combine(directory, fileName);

                    if (isError)
                    {
                        sessionErrors.Add("ERROR: " + text);
                    }

                    if (addTimeStamp)
                    {
                        text = "[" + DateTime.Now.ToString() + "] " + text;
                    }

                    Console.WriteLine(text);

                    if (Settings.data.writeToLog)
                    {
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        if (File.Exists(fullPath))
                        {
                            lines = File.ReadAllLines(fullPath).ToList();
                        }

                        lines.Insert(0, text);

                        if (maxLogFileSizeKB > 0)
                        {
                            while (GetSizeInKiloBytes(ref lines) > maxLogFileSizeKB)
                            {
                                lines.RemoveAt(lines.Count - 1);
                            }
                        }

                        File.WriteAllLines(fullPath, lines);
                    }

                    OnLogUpdated?.Invoke(null, new OnLogUpdatedEventArgs(text, isError));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static float GetSizeInKiloBytes(ref List<string> list)
        {
            float size = 0;
            foreach (string line in list)
            {
                size += ASCIIEncoding.UTF8.GetByteCount(line);
            }
            if (size > 0)
            {
                size /= 1000f;
            }
            return size;
        }

        public static string ReadLogFile()
        {
            string logFileText = "";
            try
            {
                string directory = GetDirectory();
                if (directory != null)
                {
                    string fullPath = Path.Combine(directory, fileName);

                    if (File.Exists(fullPath))
                    {
                        logFileText = File.ReadAllText(fullPath);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return logFileText;
        }

        public static List<string> GetSessionErrors()
        {
            return sessionErrors;
        }

        public static string GetSessionErrorsString()
        {
            string errorString = "";
            if (sessionErrors.Count > 0)
            {
                errorString = sessionErrors[0];
                for (int i = 1; i < sessionErrors.Count; i++)
                {
                    errorString += Environment.NewLine + sessionErrors[i];
                }
            }
            return errorString;
        }

        public static void ClearSessionErrors()
        {
            sessionErrors.Clear();
        }

        public static void ClearLog()
        {
            string directory = GetDirectory();
            if (directory != null)
            {
                string fullPath = Path.Combine(directory, fileName);
                if (File.Exists(fullPath))
                {
                    File.WriteAllText(fullPath, "");
                }
            }
        }

        public static string GetDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SimpleBackup");
        }
    }

    public class OnLogUpdatedEventArgs : EventArgs
    {
        public string text { get; set; }
        public bool isError { get; set; }

        public OnLogUpdatedEventArgs(string text, bool isError)
        {
            this.text = text;
            this.isError = isError;
        }
    }
}
