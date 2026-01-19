using System;
using System.Xml;
using CSharpInterviewMessageProcessor.Interfaces;
using CSharpInterviewMessageProcessor.Models;
using CSharpInterviewMessageProcessor.TranslatorClass;

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

        IMessageTranslator translator = message.MessageType switch
        {
            0 => new Type0TranslatorClass(),
           // 1 => new Type1TranslatorClass(),
            //2 => new Type2TranslatorClass(),
            _ => throw new NotSupportedException($"Message type {message.MessageType} is not supported")
        };

        // Step 2: Translate the message
        VehicleEvent vehicleEvent = translator.Translate(message);
    }
}
