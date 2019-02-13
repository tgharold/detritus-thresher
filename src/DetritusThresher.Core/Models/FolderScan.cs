using System;
using NPoco;

namespace Hunter2.Core.Model
{
    [TableName("FolderScans")]
    public class FolderScan
    {
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public int ScanId { get; set; }

        public string Uri { get; set; }
        public string Name { get; set; }

        public DateTimeOffset ScanCreated { get; set; } = DateTimeOffset.UtcNow;
    }
}