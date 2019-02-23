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
using NPoco;

namespace DetritusThresher.Core.Tests.Xunit
{
    public class NPocoFixture : IDisposable
    {
        private readonly SqliteDatabase _database;

        public NPocoFixture()
        {
            var dbName = $"mem-{Guid.NewGuid().ToString()}";

            var csb = new SQLiteConnectionStringBuilder();
            csb.DataSource = $"file:{dbName}?mode=memory&cache=shared";
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

        public NPoco.Database GetConnection()
        {
            return new NPoco.Database(_database.GetConnection(), DatabaseType.SQLite);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}