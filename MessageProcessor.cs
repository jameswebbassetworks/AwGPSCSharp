using System;
using CSharpInterviewMessageProcessor.EventCodeHandlers;
using CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;
using CSharpInterviewMessageProcessor.Helpers;
using CSharpInterviewMessageProcessor.MessageTypes;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor;

public class MessageProcessor
{
    public MessageProcessor()
    {
        // Register all Message Handlers
        var messageHandlers = ReflectionHelper.GetObjectsByBaseClass<BaseMessageHandler>();
        foreach (var messageHandler in messageHandlers)
        {
            messageHandler.RegisterSelf();
        }

        // Register all Event Code Handlers
        var eventCodeHandlers = ReflectionHelper.GetObjectsByBaseClass<IEventCodeHandler>();
        foreach (var eventCodeHandler in eventCodeHandlers)
        {
            _eventCodeHandlerService.RegisterHandler(eventCodeHandler.EventCodeName, eventCodeHandler);
        }
    }

    private readonly EventCodeHandlerService _eventCodeHandlerService = new();
    
    public void Process(Message message)
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message
        
        Console.WriteLine("Processing message...");
        Console.WriteLine($"Message contains {message.Fields.Count} fields");

        var parsedMessage = MessageTypeHandler.RunHandler(message.MessageType, message.Fields);
        var combinedMessage = parsedMessage.ToCombinedMessage();
        
        _eventCodeHandlerService.RunHandler(combinedMessage.EventCodeName, combinedMessage);

    }
}

