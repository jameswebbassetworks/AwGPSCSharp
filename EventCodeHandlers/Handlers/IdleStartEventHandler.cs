using System;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public class IdleStartEventHandler : IEventCodeHandler
{
    public string EventCodeName => "IdleStart";

    public void HandleEventCode(CombinedMessage message)
    {
        Console.WriteLine($"Handling the {EventCodeName} Event Code");
    }
}