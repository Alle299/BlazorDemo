using Microsoft.Extensions.Logging;

namespace BlazorDemo_WebAPI.Helpers
{
    /// <summary>
    /// Configuration for multi color consol logging
    /// </summary>
    public sealed class ColorConsoleLoggerConfiguration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new()
        {
            [LogLevel.Information] = ConsoleColor.Green
        };
    }
}
