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

        public static FolderScan CreateFolderScan(
            string name,
            string uri
            )
        {
            return new FolderScan
            {
                Name = name,
                Uri = uri
            };
        }

        public static FileScan CreateFileScan(
            string name,
            string uri
            )
        {
            return new FileScan
            {
                Name = name,
                Uri = uri
            };
        }        
    }
}