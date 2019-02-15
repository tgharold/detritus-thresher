using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentMigrator;
using System;
using Xunit;

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
                        .WithGlobalConnectionString(@"Data Source=:memory:;Version=3;New=True;")
                        .WithMigrationsIn(typeof(MigrationDate20181026113000Zero).Assembly))
                .BuildServiceProvider();
        }

        [Fact]
        public void CanRunMigrations()
        {
            var serviceProvider = this.CreateServiceProvider();
            var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp(1);

            string sqlStatement = "SELECT Description FROM VersionInfo";

            var dataSet = runner.Processor.Read(sqlStatement, string.Empty);

            Assert.NotNull(dataSet);
            Assert.True("foo", dataSet.Tables[0].Rows[0].ItemArray[0]);
        }
    }
}
