using System;
using NPoco;

namespace DetritusThresher.Core.Models
{
    [TableName("Scans")]
    public class Scan
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public DateTime ScanCreated { get; set; } = DateTime.UtcNow;
        public DateTime? ScanFinished { get; set; }

        //public FolderScan StartingFolder { get; set; }
    }
}