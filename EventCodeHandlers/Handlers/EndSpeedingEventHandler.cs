using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.Helpers.WebRequests;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public class EndSpeedingEventHandler : IEventCodeHandler
{
    public string EventCodeName => "EndSpeed";

    public void HandleEventCode(CombinedMessage message)
    {
        Console.WriteLine(new string("-"), 15);
        if (message.VIN.IsNotNullOrWhiteSpace())
        {
            var vinData = GetVinInformation(message.VIN!).Result; // Blocking await

            var model = vinData.Results.FirstOrDefault(r => r.VariableId == 28)!;
            var modelYear = vinData.Results.FirstOrDefault(r => r.VariableId == 29)!;
            var vehicleType = vinData.Results.FirstOrDefault(r => r.VariableId == 39)!;

            List<QueryVariables> features = [model, modelYear, vehicleType];
            
            Console.WriteLine($"VIN: {message.VIN}");
            foreach (var feature in features)
            {
                Console.WriteLine($"{feature.Variable}: {feature.Value}");
            }
            
        }
        Console.WriteLine($"Event Type: {message.EventCodeName}");
        Console.WriteLine($"Timestamp: {message.Timestamp:u}");
        Console.WriteLine("Location:");
        Console.WriteLine($"    Latitude: {message.Latitude}");
        Console.WriteLine($"    Longitude: {message.Longitude}");
        Console.WriteLine($"Speed: {message.Speed}{message.SpeedUnits}");
        if (message.MaxSpeed.HasValue)
        {
            Console.WriteLine($"Max Speed: {message.MaxSpeed}{message.SpeedUnits}");
        }
    }

    private static async Task<VinQueryResult> GetVinInformation(string vin)
    {
        return await WebRequestCache.GetVinData(vin);;
    }
}
