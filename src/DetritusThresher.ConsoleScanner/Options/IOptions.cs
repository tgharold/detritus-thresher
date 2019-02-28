using CommandLine;

namespace DetritusThresher.ConsoleScanner.Options
{
    interface IOptions
    {
        [Option('q', "quiet",
            HelpText = "Suppresses summary messages.")]
        bool Quiet { get; set; }
    }
}