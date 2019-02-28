using CommandLine;

namespace DetritusThresher.ConsoleScanner.Options
{
    interface ITakesScanId
    {
        [Option('i', "scan-id", Required = false, HelpText = "Scan identifier.")]
        string ScanId { get; set; }
    }
}