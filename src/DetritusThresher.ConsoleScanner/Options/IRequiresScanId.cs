using CommandLine;

namespace DetritusThresher.ConsoleScanner.Options
{
    interface IRequiresScanId
    {
        [Option('i', "scan-id", Required = true, HelpText = "Scan identifier.")]
        string ScanId { get; set; }
    }
}