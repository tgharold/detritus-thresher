using CommandLine;

namespace DetritusThresher.ConsoleScanner.Options
{
    [Verb("report", HelpText = "Report files to the index.")]
    public class ReportOptions : IOptions
    {
        public bool Quiet { get; set; }
    }
}