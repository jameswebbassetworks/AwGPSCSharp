using System.Collections.Generic;
using System.Linq;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerB;

public class ManufacturerBMessageDto : IDto
{
    public string DeviceId { get; init; }
    public string EventCode { get; init; }
    public string LatitudeLongitude { get; init; }
    public string Timestamp { get; init; }
    public string Speed { get; init; }
    public string Direction { get; init; }
    public string? Idletime { get; private set; }

    public static ManufacturerBMessageDto CreateManufacturerBMessageDto(Dictionary<int, string> fieldMap)
    {
        
        var dto = new ManufacturerBMessageDto
        {
            DeviceId = fieldMap[ManufacturerBConstants.DeviceId],
            EventCode = fieldMap[ManufacturerBConstants.EventCode],
            LatitudeLongitude = fieldMap[ManufacturerBConstants.LatitudeLongitude],
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
