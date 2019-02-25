using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using NPoco;
using static DetritusThresher.Core.Database.SqliteDatabaseType;

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

        public SqliteDatabase(
            string dbName,
            SqliteDatabaseType databaseType
            )
        {

            string connectionString;
            switch (databaseType)
            {
                case Memory: 
                    // Can't seem to use the CSB for in-memory databases, will need to hard-code conn-string
                    // See GetKeyValuePair() in System.Data.Common.DbConnectionOptions
                    connectionString = $"Data Source=file:{dbName}?mode=memory&cache=shared;datetimekind=Utc";
                    break;
                
                default:
                    var csb = new SQLiteConnectionStringBuilder();
                    csb.DataSource = dbName;
                    csb.DateTimeKind = DateTimeKind.Utc;
                    connectionString = csb.ConnectionString;
                    break;
            }
            
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