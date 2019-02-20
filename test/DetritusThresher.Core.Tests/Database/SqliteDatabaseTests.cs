using System;
using System.Data.Common;
using DetritusThresher.Core.Tests.Xunit;
using Xunit;

namespace DetritusThresher.Core.Tests.Database
{
    [Collection(nameof(SqliteCollection))]
    public class SqliteDatabaseTests : IDisposable
    {
        private readonly DbConnection _db;

        public SqliteDatabaseTests(SqliteFixture fixture)
        {
            _db = fixture?.GetConnection();
            _db.Open();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        [Fact]
        public void CanReadVersionTable()
        {
            var cmd = _db.CreateCommand();
            string sqlStatement = "SELECT Description FROM VersionInfo";
            cmd.CommandText = sqlStatement;
            var result = cmd.ExecuteScalar().ToString();
            Assert.NotNull(result);
        }
    }
}