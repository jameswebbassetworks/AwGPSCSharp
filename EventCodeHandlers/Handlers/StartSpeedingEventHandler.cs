using System;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public class StartSpeedingEventHandler : IEventCodeHandler
{
    public const string EventCodeName = "StartSpeed";

    public void HandleEventCode(CombinedMessage message)
    {
        Console.WriteLine($"Handling the {EventCodeName} Event Code");
    }
}