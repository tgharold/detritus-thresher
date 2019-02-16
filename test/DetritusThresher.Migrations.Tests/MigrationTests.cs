using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentMigrator;
using System;
using Xunit;
using DetritusThresher.Migrations.Migrations;
using FluentMigrator.Runner;

namespace DetritusThresher.Migrations.Tests
{
    public class MigrationTests
    {
        private ServiceProvider CreateServiceProvider()
        {
            return new ServiceCollection()
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddSQLite()
                        .WithGlobalConnectionString(@"Data Source=:memory:")
                        .WithMigrationsIn(typeof(InitialMigration).Assembly))
                .BuildServiceProvider();
        }

        [Fact]
        public void CanRunMigrations()
        {
            var serviceProvider = this.CreateServiceProvider();
            var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();

            string sqlStatement = "SELECT Description FROM VersionInfo";

            var dataSet = runner.Processor.Read(sqlStatement, string.Empty);

            Assert.NotNull(dataSet);
            Assert.Equal("foo", dataSet.Tables[0].Rows[0].ItemArray[0]);
        }
    }
}
