using System;
using NPoco;
using SQLite.Net.DateTimeOffset.Attributes;

namespace DetritusThresher.Core.Models
{
    [TableName("Scans")]
    public class Scan
    {
        public int Id { get; set; }
        
        public FolderScan StartingFolder { get; set; }

        [DateTimeOffsetSerialize]
        public DateTimeOffset ScanCreated { get; set; } = DateTimeOffset.UtcNow;
        [DateTimeOffsetSerialize]
        public DateTimeOffset? ScanFinished { get; set; }
    }
}