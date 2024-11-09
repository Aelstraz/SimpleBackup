namespace SimpleBackup
{
    /// <summary>
    /// Contains all the data required to transfer an individual file
    /// </summary>
    public struct TransferFile
    {
        public BackupFile backupFile;
        public string[] dstPathArray;
        public string[] backupNameArray;

        public TransferFile(BackupFile backupFile, string[] dstPathArray, string[] backupNameArray)
        {
            this.backupFile = backupFile;
            this.dstPathArray = dstPathArray;
            this.backupNameArray = backupNameArray;
        }
    }
}
