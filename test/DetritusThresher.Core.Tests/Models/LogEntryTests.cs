using System;
using System.Data.Common;
using DetritusThresher.Core.Constants;
using DetritusThresher.Core.Models;
using DetritusThresher.Core.Tests.Xunit;
using NPoco;
using Xunit;

namespace DetritusThresher.Core.Tests.Models
{
    [Collection(CollectionFixtureNames.Sqlite)]
    public class LogEntryTests
    {
        private readonly DbConnection connection;

        public LogEntryTests(SqliteFixture fixture)
        {
            connection = fixture.GetConnection();
            connection.Open();
        }

#pragma warning disable xUnit1013 // Public method should be marked as test
        public void Dispose()
        {
            connection.Dispose();
        }
#pragma warning restore xUnit1013 // Public method should be marked as test

        private LogEntry CreateLogEntry(
            string message, 
            LogSeverity logSeverity = LogSeverity.Debug,
            string logCategory = LogCategory.System
            )
        {
            return new LogEntry
            {
                Severity = (int)logSeverity,
                SeverityName = logSeverity.ToString(),
                Category = logCategory,
                Message = message
            };
        }

        [Fact]
        public void CanCreateAndSave()
        {
            var testMessage = $"test message: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var logEntry = CreateLogEntry(testMessage);
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(logEntry);
                var result = db.SingleById<LogEntry>(logEntry.Id);
                Assert.Equal(testMessage, result.Message);
            }
        }

        [Fact]
        public void CanGetBackKindUtcFromCreated()
        {
            var testMessage = $"test message: {nameof(CanGetBackKindUtcFromCreated)} {DateTimeOffset.UtcNow.Ticks}";
            var logEntry = CreateLogEntry(testMessage);
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(logEntry);
                var result = db.SingleById<LogEntry>(logEntry.Id);
                Assert.Equal(DateTimeKind.Utc, result.Created.Value.Kind);
            }
        }
    }
}