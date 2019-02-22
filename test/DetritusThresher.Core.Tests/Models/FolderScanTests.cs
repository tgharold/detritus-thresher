using System;
using System.Data.Common;
using DetritusThresher.Core.Models;
using DetritusThresher.Core.Tests.Xunit;
using NPoco;
using Xunit;

namespace DetritusThresher.Core.Tests.Models
{
    [Collection(CollectionFixtureNames.Sqlite)]
    public class FolderScanTests
    {
        private readonly DbConnection connection;

        public FolderScanTests(SqliteFixture fixture)
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
            var scanName = $"test scanName: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var name = $"test name: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var uri = $"test URI: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";

            var scan = ModelBuilders.CreateScan(scanName);
            var item = ModelBuilders.CreateFolderScan(name, uri);
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(scan);
                item.ScanId = scan.Id;
                db.Insert(item);
                var result = db.SingleById<FolderScan>(item.Id);
                Assert.Equal(name, result.Name);
            }
        }
    }
}