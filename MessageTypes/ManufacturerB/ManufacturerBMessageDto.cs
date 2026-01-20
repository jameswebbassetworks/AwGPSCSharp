using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerB;

public class ManufacturerBMessageDto : IDto
{
    public string DeviceId { get; init; }
    public string EventCode { get; init; }
    public string Latitude { get; init; }
    public string Longitude { get; init; }
    public string Timestamp { get; init; }
    public string Speed { get; init; }
    public string Direction { get; init; }
    public string? Idletime { get; private set; }

    public static ManufacturerBMessageDto CreateManufacturerBMessageDto(Dictionary<int, string> fieldMap)
    {
        // Longitude and Latitude are together in one field comma separated
        var latitudeLongitudeArray = fieldMap[ManufacturerBConstants.LatitudeLongitude].Split(",");
        var latitude = latitudeLongitudeArray[0];
        var longitude = latitudeLongitudeArray[1];
        
        var dto = new ManufacturerBMessageDto
        {
            DeviceId = fieldMap[ManufacturerBConstants.DeviceId],
            EventCode = fieldMap[ManufacturerBConstants.EventCode],
            Latitude = latitude,
            Longitude = longitude,
            Timestamp = fieldMap[ManufacturerBConstants.Timestamp],
            Speed = fieldMap[ManufacturerBConstants.Speed],
            Direction = fieldMap[ManufacturerBConstants.Direction],
        };

        // Optional Fields
        if (fieldMap.TryGetValue(ManufacturerBConstants.Idletime, out var idleTime))
        {
            dto.Idletime = idleTime;
        }

        return dto;
    }
}