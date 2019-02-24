using System;
using DetritusThresher.Core.Database;

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
        }
    }
}
