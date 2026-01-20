using System;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public class StartSpeedingEventHandler : IEventCodeHandler
{
    public string EventCodeName => "StartSpeed";

    public void HandleEventCode(CombinedMessage message)
    {
        Console.WriteLine(new string("-"), 15);
        if (message.VIN.IsNotNullOrWhiteSpace())
        {
            Console.WriteLine($"VIN: {message.VIN}");
        }
        Console.WriteLine($"Event Type: {message.EventCodeName}");
        Console.WriteLine($"Timestamp: {message.Timestamp:u}");
        Console.WriteLine("Location:");
        Console.WriteLine($"    Latitude: {message.Latitude}");
        Console.WriteLine($"    Longitude: {message.Longitude}");
        Console.WriteLine($"Speed: {message.Speed}{message.SpeedUnits}");
    }
}
