using System.Collections.Generic;

namespace SimpleBackup
{
    /// <summary>
    /// Stores which files are to be ignored when backing up, using a single linked list structure
    /// </summary>
    public struct BackupComparison
    {
        public List<BackupComparison> subDirectoryList;
        public bool[] filesToIgnoreArray;

        public BackupComparison(List<BackupComparison> subDirectoryList, bool[] filesToIgnoreArray)
        {
            this.subDirectoryList = subDirectoryList;
            this.filesToIgnoreArray = filesToIgnoreArray;
        }
    }
}