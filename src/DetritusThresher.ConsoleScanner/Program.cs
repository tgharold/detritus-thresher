using System;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator;
using DetritusThresher.Core.Database;
using FluentMigrator.Runner;
using DetritusThresher.Migrations.Migrations;

namespace DetritusThresher.ConsoleScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DetritusThresher.ConsoleScanner");

            var db = new SqliteDatabase(
                "foo.sqlite",
                SqliteDatabase.DatabaseType.Memory
                );

            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddSQLite()
                        .WithGlobalConnectionString(db.GetConnectionString())
                        .WithMigrationsIn(typeof(InitialMigration).Assembly))
                .BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }            

            Console.WriteLine("Setup database.");
        }
    }
}
