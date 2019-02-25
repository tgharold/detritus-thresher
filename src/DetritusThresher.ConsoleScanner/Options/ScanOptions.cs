using System.Collections.Generic;
using CommandLine;

namespace DetritusThresher.ConsoleScanner.Options
{
    [Verb("scan", HelpText = "Scan files to the index.")]
    public class ScanOptions : IOptions
    {
        [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        public bool Quiet { get; set; }
    }
}