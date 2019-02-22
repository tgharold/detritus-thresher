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

        public void Dispose()
        {
            connection.Dispose();
        }

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