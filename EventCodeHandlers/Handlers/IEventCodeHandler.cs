using System;
using System.Collections.Generic;
using System.Linq;
using CSharpInterviewMessageProcessor.Helpers.WebRequests;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.EventCodeHandlers.Handlers;

public interface IEventCodeHandler
{
    public string EventCodeName { get; }
    
    void HandleEventCode(CombinedMessage message);

    void LogVinInformation(CombinedMessage message)
    {
        var vinData = WebRequestCache.GetVinInformation(message.VIN!).Result;

        var modelYear = vinData.Results.FirstOrDefault(r => r.VariableId == 29)!;
        var make = vinData.Results.FirstOrDefault(r => r.VariableId == 26)!;
        var model = vinData.Results.FirstOrDefault(r => r.VariableId == 28)!;
        var fuelTypePrimary = vinData.Results.FirstOrDefault(r => r.VariableId == 24)!;

        List<QueryVariables> features = [modelYear, make, model, fuelTypePrimary];
            
        Console.WriteLine($"VIN: {message.VIN}");
        foreach (var feature in features)
        {
            Console.WriteLine($"    {feature.Variable}: {feature.Value}");
        }
    }

}
