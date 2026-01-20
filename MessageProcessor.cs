using System;
using System.Collections.Generic;

namespace CSharpInterviewMessageProcessor;

public class MessageProcessor
{
    public void Process(Message message)
    {
        // TODO: Interviewee should implement logic to:
        // 1. Extract the relevent information from the message
        // 2. Take the approrpriate action based on the event code in the message
        
        switch (message.MessageType)
        {
            case 0:
                ManufacturerA(message.Fields);
                break;

            case 1: 
                ManufacturerB(message.Fields);
                break;

            case 3:
                ManufacturerC(message.Fields);
                break;
        }
    }

    private void ManufacturerA(Dictionary<int, string> fields)
    {
        var latitude = double.Parse(fields[4]);
        var longitude = double.Parse(fields[5]);
        var speed = double.Parse(fields[7]);

        Console.WriteLine($"Latitude: {latitude}, Longitude: {longitude}, Speed: {speed}");
    }
    private void ManufacturerB(Dictionary<int, string> fields)
    {

        var coords = fields[2].Split(',');
        var latitude = double.Parse(coords[0]);
        var longitude = double.Parse(coords[1]);
        var speed = double.Parse(fields[4]);
        Console.WriteLine($"Latitude: {latitude}, Longitude: {longitude}, Speed: {speed}");
    }

    private void ManufacturerC(Dictionary<int, string> fields)
    {
        var vin = fields[0];
        var latitude = double.Parse(fields[10]);
        var longitude = double.Parse(fields[11]);
        var speed = double.Parse(fields[25]);
        Console.WriteLine($"VIN: {vin}, Latitude: {latitude}, Longitude: {longitude}, Speed: {speed}");


    }
}
