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

    }
}