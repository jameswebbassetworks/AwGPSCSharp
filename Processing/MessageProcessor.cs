using System;
using AwGPSCSharp.Domain;
using AwGPSCSharp.Infrastructure.Logging;

namespace AwGPSCSharp.Processing;

public class MessageProcessor
{
    private readonly MessageBuilder _builder = new();
    private readonly VehicleEventDispatcher _dispatcher = new();
    private readonly ILogger _logger = new ConsoleLogger();

    public void Process(Message message)
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message

        if (message == null)
            throw new ArgumentNullException(nameof(message));

        //log before dispatch:

        _logger.Info(
            $"Processing message type {message.MessageType} with {message.Fields.Count} fields");

        var vehicleEvent = _builder.Build(message);
        _dispatcher.Dispatch(vehicleEvent);

        //log after dispatch:
        _logger.Info(
            $"Processed event {vehicleEvent.EventType} at {vehicleEvent.Timestamp:u}");

        Console.WriteLine("Processing message...");
        Console.WriteLine($"Message contains {message.Fields.Count} fields");
    }
}
