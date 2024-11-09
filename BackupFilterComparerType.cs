using System;

namespace SimpleBackup
{
    [Serializable]
    public enum BackupFilterComparerType
    {
        EQUALS,
        DOES_NOT_EQUAL,
        CONTAINS,
        DOES_NOT_CONTAIN
    }
}