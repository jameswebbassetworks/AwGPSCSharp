using System;
using System.Collections.Generic;

namespace CSharpInterviewMessageProcessor.Models;

/// <summary>
/// Possible solution to make a more generic parse messages into a known static model.
/// </summary>
public class CombinedMessage
{
    public string DeviceID { get; set; }
    public string EventCodeID { get; set; }
    public string EventCodeName { get; set; }
    public long Latitude { get; set; }
    public long Longitude { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public SpeedType Speed { get; set; }
    public long Direction { get; set; }

    public static CombinedMessage Map(Dictionary<int, string> messageFields, int messageType)
    {
        // Map to a Combined Message.  This should come from Manufacturer Logic
        return null;
    }
}


public class SpeedType
{
    public int Speed { get; set; }
    public string Unit { get; set; }
}