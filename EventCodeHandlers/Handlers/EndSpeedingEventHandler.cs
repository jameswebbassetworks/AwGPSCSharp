using System;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public class EndSpeedingEventHandler : IEventCodeHandler
{
    public string EventCodeName => "EndSpeed";

    public void HandleEventCode(CombinedMessage message)
    {
        Console.WriteLine($"Handling the {EventCodeName} Event Code");
    }
}