using System;
using DetritusThresher.Core.Models;
using Xunit;

namespace DetritusThresher.Core.Tests.Models
{
    public class DateTimeRoundTripTests
    {
        [Theory]
        [InlineData("2018-11-25T23:55:03.450")]
        public void LogEntry_Created_round_trips_correctly(string input)
        {
            var date = DateTime.Parse(input);
            var x = new LogEntry{
                Created = date,
            };
            Assert.Equal(DateTimeKind.Utc, x.Created?.Kind);        
        }
    }
}