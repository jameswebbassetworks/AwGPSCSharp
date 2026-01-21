using Microsoft.Extensions.Logging;

namespace CSharpInterviewMessageProcessor.Logging;

public static class ConsoleAppLogging
{
    public static ILoggerFactory LoggerFactory { get; } = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
    {
        builder
            .AddConsole()
            .SetMinimumLevel(LogLevel.Information);
    });

    public static ILogger CreateLogger<T>() => LoggerFactory.CreateLogger<T>();

    public static ILogger CreateLogger(string categoryName) => LoggerFactory.CreateLogger(categoryName);
}
