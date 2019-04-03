using System;

namespace FileInfoAPI
{
    public class FileInfo
    {
        public string ErrorMessage { get; set; }
        public DateTimeOffset Modified { get; set; }
        public DateTimeOffset Created { get; set; }
        public int Size { get; set; }
        public string Version { get; set; }
    }
}
