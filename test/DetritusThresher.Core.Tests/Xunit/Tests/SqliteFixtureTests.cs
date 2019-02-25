using Xunit;

namespace DetritusThresher.Core.Tests.Xunit.Tests
{
    public class SqliteFixtureTests
    {
        [Fact]
        public void CanCreateFixture()
        {
            using (var fixture = new SqliteFixture())
            {
                Assert.NotNull(fixture);
            }
        }

        [Fact]
        public void CanGetConnection()
        {
            using (var fixture = new SqliteFixture())
            {
                using (var conn = fixture.GetConnection())
                {
                    Assert.NotNull(conn);
                }
            }
        }
    }
}