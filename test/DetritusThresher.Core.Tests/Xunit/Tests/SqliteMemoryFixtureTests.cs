using Xunit;

namespace DetritusThresher.Core.Tests.Xunit.Tests
{
    public class SqliteMemoryFixtureTests
    {
        [Fact]
        public void CanCreateFixture()
        {
            using (var fixture = new SqliteMemoryFixture())
            {
                Assert.NotNull(fixture);
            }
        }

        [Fact]
        public void CanGetConnection()
        {
            using (var fixture = new SqliteMemoryFixture())
            {
                using (var conn = fixture.GetConnection())
                {
                    Assert.NotNull(conn);
                }
            }
        }
    }
}