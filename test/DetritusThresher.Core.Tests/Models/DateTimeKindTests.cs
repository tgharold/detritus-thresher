using DetritusThresher.Core.Models;
using System;
using Xunit;

namespace DetritusThresher.Core.Tests.Models
{
    public class DateTimeKindTests
    {
        [Fact]
        public void FileScan_ScanCreated_defaults_to_correct_kind()
        {
            var x = new FileScan();
            Assert.Equal(DateTimeKind.Utc, x.ScanCreated.Kind);
        }

        [Fact]
        public void FolderScan_ScanCreated_defaults_to_correct_kind()
        {
            var x = new FolderScan();
            Assert.Equal(DateTimeKind.Utc, x.ScanCreated.Kind);
        }

        [Fact]
        public void Scan_ScanCreated_defaults_to_correct_kind()
        {
            var x = new Scan();
            Assert.Equal(DateTimeKind.Utc, x.ScanCreated.Kind);
        }
    }
}