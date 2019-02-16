using System;
using NPoco;

namespace DetritusThresher.Core.Models
{
    [TableName("Log")]
    public class LogEntry
    {
        public long Id { get; set; }

        public DateTime? Created { get; set; } = DateTime.UtcNow;

        public int Severity { get; set; }
        public string SeverityName { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
    }
}