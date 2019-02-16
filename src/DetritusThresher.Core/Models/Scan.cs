using System;
using NPoco;

namespace DetritusThresher.Core.Models
{
    [TableName("Scans")]
    public class Scan
    {
        public int Id { get; set; }
        
        public FolderScan StartingFolder { get; set; }

        public DateTimeOffset ScanCreated { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ScanFinished { get; set; }
    }
}