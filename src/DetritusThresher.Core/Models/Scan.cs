using System;
using NPoco;

namespace Hunter2.Core.Model
{
    [TableName("Scans")]
    public class Scan
    {
        public int Id { get; set; }
        
        public FolderScan StartingFolder { get; set; }

        public DateTimeOffset ScanCreated { get; set; } = DateTimeOffset.UtcNow;
    }
}