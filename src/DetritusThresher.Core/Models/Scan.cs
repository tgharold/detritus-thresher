using System;
using NPoco;

namespace DetritusThresher.Core.Models
{
    [TableName("Scans"), PrimaryKey("ScanId")]
    public class Scan
    {
        public int ScanId { get; set; }

        public string Name { get; set; }
        
        public DateTime ScanCreated { get; set; } = DateTime.UtcNow;
        public DateTime? ScanFinished { get; set; }

        public int? StartingFolderId { get; set; }
    }
}