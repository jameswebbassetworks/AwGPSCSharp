using System;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using FluentValidation;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerA;

public class ManufacturerAMessage : BaseMessageHandler, IMessageType, IMessageHandler
{
    public string DeviceId { get; set; }
    public EventCodeType EventCode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public double Speed { get; set; }
    public int Direction { get; set; }
    public double? Idletime { get; set; }
    public double? MaxSpeed { get; set; }

    protected override int MessageTypeId => 0;

    
    public static ManufacturerAMessage MapToMessageFromDto(IDto messageBaseDto)
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
            Speed = manufacturerAMessageDto.Speed.ToInt(),
            Direction = manufacturerAMessageDto.Direction.ToInt(),
            Idletime = manufacturerAMessageDto.Idletime.ToInt(),
            MaxSpeed = manufacturerAMessageDto.MaxSpeed.ToInt()
        };

        return newMessage;
    }

    public CombinedMessage CombinedMessage { get; set; }

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

        CombinedMessage = combinedMessage;
        
        return combinedMessage;
    }

    public sealed class ManufacturerAMessageDtoValidator : AbstractValidator<ManufacturerAMessageDto>
    {
        public ManufacturerAMessageDtoValidator()
        {
            RuleFor(msg => msg.DeviceId)
                .Length(4,15)
                .Must(deviceId => deviceId.StartsWith("DEV"))
                .NotNull();
            RuleFor(msg => msg.EventCode)
                .NotNull()
                .Must(IsValidEventCode)
                .WithMessage("Invalid event code");
            RuleFor(msg => msg.Latitude)
                .Must(latitude => double.TryParse(latitude, out _))
                .NotNull();
            RuleFor(msg => msg.Longitude)
                .Must(longitude => double.TryParse(longitude, out _))
                .NotNull();
            RuleFor(msg => msg.Timestamp)
                .Must(timestamp => DateTimeOffset.TryParse(timestamp, out _))
                .NotNull();
            RuleFor(msg => msg.Speed)
                .NotNull();
            RuleFor(msg => msg.Direction)
                .Must(direction => int.TryParse(direction, out _))
                .NotNull();
            RuleFor(msg => msg.Idletime)
                .Must(idleTime => double.TryParse(idleTime, out _))
                .When(msg => msg.Idletime.IsNotNullOrWhiteSpace())
                .WithMessage("Invalid idle time");
            RuleFor(msg => msg.MaxSpeed)
                .Must(maxSpeed => double.TryParse(maxSpeed, out _))
                .When(msg => msg.MaxSpeed.IsNotNullOrWhiteSpace());
        }

        private static bool IsValidEventCode(string eventCode)
        {
            return int.TryParse(eventCode, out var code) 
                   && Enum.IsDefined(typeof(EventCodeType), code);
        }
    }

 
    // public void ParseMessage(Dictionary<int, string> fields)
    // {
    //     var dto = ManufacturerAMessageDto.CreateManufacturerAMessageDto(fields);
    //     // ParsedMessage = MapToMessageFromDto(dto);
    // }

    public override IMessageType GenerateMessage(Dictionary<int, string> fields)
    {
        var dto = ManufacturerAMessageDto.CreateManufacturerAMessageDto(fields);
        // ParsedMessage = MapToMessageFromDto(dto);
        
        return MapToMessageFromDto(dto);
    }

    // private ManufacturerAMessage ParsedMessage { get; set; }
}
