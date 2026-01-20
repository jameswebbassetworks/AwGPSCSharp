using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public interface IEventCodeHandler
{
    public string EventCodeName { get; }
    
    void HandleEventCode(CombinedMessage message);
}