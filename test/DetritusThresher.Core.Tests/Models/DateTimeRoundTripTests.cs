using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FluentMigrator;
using System;
using Xunit;
using SQLitePCL;
using DetritusThresher.Migrations.Migrations;
using DetritusThresher.Core.Models;
using FluentMigrator.Runner;
using Microsoft.Data.Sqlite;
using DetritusThresher.Core.Database;

namespace DetritusThresher.Core.Tests.Models
{
    public class DateTimeRoundTripTests
    {
        private ServiceProvider CreateServiceProvider()
        {
            var sc = new ServiceCollection();

            var dbConnectionFactory = new SqliteDbConnectionFactory(
                @"Data Source=:memory:"
                );

            sc.AddSingleton<SqliteDbConnectionFactory>(dbConnectionFactory);

            sc.AddLogging(lb => lb.AddFluentMigratorConsole());

            using (var dbConnection = dbConnectionFactory.GetConnection())
            {
                var dbConnectionString = dbConnection.ConnectionString;
                sc.AddFluentMigratorCore()
                    .ConfigureRunner(
                        builder => builder
                            .AddSQLite()
                            .WithGlobalConnectionString(dbConnectionString)
                            .WithMigrationsIn(typeof(InitialMigration).Assembly))
                    ;
            }

            return sc.BuildServiceProvider();
        }

        [Theory]
        [InlineData("2018-11-25T23:55:03.450")]
        public void LogEntry_Created_round_trips_correctly(string input)
        {
            var serviceProvider = this.CreateServiceProvider();
            var scope = serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
            var dbConnectionFactory = scope.ServiceProvider.GetRequiredService<SqliteDbConnectionFactory>();

            var date = DateTime.Parse(input);
            var x = new LogEntry{
                Created = date,
            };

            using (var conn = dbConnectionFactory.GetConnection())
            {
                
            }            
            

            Assert.Equal(DateTimeKind.Utc, x.Created?.Kind);        
        }
    }
}