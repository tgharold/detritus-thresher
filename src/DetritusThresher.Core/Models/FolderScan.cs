using System;
using NPoco;

namespace DetritusThresher.Core.Models
{
    [TableName("FolderScans"), PrimaryKey("FolderScanId")]
    public class FolderScan
    {
        public long FolderScanId { get; set; }
        public long? ParentId { get; set; }
        public int ScanId { get; set; }

        public string Uri { get; set; }
        public string Name { get; set; }

        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }

        public DateTime ScanCreated { get; set; } = DateTime.UtcNow;
        public DateTime? ScanFinished { get; set; }
    }
}