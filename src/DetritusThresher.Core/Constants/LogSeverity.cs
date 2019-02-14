namespace DetritusThresher.Core.Constants
{
    // note: This follows the same as Microsoft.Extensions.Logging.LogLevel
    // done as an enum since log levels are often sorted by severity
    public enum LogSeverity
    {
        //TODO: unit test to verify maximum lengths
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical
    }
}