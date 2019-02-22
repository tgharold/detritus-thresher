using System;
using System.Data.Common;
using DetritusThresher.Core.Models;
using DetritusThresher.Core.Tests.Xunit;
using NPoco;
using Xunit;

namespace DetritusThresher.Core.Tests.Models
{
    [Collection(CollectionFixtureNames.Sqlite)]
    public class ScanTests
    {
        private readonly DbConnection connection;

        public ScanTests(SqliteFixture fixture)
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

        private Scan CreateScan(
            string name
            )
        {
            return new Scan
            {
                Name = name
            };
        }

        [Fact]
        public void CanCreateAndSave()
        {
            var name = $"test name: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var scan = CreateScan(name);
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(scan);
                var result = db.SingleById<Scan>(scan.Id);
                Assert.Equal(name, result.Name);
            }
        }

        [Fact]
        public void CanGetBackKindUtcFromCreated()
        {
            // Microsoft.Data.Sqlite has problems storing/retrieving DateTime values as kind UTC
            // https://system.data.sqlite.org/ -- works fine

            var name = $"test message: {nameof(CanGetBackKindUtcFromCreated)} {DateTimeOffset.UtcNow.Ticks}";
            var scan = CreateScan(name);
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(scan);
                var result = db.SingleById<Scan>(scan.Id);
                Assert.Equal(DateTimeKind.Utc, result.ScanCreated.Kind);
            }
        }
    }
}