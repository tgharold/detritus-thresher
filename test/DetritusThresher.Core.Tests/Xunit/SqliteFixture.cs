using System;
using DetritusThresher.Core.Database;
using System.Data.Common;
using System.Data.SQLite;

namespace DetritusThresher.Core.Tests.Xunit
{
    public class SqliteFixture : IDisposable
    {
        private readonly SqliteDatabase _database;

        public SqliteFixture()
        {
            var dbName = $"mem-{Guid.NewGuid().ToString()}";
            
            var csb = new SQLiteConnectionStringBuilder();
            //csb.FullUri = $"file:{dbName}?mode=memory&cache=shared";
            csb.DataSource = $"{dbName}?mode=memory&cache=shared";
            csb.DateTimeKind = DateTimeKind.Utc;
            var connString2 = csb.ConnectionString;
            var connString = $"Data Source=file:{dbName}?mode=memory&cache=shared;datetimekind=Utc";

            // ConnectionStringBuilder is creating:
            // "fulluri=\"file:mem-e60d7795-e60f-4ac3-8c0c-26294b1c436b?mode=memory&cache=shared\";datetimekind=Utc"
            // versuss
            // "Data Source=file:mem-727643e8-a54a-418e-b698-a0f4a85e54ee?mode=memory&cache=shared"

            _database = new SqliteDatabase(connString);
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