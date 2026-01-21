using System;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using FluentValidation;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerA;

public class ManufacturerAMessage : BaseMessageGenerator, IMessageType, IMessageGenerator
{
    private string DeviceId { get; init; }
    private EventCodeType EventCode { get; init; }
    private double Latitude { get; init; }
    private double Longitude { get; init; }
    private DateTimeOffset Timestamp { get; init; }
    private double Speed { get; init; }
    private int Direction { get; set; }
    private double? Idletime { get; init; }
    private double? MaxSpeed { get; init; }

    public override int MessageTypeId => 0;


    private static ManufacturerAMessage MapToMessageFromDto(IDto messageBaseDto)
    {
        var manufacturerAMessageDto = (ManufacturerAMessageDto)messageBaseDto;
        
        new ManufacturerAMessageDtoValidator()
            .ValidateAndThrow(manufacturerAMessageDto);

        var newMessage = new ManufacturerAMessage
        {
            DeviceId = manufacturerAMessageDto.DeviceId,
            EventCode = Enum.Parse<EventCodeType>(manufacturerAMessageDto.EventCode),
            Latitude = manufacturerAMessageDto.Latitude.ToDouble(),
            Longitude = manufacturerAMessageDto.Longitude.ToDouble(),
            Timestamp = manufacturerAMessageDto.Timestamp.ToDateTimeOffset(),
            Speed = manufacturerAMessageDto.Speed.ToDouble(),
            Direction = manufacturerAMessageDto.Direction.ToInt(),
            Idletime = manufacturerAMessageDto.Idletime.ToInt(),
            MaxSpeed = manufacturerAMessageDto.MaxSpeed!.ToDouble()
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
            SpeedUnits = ManufacturerAConstants.SpeedUnits
        };
        
        return combinedMessage;
    }

    public override IMessageType GenerateMessage(Dictionary<int, string> fields)
    {
        var dto = ManufacturerAMessageDto.CreateManufacturerAMessageDto(fields);
        
        return MapToMessageFromDto(dto);
    }
}
