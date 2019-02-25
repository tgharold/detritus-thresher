using System;
using DetritusThresher.ConsoleScanner.Options;
using CommandLine;

namespace DetritusThresher.ConsoleScanner
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("DetritusThresher.ConsoleScanner");

            return CommandLine.Parser.Default.ParseArguments<
                    ScanOptions,
                    ReportOptions
                >(args).MapResult(
                    (ScanOptions opts) => RunScanAndReturnExitCode(opts),
                    (ReportOptions opts) => RunReportAndReturnExitCode(opts),
                    errs => 1
                );
        }

        private static int RunScanAndReturnExitCode(ScanOptions opts)
        {
            throw new NotImplementedException();
        }

        private static int RunReportAndReturnExitCode(ReportOptions opts)
        {
            throw new NotImplementedException();
        }
    }
}
