using System;

namespace CSharpInterviewMessageProcessor.MessageTypes.Common;

public class CombinedMessage
{
    public string DeviceId { get; set; }
    public string EventCodeName { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public double Speed { get; set; }
    public string SpeedUnits { get; set; }
    public int Direction { get; set; }
    public double? Idletime { get; set; }
    public double? MaxSpeed { get; set; }
    public string? VIN { get; set; }
}
