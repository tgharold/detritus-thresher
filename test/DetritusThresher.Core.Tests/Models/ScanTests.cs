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



        [Fact]
        public void CanCreateAndSave()
        {
            var name = $"test name: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var scan = ModelBuilders.CreateScan(name);
        
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
            // Microsoft.Data.Sqlite (as of 2.2.2) has problems storing/retrieving DateTime values as kind UTC
            // https://system.data.sqlite.org/ -- works fine and returns UTC timestamp

            // Update: NPoco/System.Data.SQLite seems to have trouble round-tripping UTC time to the database
            // The fix for this is adding "datetimekind=Utc" to the connetion string
            // "data source=\"file:mem-073d7ec1-528a-4482-8264-caf718d4fc06?mode=memory&cache=shared\";datetimekind=Utc"

            var name = $"test message: {nameof(CanGetBackKindUtcFromCreated)} {DateTimeOffset.UtcNow.Ticks}";
            var scan = ModelBuilders.CreateScan(name);
            scan.ScanFinished = new DateTime(2018, 12, 13, 22, 55, 43, DateTimeKind.Utc);
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(scan);
                var result = db.SingleById<Scan>(scan.Id);
                Assert.Equal(DateTimeKind.Utc, result.ScanFinished?.Kind);
                Assert.Equal(scan.ScanFinished, result.ScanFinished);
            }
        }
    }
}