using System;
using SQLite.Net.DateTimeOffset.Attributes;

namespace DetritusThresher.Core.Models
{
    public class Log
    {
        public long Id { get; set; }

        [DateTimeOffsetSerialize]
        public DateTimeOffset? Created { get; set; } = DateTimeOffset.UtcNow;

        public int Severity { get; set; }
        public string SeverityName { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }
    }
}