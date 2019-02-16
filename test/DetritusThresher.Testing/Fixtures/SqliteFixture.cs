using Microsoft.Extensions.DependencyInjection;
using FluentMigrator;
using System;
using Xunit;

namespace DetritusThresher.Testing.Fixtures
{
    public class SqliteFixture
    {
        private ServiceCollection CreateServiceCollection()
        {
            return new ServiceCollection();
        }
    }
}