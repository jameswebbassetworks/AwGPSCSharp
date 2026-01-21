using System;
using System.Collections.Generic;

namespace CSharpInterviewMessageProcessor
{
    public class DynamicTranslator : IMessageTranslator
    {
        private readonly string _manufacturer;
        private readonly Dictionary<string, Dictionary<string, string>> _mappings;

        public DynamicTranslator(string manufacturer)
        {
            _manufacturer = manufacturer;

            _mappings = new Dictionary<string, Dictionary<string, string>>
            {
                // Type 0 (Manufacturer A)
                // EventCode=1, Lat=4, Lon=5, Timestamp=6, CurrSpeed=8, MaxSpeed=10, IdleTime=9
                ["ManufacturerA"] = new Dictionary<string, string>
    {
        { "EventCode", "1" },
        { "Latitude", "4" },
        { "Longitude", "5" },
        { "Timestamp", "6" },
        { "Speed", "8" },
        { "MaxSpeed", "10" },   // optional, only present in speeding-start sample
        { "IdleTime", "9" }     // optional, only present in idle-end sample
    },

                // Type 1 (Manufacturer B)
                // EventCode=0, Coords=2 ("lat,lon"), Timestamp=3, Speed=4, IdleTime=12
                ["ManufacturerB"] = new Dictionary<string, string>
    {
        { "EventCode", "0" },
        { "Coordinates", "2" },
        { "Timestamp", "3" },
        { "Speed", "4" },
        { "IdleTime", "12" }    // optional, only in idle-end sample
    },

                // Type 3 (Manufacturer C)
                // VIN=0, EventCode=1, Date=2 + Time=3 -> Timestamp, Lat=10, Lon=11, Speed=25, MaxSpeed=27, IdleTime=30
                ["ManufacturerC"] = new Dictionary<string, string>
{
    { "VIN", "0" },
    { "EventCode", "1" },
    { "Timestamp", "2,3" }, // Combine Date (2) + Time (3)
    { "Latitude", "10" },
    { "Longitude", "11" },
    { "Speed", "25" },
    { "MaxSpeed", "27" },   // optional
    { "IdleTime", "30" }    // optional
}
            };
        }

        public DeviceData Translate(Message message)
        {
            try
            {
                if (!_mappings.ContainsKey(_manufacturer))
            {
                Console.WriteLine($"Unknown manufacturer: {_manufacturer}");
                return null;
            }

            var map = _mappings[_manufacturer];
            var f = message.Fields;

            string Get(string key) =>
                map.ContainsKey(key) && f.ContainsKey(int.Parse(map[key])) ? f[int.Parse(map[key])] : null;

                DateTime timestamp = DateTime.MinValue;

                if (map.ContainsKey("Timestamp"))
                {
                    var timestampMapping = map["Timestamp"];

                    // Handle combined date + time fields
                    if (timestampMapping.Contains(","))
                    {
                        var parts = timestampMapping.Split(',');
                        if (parts.Length == 2)
                        {
                            var date = f[int.Parse(parts[0])];
                            var time = f[int.Parse(parts[1])];
                            timestamp = DateTime.Parse($"{date}T{time}");
                        }
                    }
                    else
                    {
                        if (DateTime.TryParse(f[int.Parse(timestampMapping)], out var ts))
                            timestamp = ts;
                    }
                }
                return new DeviceData
                {
                    EventCode = Get("EventCode"),
                    Latitude = double.TryParse(Get("Latitude"), out var lat) ? lat : 0,
                    Longitude = double.TryParse(Get("Longitude"), out var lon) ? lon : 0,
                    Speed = double.TryParse(Get("Speed"), out var spd) ? spd : 0,
                    Timestamp = timestamp,
                    VIN = Get("VIN")
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error translating message for {_manufacturer}: {ex.Message}");
                return null;
            }
        }
    }
}