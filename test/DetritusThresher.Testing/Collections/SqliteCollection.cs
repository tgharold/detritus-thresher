using DetritusThresher.Testing.Constants;
using DetritusThresher.Testing.Fixtures;
using Xunit;

namespace DetritusThresher.Testing.CollectionFixtures
{
    [CollectionDefinition(CollectionFixtureNames.Sqlite)]
    public class SqliteCollection : ICollectionFixture<SqliteFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}