using System;
using AwGPSCSharp.Domain;        
using AwGPSCSharp.Processing;

namespace AwGPSCSharp.Processing;

public class MessageProcessor
{
    private readonly MessageBuilder _builder = new();
    private readonly VehicleEventDispatcher _dispatcher = new();

    public void Process(Message message)
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message

        if (message == null)
            throw new ArgumentNullException(nameof(message));

        var vehicleEvent = _builder.Build(message);
        _dispatcher.Dispatch(vehicleEvent);

        Console.WriteLine("Processing message...");
        Console.WriteLine($"Message contains {message.Fields.Count} fields");
    }
}
