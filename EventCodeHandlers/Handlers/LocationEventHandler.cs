using System;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public class LocationEventHandler : IEventCodeHandler
{
    public string EventCodeName => "Location";

    public void HandleEventCode(CombinedMessage message)
    {
        Console.WriteLine($"Handling the {EventCodeName} Event Code");
    }
}