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

        [Fact]
        public void CanCreateMultiplesAndSave()
        {
            var scanName = $"test scanName: {nameof(CanCreateMultiplesAndSave)} {DateTimeOffset.UtcNow.Ticks}";
            var scan = ModelBuilders.CreateScan(scanName);

            string lastFolderName = null;
            long lastFileId = 0;
            string lastFileName = null;
        
            using (var db = new NPoco.Database(connection, DatabaseType.SQLite))
            {
                db.Insert(scan);

                for (var i = 0; i <= 9; i++)
                {
                    var folder = ModelBuilders.CreateFolderScan($"Folder-{i}-{Guid.NewGuid()}", string.Empty);
                    folder.ScanId = scan.Id;
                    db.Insert(folder);

                    lastFolderName = folder.Name;

                    for (var j = 0; j <= 9; j++)
                    {
                        var file = ModelBuilders.CreateFileScan($"File-{i}-{j}-{Guid.NewGuid()}", string.Empty);
                        file.ScanId = scan.Id;
                        file.ParentFolderId = folder.Id;
                        db.Insert(file);

                        lastFileId = file.Id;
                        lastFileName = file.Name;
                    }
                }

                var fileResult = db.SingleById<FileScan>(lastFileId);
                Assert.Equal(lastFileName, fileResult.Name);
                var folderResult = db.SingleById<FolderScan>(fileResult.ParentFolderId);
                Assert.Equal(lastFolderName, folderResult.Name);
            }
        }        
    }
}