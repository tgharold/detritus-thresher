using System;
using System.Data.Common;
using DetritusThresher.Core.Tests.Xunit;
using Xunit;

namespace DetritusThresher.Core.Tests.Database
{
    [Collection(CollectionFixtureNames.Sqlite)]
    public class SqliteDatabaseTests : IDisposable
    {
        private readonly DbConnection connection;

        public SqliteDatabaseTests(SqliteFixture fixture)
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
        public void CanReadVersionTable()
        {
            var cmd = connection.CreateCommand();
            string sqlStatement = "SELECT Description FROM VersionInfo";
            cmd.CommandText = sqlStatement;
            var result = cmd.ExecuteScalar().ToString();
            Assert.NotNull(result);
        }
    }
}