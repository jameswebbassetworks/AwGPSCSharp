using System;

namespace CSharpInterviewMessageProcessor;

public class MessageProcessor
{
    public void Process(Message message)
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message

        Event ev = new Event();

        MessageEventMapper.MapEventType(message, ev);
        MessageEventMapper.MapLocation(message, ev);
        MessageEventMapper.MapTimestamp(message, ev);
        MessageEventMapper.MapCurrentSpeed(message, ev);
        MessageEventMapper.MapMaxSpeed(message, ev);
        MessageEventMapper.MapIdleTime(message, ev);
        MessageEventMapper.MapVIN(message, ev);
        MessageEventMapper.SetDisplayValues(ev);

        Console.WriteLine(ev.ToString());

        Console.WriteLine("Processing message...");
        Console.WriteLine($"Message contains {message.Fields.Count} fields");
    }

    
}
