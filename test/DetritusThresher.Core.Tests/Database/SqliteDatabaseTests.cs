using DetritusThresher.Core.Database;
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
            const string tableName = nameof(CanGetConnectionToExistingDatabase);
            const string fieldName = "Id";

            using(var db = new SqliteDatabase())
            {
                using (var conn1 = db.GetConnection())
                {
                    conn1.Open();

                    var createCommand = conn1.CreateCommand();
                    createCommand.CommandText = $"create table {tableName} ({fieldName} INTEGER);";
                    createCommand.ExecuteNonQuery();

                    // https://www.sqlite.org/fileformat.html#storage_of_the_sql_database_schema
                    // column names: type, name, tbl_name, rootpage, sql

                    var infoCommand = conn1.CreateCommand();
                    infoCommand.CommandText = 
                        $"SELECT name FROM sqlite_master WHERE type='table' and name='{tableName}'";
                    var reader = infoCommand.ExecuteReader();
                    Assert.True(reader.Read());
                    var foundTableName = reader.GetString(0);
                    Assert.Equal(tableName, foundTableName);

                    conn1.Close();
                }   

                using (var conn2 = db.GetConnection())
                {
                    conn2.Open();

                    var infoCommand = conn2.CreateCommand();
                    infoCommand.CommandText = 
                        $"SELECT name FROM sqlite_master WHERE type='table' and name='{tableName}'";
                    var reader = infoCommand.ExecuteReader();
                    Assert.True(reader.Read(), $"Failed to find table in {nameof(conn2)} connection.");
                    var foundTableName = reader.GetString(0);
                    Assert.Equal(tableName, foundTableName);

                    conn2.Close();
                }
            }
        }
    }
}