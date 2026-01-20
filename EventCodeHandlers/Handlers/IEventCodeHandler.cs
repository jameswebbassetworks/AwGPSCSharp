using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public interface IEventCodeHandler
{
    void HandleEventCode(CombinedMessage message);
}