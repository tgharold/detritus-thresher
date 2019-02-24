using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using NPoco;

namespace DetritusThresher.Core.Database
{
    public class SqliteDatabase : IDisposable
    {
        /// <summary>
        /// Sqlite will close/delete memory/temporary databases when the last connection
        /// is closed. We use a hidden connection variable to keep the database
        /// intact until we no longer need it.
        /// </summary>
        private SQLiteConnection _holdOpenConnection;

        // Sqlite in-memory: https://www.sqlite.org/inmemorydb.html
        // Use a "named" memory string with shared cache.

        public SqliteDatabase(string connectionString)
        {
            _holdOpenConnection = new SQLiteConnection(connectionString);
            _holdOpenConnection.Open();
        }

        public enum DatabaseType
        {
            Memory,
            Temporary,
            File
        }

        public SqliteDatabase(
            string dbName,
            DatabaseType databaseType
            )
        {
            var csb = new SQLiteConnectionStringBuilder();

            switch (databaseType)
            {
                case DatabaseType.Memory: 
                    csb.FullUri = $"file:{dbName}?mode=memory&cache=shared";
                    break;
                
                default:
                    csb.DataSource = dbName;
                    break;
            }
            
            csb.DateTimeKind = DateTimeKind.Utc;
            var connectionString = csb.ConnectionString;

            _holdOpenConnection = new SQLiteConnection(connectionString);
            _holdOpenConnection.Open();
        }

        public string GetConnectionString()
        {
            return _holdOpenConnection.ConnectionString;
        }

        public DbConnection GetConnection()
        {
            var connectionString = _holdOpenConnection.ConnectionString;
            return new SQLiteConnection(connectionString);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _holdOpenConnection.Close();
                    _holdOpenConnection.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}