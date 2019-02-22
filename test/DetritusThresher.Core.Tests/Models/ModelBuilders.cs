using DetritusThresher.Core.Models;

namespace DetritusThresher.Core.Tests.Models
{
    public static class ModelBuilders
    {
        public static Scan CreateScan(
            string name
            )
        {
            return new Scan
            {
                Name = name
            };
        }
    }
}