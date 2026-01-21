using System;
using System.Collections.Concurrent;
using CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers;

public class EventCodeHandlerService : IEventCodeHandlerService
{
    private readonly ConcurrentDictionary<string, IEventCodeHandler> _handlers = new();
    public void RegisterHandler(string eventCodeName, IEventCodeHandler eventCodeHandler)
    {
        _handlers.TryAdd(eventCodeName, eventCodeHandler);
    }

    public void RunHandler(string eventCodeName, CombinedMessage combinedMessage)
    {
        if (_handlers.TryGetValue(eventCodeName, out var eventCodeHandler))
        {
            eventCodeHandler.HandleEventCode(combinedMessage);
        }
        else
        {
            throw new Exception($"No handler found for event code {eventCodeName}");
        }
    }
}
