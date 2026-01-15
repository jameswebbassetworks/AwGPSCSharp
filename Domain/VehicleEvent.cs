using System;

namespace AwGPSCSharp.Domain;

public class VehicleEvent
{
    public VehicleEventType EventType { get; set; }

    public string DeviceId { get; set; } = string.Empty;
    public string? Vin { get; set; }

    public DateTime Timestamp { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public double? Speed { get; set; }
    public double? MaxSpeed { get; set; }
    public int? IdleTimeSeconds { get; set; }
}
