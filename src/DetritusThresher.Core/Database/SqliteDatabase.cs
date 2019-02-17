using System;
using System.Data.Common;
using System.Transactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using NPoco;

namespace DetritusThresher.Core.Database
{
    public class SqliteDatabase : IDisposable
    {
        private SqliteConnection _holdOpenConnection;

//TODO: take in a connection string
        public SqliteDatabase()
        {
            var csb = new SqliteConnectionStringBuilder{
                DataSource = @"Data Source=:memory:"
            };
            
            _holdOpenConnection = new SqliteConnection(csb.ToString());
        }

        public DbConnection GetConnection()
        {
            var dataSource = _holdOpenConnection.DataSource;
            return new SqliteConnection(dataSource);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _holdOpenConnection.Close();
                    _holdOpenConnection.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SqliteDatabase() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}