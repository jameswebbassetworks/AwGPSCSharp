using System;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public class IdleEndEventHandler : IEventCodeHandler
{
    public string EventCodeName => "IdleEnd";

    public void HandleEventCode(CombinedMessage message)
    {
        Console.WriteLine(new string('-', 15));
        if (message.VIN.IsNotNullOrWhiteSpace())
        {
            ((IEventCodeHandler)this).LogVinInformation(message); // Trying out Default Interface Method here.
        }
        Console.WriteLine($"Event Type: {message.EventCodeName}");
        Console.WriteLine($"Timestamp: {message.Timestamp:u}");
        Console.WriteLine("Location:");
        Console.WriteLine($"    Latitude: {message.Latitude}");
        Console.WriteLine($"    Longitude: {message.Longitude}");
        if (message.Idletime.HasValue)
        {
            Console.WriteLine($"Idle Time: {message.Idletime}");
        }
    }
}
