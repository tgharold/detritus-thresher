using DetritusThresher.Migrations.Migrations;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace DetritusThresher.Core.Tests.Xunit
{
    public static class MigratorHelper
    {
        public static void RunSqliteMigrations(string connString)
        {
            using (var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddSQLite()
                        .WithGlobalConnectionString(connString)
                        .WithMigrationsIn(typeof(InitialMigration).Assembly))
                .BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                    runner.MigrateUp();
                }
            }
        }
    }
}