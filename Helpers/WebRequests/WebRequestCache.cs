using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.Helpers.WebRequests;

public static class WebRequestCache
{
    private static readonly ConcurrentDictionary<string, string> VinQueryCache = new();

    private static readonly WebRequestHelper WebRequestHelper = new();

    private const string RequestFormatString = "https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{0}?format=json";

    public static async Task<VinQueryResult> GetVinData(string vin)
    {
        if (VinQueryCache.TryGetValue(vin, out var vinData))
        {
            return JsonSerializer.Deserialize<VinQueryResult>(vinData)!;
        }
        
        var queryUrl = string.Format(RequestFormatString, vin);
        var results = await WebRequestHelper.GetAsync(queryUrl);
        
        VinQueryCache.TryAdd(vin, results);
        
        return JsonSerializer.Deserialize<VinQueryResult>(results)!;
    }    
    

    public static async Task<VinQueryResult> GetVinInformation(string vin)
    {
        return await GetVinData(vin);
    }
}
