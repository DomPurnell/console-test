using System;

namespace FileData
{
    [Flags]
    public enum SearchTypeEnum
    {
        NONE = 1,
        SIZE = 2,
        VERSION = 4
    }
}
