using Microsoft.Extensions.DependencyInjection;
using FluentMigrator;
using System;
using Xunit;
using DetritusThresher.Migrations.Migrations;
using DetritusThresher.Core.Database;
using FluentMigrator.Runner;
using System.Data.Common;

namespace DetritusThresher.Core.Tests.Xunit
{
    public class SqliteFixture : IDisposable
    {
        private readonly SqliteDatabase _database;

        public SqliteFixture()
        {
            var dbName = DateTimeOffset.UtcNow.Ticks.ToString().PadLeft(10, '0');
            dbName = dbName.Substring(dbName.Length-10);
            var connString = $"Data Source=file:mem{dbName}?mode=memory&cache=shared";

            _database = new SqliteDatabase();

            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddSQLite()
                        .WithGlobalConnectionString(_database.GetConnectionString())
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