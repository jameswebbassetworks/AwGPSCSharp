using System;

namespace CSharpInterviewMessageProcessor;

public class MessageProcessor
{
    public void Process(IMessage message) 
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message
        
        
        message switch
        {
            //check the type 
            typeof MessageTypeOne => 
            //call the intended parser
        }

        Console.WriteLine("Processing message...");
        Console.WriteLine($"Message contains {message.Fields.Count} fields");


    }
}
