using System;

namespace AwGPSCSharp.Infrastructure.Logging;

public interface ILogger
{
    void Info(string message);
    void Error(string message, Exception ex);
}
