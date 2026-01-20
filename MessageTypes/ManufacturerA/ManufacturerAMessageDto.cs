using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerA;

public class ManufacturerAMessageDto : IDto
{
    public string DeviceId { get; set; }
    public string EventCode { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public string Timestamp { get; set; }
    public string Speed { get; set; }
    public string Direction { get; set; }
    public string? Idletime { get; set; }
    public string? MaxSpeed { get; set; }

    public static ManufacturerAMessageDto CreateManufacturerAMessageDto(Dictionary<int, string> fieldMap)
    {
        var dto = new ManufacturerAMessageDto
        {
            DeviceId = fieldMap[ManufacturerAConstants.DeviceId],
            EventCode = fieldMap[ManufacturerAConstants.EventCode],
            Latitude = fieldMap[ManufacturerAConstants.Latitude],
            Longitude = fieldMap[ManufacturerAConstants.Longitude],
            Timestamp = fieldMap[ManufacturerAConstants.Timestamp],
            Speed = fieldMap[ManufacturerAConstants.Speed],
            Direction = fieldMap[ManufacturerAConstants.Direction],
        };

        // Optional Fields
        if (fieldMap.TryGetValue(ManufacturerAConstants.Idletime, out var idleTime))
        {
            dto.Idletime = idleTime;
        }
        
        if (fieldMap.TryGetValue(ManufacturerAConstants.MaxSpeed, out var maxSpeed))
        {
            dto.MaxSpeed = maxSpeed;
        }

        return dto;
    }
}