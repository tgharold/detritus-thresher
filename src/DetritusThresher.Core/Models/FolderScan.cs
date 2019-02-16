using System;
using NPoco;
using SQLite.Net.DateTimeOffset.Attributes;

namespace DetritusThresher.Core.Models
{
    [TableName("FolderScans")]
    public class FolderScan
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public int ScanId { get; set; }

        public string Uri { get; set; }
        public string Name { get; set; }

        [DateTimeOffsetSerialize]
        public DateTimeOffset? Created { get; set; }
        [DateTimeOffsetSerialize]
        public DateTimeOffset? Modified { get; set; }

        [DateTimeOffsetSerialize]
        public DateTimeOffset ScanCreated { get; set; } = DateTimeOffset.UtcNow;
        [DateTimeOffsetSerialize]
        public DateTimeOffset? ScanFinished { get; set; }
    }
}