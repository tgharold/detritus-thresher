using System;
using DetritusThresher.Core.Database;
using Microsoft.Data.Sqlite;
using Xunit;

namespace DetritusThresher.Core.Tests.Database
{
    public class SqliteDatabaseTests
    {
        [Fact]
        public void CanCreateDatabase()
        {
            using(var db = new SqliteDatabase())
            {
                Assert.NotNull(db);
            }
        }

        [Fact] 
        public void CanGetConnectionToExistingDatabase()
        {
            var tableName = $"test_{nameof(CanGetConnectionToExistingDatabase)}_{DateTimeOffset.UtcNow.Ticks}";
            const string fieldName = "Id";

            using(var db = new SqliteDatabase())
            {
                using (var conn1 = db.GetConnection())
                {
                    conn1.Open();

                    using (var createCommand = conn1.CreateCommand())
                    {
                        createCommand.CommandText = $"create table @tableName (@fieldName INTEGER);";
                        createCommand.Parameters.Add(new SqliteParameter{
                            ParameterName = "@tableName",
                            Value = tableName,
                        });
                        createCommand.Parameters.Add(new SqliteParameter{
                            ParameterName = "@fieldName",
                            Value = fieldName,
                        });
                        createCommand.ExecuteNonQuery();
                    }

                    using (var infoCommand = conn1.CreateCommand())
                    {
                        // https://www.sqlite.org/fileformat.html#storage_of_the_sql_database_schema
                        // column names: type, name, tbl_name, rootpage, sql

                        infoCommand.CommandText = 
                            $"SELECT name FROM sqlite_master WHERE type='table' and name='@tableName'";
                        infoCommand.Parameters.Add(new SqliteParameter{
                            ParameterName = "@tableName",
                            Value = tableName,
                        });
                        var reader = infoCommand.ExecuteReader();
                        Assert.True(reader.Read());

                        var foundTableName = reader.GetString(0);
                        Assert.Equal(tableName, foundTableName);
                    }
                }   

                using (var conn2 = db.GetConnection())
                {
                    conn2.Open();

                    using (var infoCommand = conn2.CreateCommand())
                    {
                        infoCommand.CommandText = 
                            $"SELECT name FROM sqlite_master WHERE type='table' and name='{tableName}'";
                        var reader = infoCommand.ExecuteReader();
                        Assert.True(reader.Read(), $"Failed to find table in {nameof(conn2)} connection.");
                        var foundTableName = reader.GetString(0);
                        Assert.Equal(tableName, foundTableName);
                    }
                }
            }
        }
    }
}