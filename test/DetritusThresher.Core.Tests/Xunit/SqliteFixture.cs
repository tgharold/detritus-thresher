using Microsoft.Extensions.DependencyInjection;
using FluentMigrator;
using System;
using Xunit;
using DetritusThresher.Migrations.Migrations;
using DetritusThresher.Core.Database;
using FluentMigrator.Runner;
using System.Data.Common;
using System.Data.SQLite;
using System.Data;

namespace DetritusThresher.Core.Tests.Xunit
{
    public class SqliteFixture : IDisposable
    {
        private readonly SqliteDatabase _database;

        public SqliteFixture()
        {
            var dbName = $"mem-{Guid.NewGuid().ToString()}";
            //var connString = $"Data Source=file:{dbName}?mode=memory&cache=shared";

            var csb = new SQLiteConnectionStringBuilder();
            csb.FullUri = $"file:{dbName}?mode=memory&cache=shared";
            csb.DateTimeKind = DateTimeKind.Utc;
            var connString = csb.ConnectionString;

            _database = new SqliteDatabase(connString);

            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddSQLite()
                        .WithGlobalConnectionString(connString)
                        .WithMigrationsIn(typeof(InitialMigration).Assembly))
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }
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