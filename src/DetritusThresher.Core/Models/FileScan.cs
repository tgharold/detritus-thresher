using System;
using NPoco;

namespace DetritusThresher.Core.Models
{
    [TableName("FileScans")]
    public class FileScan
    {
        public long Id { get; set; }
        public long FolderScanId { get; set; }
        public int ScanId { get; set; }

        public string Uri { get; set; }
        public string Name { get; set; }

        public long Bytes { get; set; }

        public DateTimeOffset? Created { get; set; }
        public DateTimeOffset? Modified { get; set; }

        public string BeginningHashSha256 { get; set; }
        public string CompleteHashSha256 { get; set; }

        public DateTimeOffset ScanCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ScanFinished { get; set; }
    }
}