namespace SimpleBackup
{
    /// <summary>
    /// Contains all the data required to transfer a directory
    /// </summary>
    public struct TransferDirectory
    {
        public BackupDirectory srcDirectory;
        public string[] dstPathArray;
        public BackupComparison[] backupComparisonArray;
        public string[] backupNameArray;

        public TransferDirectory(BackupDirectory srcDirectory, string[] dstPathArray, BackupComparison[] backupComparisonArray, string[] backupNameArray)
        {
            this.srcDirectory = srcDirectory;
            this.dstPathArray = dstPathArray;
            this.backupComparisonArray = backupComparisonArray;
            this.backupNameArray = backupNameArray;
        }
    }
}