using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SimpleBackup
{
    /// <summary>
    /// Used for saving and reading settings to/from an external settings file
    /// </summary>
    public static class Settings
    {
        public static SettingsData data = new SettingsData();
        private static readonly string fileName = "settings.txt";

        public static void Save()
        {
            try
            {
                string directory = GetDirectory();
                if (directory != null)
                {
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    string fileData = JsonConvert.SerializeObject(data);
                    File.WriteAllText(Path.Combine(directory, fileName), fileData);
                }
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
            }
        }

        public static void Load()
        {
            try
            {
                string directory = GetDirectory();
                if (directory != null && File.Exists(Path.Combine(directory, fileName)))
                {
                    string fileData = File.ReadAllText(Path.Combine(directory, fileName));
                    data = JsonConvert.DeserializeObject<SettingsData>(fileData);
                }
            }
            catch (Exception e)
            {
                Log.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Encrypts any input value
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="value">The value to be encrypted</param>
        /// <returns>The value encrypted to a string</returns>
        public static string Encrypt<T>(T value)
        {
            string text = value.ToString();
            if (!string.IsNullOrEmpty(text))
            {
                return Convert.ToBase64String(ProtectedData.Protect(Encoding.Unicode.GetBytes(text), null, DataProtectionScope.CurrentUser));
            }
            return null;
        }

        /// <summary>
        /// Decrypts the encrypted input value as an integer
        /// </summary>
        /// <param name="encryptedText">The encrypted value</param>
        /// <returns>The input value decrypted to an integer</returns>
        public static int DecryptInt(string encryptedText)
        {
            string returnText = DecryptString(encryptedText);
            if (!string.IsNullOrEmpty(returnText))
            {
                return int.Parse(returnText);
            }
            return -1;
        }

        /// <summary>
        /// Decrypts the encrypted input value as a boolean
        /// </summary>
        /// <param name="encryptedText">The encrypted value</param>
        /// <returns>The input value decrypted to a boolean</returns>
        public static bool DecryptBool(string encryptedText)
        {
            string returnText = DecryptString(encryptedText);
            if (!string.IsNullOrEmpty(returnText))
            {
                return bool.Parse(returnText);
            }
            return false;
        }

        /// <summary>
        /// Decrypts the encrypted input value as a string
        /// </summary>
        /// <param name="encryptedText">The encrypted value</param>
        /// <returns>The input value decrypted to a string</returns>
        public static string DecryptString(string encryptedText)
        {
            if (!string.IsNullOrEmpty(encryptedText))
            {
                return Encoding.Unicode.GetString(ProtectedData.Unprotect(Convert.FromBase64String(encryptedText), null, DataProtectionScope.CurrentUser));
            }
            return "";
        }

        public static string GetDirectory()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SimpleBackup");
        }
    }
}