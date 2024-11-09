using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleBackup
{
    /// <summary>
    /// Stores file info
    /// </summary>
    public struct BackupFile
    {
        public string name;
        public ulong fileSize;
        public DateTime lastModified;

        public BackupFile(string name, ulong fileSize, DateTime lastModified)
        {
            this.name = name;
            this.fileSize = fileSize;
            this.lastModified = lastModified;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType() == typeof(BackupFile))
            {
                BackupFile fileData = (BackupFile)obj;
                if (Path.GetFileName(fileData.name) == Path.GetFileName(name) && fileData.fileSize == fileSize && fileData.lastModified == lastModified)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = -1200454722;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + fileSize.GetHashCode();
            hashCode = hashCode * -1521134295 + lastModified.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "Name: " + name + ", File Size:" + fileSize + ", Last Modified: " + lastModified;
        }
    }
}