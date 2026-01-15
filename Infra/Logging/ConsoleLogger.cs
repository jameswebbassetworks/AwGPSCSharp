using System;

namespace AwGPSCSharp.Infrastructure.Logging;

public class ConsoleLogger : ILogger
{
    public void Info(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.UtcNow:u} {message}");
    }

    public void Error(string message, Exception ex)
    {
        Console.WriteLine($"[ERROR] {DateTime.UtcNow:u} {message}");
        Console.WriteLine(ex.Message);
    }
}
