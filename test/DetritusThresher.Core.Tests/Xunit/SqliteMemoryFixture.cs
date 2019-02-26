using System;
using DetritusThresher.Core.Database;
using System.Data.Common;

namespace DetritusThresher.Core.Tests.Xunit
{
    public class SqliteMemoryFixture : IDisposable
    {
        private readonly SqliteDatabase _database;

        public SqliteMemoryFixture()
        {
            var dbName = $"mem-{Guid.NewGuid().ToString()}";
            _database = new SqliteDatabase(dbName, SqliteDatabaseType.Memory);
            var connString = _database.GetConnectionString();
            MigratorHelper.RunSqliteMigrations(connString);
        }

        public DbConnection GetConnection()
        {
            return _database.GetConnection();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}