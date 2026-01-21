using System;
using CSharpInterviewMessageProcessor.EventCodeHandlers;
using CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;
using CSharpInterviewMessageProcessor.Helpers;
using CSharpInterviewMessageProcessor.Logging;
using CSharpInterviewMessageProcessor.MessageTypes;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using Microsoft.Extensions.Logging;

namespace CSharpInterviewMessageProcessor;

public class MessageProcessor
{
    public MessageProcessor()
    {
        // Register all Message Handlers
        var messageHandlers = ReflectionHelper.GetObjectsByBaseClass<BaseMessageGenerator>();
        foreach (var messageHandler in messageHandlers)
        {
            _messageTypeHandlerService.RegisterHandler(messageHandler.MessageTypeId, messageHandler);
        }

        // Register all Event Code Handlers
        var eventCodeHandlers = ReflectionHelper.GetObjectsByBaseClass<IEventCodeHandler>();
        foreach (var eventCodeHandler in eventCodeHandlers)
        {
            _eventCodeHandlerService.RegisterHandler(eventCodeHandler.EventCodeName, eventCodeHandler);
        }
    }

    private readonly IEventCodeHandlerService _eventCodeHandlerService = new EventCodeHandlerService();
    private readonly IMessageTypeHandlerService _messageTypeHandlerService = new MessageTypeHandlerService();

    private readonly ILogger _logger = ConsoleAppLogging.CreateLogger<MessageProcessor>();
    
    public void Process(Message message)
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message
        
        _logger.LogInformation($"Processing message type: {message.MessageType}...");
        _logger.LogInformation($"Message contains {message.Fields.Count} fields");

        // Parse out the message files into a typed DTO class
        var parsedMessage = _messageTypeHandlerService.RunHandler(message.MessageType, message.Fields);
        
        // Convert that DTO to a 
        var combinedMessage = parsedMessage.ToCombinedMessage();
        
        _eventCodeHandlerService.RunHandler(combinedMessage.EventCodeName, combinedMessage);

    }
}

