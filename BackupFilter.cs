using System;

namespace SimpleBackup
{
    /// <summary>
    /// Contains user defined values for filtering a file/folder by its path
    /// </summary>
    [Serializable]
    public struct BackupFilter
    {
        public BackupFilterIgnoreType backupFilterIgnoreType;
        public BackupFilterPathType backupFilterPathType;
        public BackupFilterComparerType backupFilterComparerType;
        public bool ignoreCase;
        public string value;

        public BackupFilter(BackupFilterIgnoreType backupFilterIgnoreType, BackupFilterPathType backupFilterPathType, BackupFilterComparerType backupFilterComparerType, bool ignoreCase, string value)
        {
            this.backupFilterIgnoreType = backupFilterIgnoreType;
            this.backupFilterPathType = backupFilterPathType;
            this.backupFilterComparerType = backupFilterComparerType;
            this.ignoreCase = ignoreCase;
            this.value = value;
        }
    }
}