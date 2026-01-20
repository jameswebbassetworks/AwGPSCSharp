using System;
using CSharpInterviewMessageProcessor.EventCodeHandlers;
using CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;
using CSharpInterviewMessageProcessor.MessageTypes;
using CSharpInterviewMessageProcessor.MessageTypes.ManufacturerA;
using CSharpInterviewMessageProcessor.MessageTypes.ManufacturerB;
using CSharpInterviewMessageProcessor.MessageTypes.ManufacturerC;

namespace CSharpInterviewMessageProcessor;

public class MessageProcessor
{
    public MessageProcessor()
    {
        MessageTypeHandler.RegisterHandler(ManufacturerAMessage.MessageTypeId, new ManufacturerAMessage());
        MessageTypeHandler.RegisterHandler(ManufacturerBMessage.MessageTypeId, new ManufacturerBMessage());
        MessageTypeHandler.RegisterHandler(ManufacturerCMessage.MessageTypeId, new ManufacturerCMessage());
        
        _eventCodeHandlerService.RegisterHandler(LocationEventHandler.EventCodeName, new LocationEventHandler());
        _eventCodeHandlerService.RegisterHandler(StartSpeedingEventHandler.EventCodeName, new StartSpeedingEventHandler());
        _eventCodeHandlerService.RegisterHandler(EndSpeedingEventHandler.EventCodeName, new EndSpeedingEventHandler());
        _eventCodeHandlerService.RegisterHandler(IdleStartEventHandler.EventCodeName, new IdleStartEventHandler());
        _eventCodeHandlerService.RegisterHandler(IdleEndEventHandler.EventCodeName, new IdleEndEventHandler());
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

