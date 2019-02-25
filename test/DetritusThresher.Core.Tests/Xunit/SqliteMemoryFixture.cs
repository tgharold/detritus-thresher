using Microsoft.Extensions.DependencyInjection;
using System;
using DetritusThresher.Migrations.Migrations;
using DetritusThresher.Core.Database;
using FluentMigrator.Runner;
using System.Data.Common;

namespace DetritusThresher.Core.Tests.Xunit
{
    public class SqliteMemoryFixture : IDisposable
    {
        private readonly SqliteDatabase _database;

        public SqliteMemoryFixture()
        {
            var dbName = $"mem-{Guid.NewGuid().ToString()}";
            _database = new SqliteDatabase(dbName, SqliteDatabaseType.Memory);

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