using System;
using System.Collections.Generic;

namespace SimpleBackup
{
    /// <summary>
    /// Contains all the application settings that are saved/loaded
    /// </summary>
    [Serializable]
    public class SettingsData
    {
        public List<string> sourcePaths { get; set; } = new List<string>();
        public List<string> destinationPaths { get; set; } = new List<string>();
        public int numberOfConcurrentBackups { get; set; } = 1;
        public bool transferUnchangedFiles { get; set; } = false;
        public string backupName { get; set; } = "SimpleBackup";
        public bool useMD5ForTransfer { get; set; } = true;
        public bool useMD5ForComparison { get; set; } = false;
        public bool checkForUpdateOnLaunch { get; set; } = true;
        public bool writeToLog { get; set; } = true;
        public bool scheduleAutoClose { get; set; } = true;
        public bool useEmail { get; set; } = false;
        public int emailSendMode { get; set; } = 0;
        public List<BackupFilter> backupFilters { get; set; } = new List<BackupFilter>();

        //encrypted variables
        public string emailServer { get; set; } = "";
        public string emailServerPort { get; set; } = "";
        public string emailUserName { get; set; } = "";
        public string emailPassword { get; set; } = "";
        public string emailSender { get; set; } = "";
        public string emailReceiver { get; set; } = "";
        public string emailUseSSL { get; set; } = "";
    }
}