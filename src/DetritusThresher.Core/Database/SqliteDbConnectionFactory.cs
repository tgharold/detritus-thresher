using System;
using System.Data.Common;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using NPoco;

namespace DetritusThresher.Core.Database
{
    public class SqliteDbConnectionFactory
    {
        private DatabaseType DbType { get; set; }
        private string ConnectionString { get; set; }
        private string ProviderName { get; set; }

        protected static readonly object _syncRoot = new object();

        public SqliteDbConnectionFactory(string connectionString)
        {
            DbType = DatabaseType.SQLite;
            ConnectionString = connectionString;
            ProviderName = DatabaseType.SQLite.GetProviderName();
        }

        public DbConnection GetConnection()
        {
            return new SqliteConnection(ConnectionString);
        }
    }
}