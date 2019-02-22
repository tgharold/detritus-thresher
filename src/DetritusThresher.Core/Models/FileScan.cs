using System;
using NPoco;

namespace DetritusThresher.Core.Models
{
    [TableName("FileScans"), PrimaryKey("FileScanId")]
    public class FileScan
    {
        public long FileScanId { get; set; }
        public long FolderScanId { get; set; }
        public int ScanId { get; set; }

        public string Uri { get; set; }
        public string Name { get; set; }

        public long Bytes { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public string BeginningHashSha256 { get; set; }
        public string CompleteHashSha256 { get; set; }

        public DateTime ScanCreated { get; set; } = DateTime.UtcNow;
        public DateTime? ScanFinished { get; set; }
    }
}