using System;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor;

public class MessageProcessor
{
    public void Process(Message message)
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message
        
        Console.WriteLine("Processing message...");
        Console.WriteLine($"Message contains {message.Fields.Count} fields");

        // Thought 1
        var m1Message = new CombinedMessage
        {
            DeviceID = message.Fields[ManufacturerA.DeviceId],
            EventCodeID = message.Fields[ManufacturerA.EventCode]
        };


        // Thought 2
        var newMessage = message.MessageType switch
        {
            0 or 1 or 3 => CombinedMessage.Map(message.Fields, message.MessageType),
            _ => throw new Exception("Unknown MessageType")
        };
        
        // Mechanism to:
        // Register a parser for the different manufacturer based on the MessageType
        // Each manufacturers logic maintained in into own Parse
        
        
        // For the Event Codes
        // Register an action for the different event codes by name as the ID changes depending on the Manufacturer
        // Those actions would run against the message and manufacturer
        
        
        
    }
}
