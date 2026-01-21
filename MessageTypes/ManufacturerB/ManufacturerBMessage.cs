using System;
using System.Collections.Generic;
using System.Linq;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using FluentValidation;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerB;

public class ManufacturerBMessage : BaseMessageGenerator, IMessageType, IMessageGenerator
{
    private string DeviceId { get; init; }
    private EventCodeType EventCode { get; init; }
    private double Latitude { get; init; }
    private double Longitude { get; init; }
    private DateTimeOffset Timestamp { get; init; }
    private double Speed { get; init; }
    public int Direction { get; set; }
    private double? Idletime { get; init; }
    public double? MaxSpeed { get; set; }

    public override int MessageTypeId => 1;


    private static ManufacturerBMessage MapToMessageFromDto(IDto messageBaseDto)
    {
        var manufacturerBMessageDto = (ManufacturerBMessageDto)messageBaseDto;
        
        new ManufacturerBMessageDtoValidator()
            .ValidateAndThrow(manufacturerBMessageDto);
        
        // Longitude and Latitude are together in one field comma separated
        var latitudeLongitudeArray = manufacturerBMessageDto.LatitudeLongitude.Split(",");
        var latitude = latitudeLongitudeArray[0];
        var longitude = latitudeLongitudeArray[1];
        
        var newMessage = new ManufacturerBMessage
        {
            DeviceId = manufacturerBMessageDto.DeviceId,
            EventCode = Enum.Parse<EventCodeType>(manufacturerBMessageDto.EventCode),
            Latitude = latitude.ToDouble(),
            Longitude = longitude.ToDouble(),
            Timestamp = manufacturerBMessageDto.Timestamp.ToDateTimeOffset(),
            Speed = manufacturerBMessageDto.Speed.ToDouble(),
            Direction = manufacturerBMessageDto.Direction.ToInt(),
            Idletime = manufacturerBMessageDto.Idletime.ToInt(),
        };

        return newMessage;
    }

    public CombinedMessage ToCombinedMessage()
    {
        var combinedMessage = new CombinedMessage
        {
            DeviceId = DeviceId,
            EventCodeName = Enum.GetName(EventCode)!,
            Latitude = Latitude,
            Longitude = Longitude,
            Timestamp = Timestamp,
            Date = DateOnly.FromDateTime(Timestamp.DateTime),
            Time = TimeOnly.FromDateTime(Timestamp.DateTime),
            Speed = Speed,
            MaxSpeed = MaxSpeed,
            Idletime = Idletime,
            SpeedUnits = ManufacturerBConstants.SpeedUnits
        };

        return combinedMessage;
    }


    public override IMessageType GenerateMessage(Dictionary<int, string> fields)
    {
        var dto = ManufacturerBMessageDto.CreateManufacturerBMessageDto(fields);
        
        return MapToMessageFromDto(dto);
    }

}
