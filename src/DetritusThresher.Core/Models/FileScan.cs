using SQLite.Net.DateTimeOffset.Attributes;
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
        [DateTimeOffsetSerialize]
        public DateTimeOffset? Created { get; set; }
        [DateTimeOffsetSerialize]
        public DateTimeOffset? Modified { get; set; }

        public string BeginningHashSha256 { get; set; }
        public string CompleteHashSha256 { get; set; }

        [DateTimeOffsetSerialize]
        public DateTimeOffset ScanCreated { get; set; } = DateTimeOffset.UtcNow;
        [DateTimeOffsetSerialize]
        public DateTimeOffset? ScanFinished { get; set; }
    }
}