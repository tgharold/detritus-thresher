using Xunit;
using  DetritusThresher.Core.Tests.Xunit;

namespace DetritusThresher.Core.Tests.Xunit.Tests
{
    public class SqliteFixtureTests
    {
        [Fact]
        public void CanCreateFixture()
        {
            var fixture = new SqliteFixture();
            Assert.NotNull(fixture);
        }
    }
}