using CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers;

public interface IEventCodeHandlerService
{
    public void RegisterHandler(string eventCodeName, IEventCodeHandler eventCodeHandler);
    public void RunHandler(string eventCodeName, CombinedMessage combinedMessage);
}