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

        [Fact]
        public void CanCreateAndSave()
        {
            var testMessage = $"test message: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var logEntry = new LogEntry
            {
                Severity = (int)LogSeverity.Debug,
                SeverityName = LogSeverity.Debug.ToString(),
                Category = LogCategory.System,
                Message = testMessage
            };
            Assert.NotNull(logEntry);   
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(logEntry);

                var result = db.SingleById<LogEntry>(logEntry.Id);
                Assert.Equal(testMessage, result.Message);

                Assert.Equal(DateTimeKind.Utc, result.Created.Value.Kind);
            }
        }
    }
}