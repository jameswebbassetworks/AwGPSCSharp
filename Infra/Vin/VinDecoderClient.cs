using System.Linq;
using System.Text.Json;
using CSharpInterviewMessageProcessor;

namespace AwGPSCSharp.Infra.Vin;

public class VinDecoderClient
{
    private readonly WebRequestHelper _web = new();

    public VinInfo Decode(string vin)
    {
        var url =
            $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{vin}?format=json";

        var json = _web.Get(url);
        using var doc = JsonDocument.Parse(json);

        var results = doc.RootElement.GetProperty("Results");

        string Get(string name)
        {
            foreach (var item in results.EnumerateArray())
            {
                if (item.TryGetProperty("Variable", out var variable) &&
                    variable.GetString() == name &&
                    item.TryGetProperty("Value", out var value))
                {
                    return value.GetString() ?? "N/A";
                }
            }

            return "N/A";
        }

        return new VinInfo
        {
            ModelYear = Get("Model Year"),
            Make = Get("Make"),
            Model = Get("Model"),
            FuelType = Get("Fuel Type - Primary")
        };
    }
}
