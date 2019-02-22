using System;
using System.Data.Common;
using DetritusThresher.Core.Models;
using DetritusThresher.Core.Tests.Xunit;
using NPoco;
using Xunit;

namespace DetritusThresher.Core.Tests.Models
{
    [Collection(CollectionFixtureNames.Sqlite)]
    public class FileScanTests
    {
        private readonly DbConnection connection;

        public FileScanTests(SqliteFixture fixture)
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
            var folderName = $"test folderName: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var folderUri = $"test folderUri: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var fileName = $"test fileName: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var fileUri = $"test fileUri: {nameof(CanCreateAndSave)} {DateTimeOffset.UtcNow.Ticks}";

            var scan = ModelBuilders.CreateScan(scanName);
            var folder = ModelBuilders.CreateFolderScan(folderName, folderUri);
            var file = ModelBuilders.CreateFileScan(fileName, fileUri);
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(scan);
                folder.ScanId = scan.Id;
                db.Insert(folder);
                file.ScanId = scan.Id;
                file.ParentFolderId = folder.Id;
                db.Insert(file);
                var result = db.SingleById<FileScan>(file.Id);
                Assert.Equal(fileName, result.Name);
            }
        }
    }
}