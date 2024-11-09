using System;

namespace SimpleBackup
{
    [Serializable]
    public enum BackupFilterPathType
    {
        FILE_NAME,
        FILE_EXTENSION,
        FILE_NAME_AND_EXTENSION,
        FOLDER_NAME,
        FULL_PATH
    }
}