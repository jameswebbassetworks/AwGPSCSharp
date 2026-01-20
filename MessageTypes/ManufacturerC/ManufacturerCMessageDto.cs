using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerC;

public class ManufacturerCMessageDto : IDto
{
    public string VIN { get; init; }
    public string DeviceId { get; init; }
    public string EventCode { get; init; }
    public string Latitude { get; init; }
    public string Longitude { get; init; }
    public string Timestamp { get; init; }
    public string Speed { get; init; }
    public string Direction { get; init; }
    public string? Idletime { get; private set; }
    public string? MaxSpeed { get; private set; }

    public static ManufacturerCMessageDto CreateManufacturerCMessageDto(Dictionary<int, string> fieldMap)
    {
        // Date and Time are split up.  Combine them to be parsed
        var dateTime = $"{fieldMap[ManufacturerCConstants.Date]} {fieldMap[ManufacturerCConstants.Time]}";
        
        var dto = new ManufacturerCMessageDto
        {
            VIN = fieldMap[ManufacturerCConstants.VIN],
            DeviceId = fieldMap[ManufacturerCConstants.DeviceId],
            EventCode = fieldMap[ManufacturerCConstants.EventCode],
            Latitude = fieldMap[ManufacturerCConstants.Latitude],
            Longitude = fieldMap[ManufacturerCConstants.Longitude],
            Timestamp = dateTime,
            Speed = fieldMap[ManufacturerCConstants.Speed],
            Direction = fieldMap[ManufacturerCConstants.Direction],
        };

        // Optional Fields
        if (fieldMap.TryGetValue(ManufacturerCConstants.Idletime, out var idleTime))
        {
            dto.Idletime = idleTime;
        }
        
        if (fieldMap.TryGetValue(ManufacturerCConstants.MaxSpeed, out var maxSpeed))
        {
            dto.MaxSpeed = maxSpeed;
        }

        return dto;
    }
}