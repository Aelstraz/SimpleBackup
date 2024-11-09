using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleBackup
{
    /// <summary>
    /// Stores which files and sub-directories are contained within a directory, using a single linked list structure
    /// </summary>
    public struct BackupDirectory
    {
        public string path;
        public List<BackupDirectory> subDirectoryList;
        public List<BackupFile> backupFileList;

        public BackupDirectory(string path, List<BackupDirectory> subDirectoryList, List<BackupFile> backupFileList)
        {
            this.path = path;
            this.subDirectoryList = subDirectoryList;
            this.backupFileList = backupFileList;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[" + Path.GetFileName(path) + "]");

            foreach (BackupDirectory subDirectory in subDirectoryList)
            {
                sb.AppendLine("--->[" + Path.GetFileName(subDirectory.path) + "]");
            }
            foreach (BackupFile file in backupFileList)
            {
                if (file.name != null)
                {
                    sb.AppendLine("-" + file.name);
                }
            }
            foreach (BackupDirectory subDirectory in subDirectoryList)
            {
                sb.Append(subDirectory.ToString());
            }
            return sb.ToString();
        }
    }
}
