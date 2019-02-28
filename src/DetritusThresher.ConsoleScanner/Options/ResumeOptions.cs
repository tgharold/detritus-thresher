using CommandLine;

namespace DetritusThresher.ConsoleScanner.Options
{
    [Verb("resume", HelpText = "Resume scanning.")]
    public class ResumeOptions : IOptions, IRequiresScanId
    {
        public bool Quiet { get; set; }
        public string ScanId { get; set; }
    }
}