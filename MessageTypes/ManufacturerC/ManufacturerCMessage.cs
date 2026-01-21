using System;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using FluentValidation;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerC;

public class ManufacturerCMessage : BaseMessageHandler, IMessageType, IMessageHandler
{
    public string DeviceId { get; set; }
    public string VIN { get; set; }
    public ManufacturerCConstants.EventCodeType EventCode { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public double Speed { get; set; }
    public int Direction { get; set; }
    public double? Idletime { get; set; }
    public double? MaxSpeed { get; set; }

    public override int MessageTypeId => 3;


    public static ManufacturerCMessage MapToMessageFromDto(IDto messageBaseDto)
    {
        var manufacturerCMessageDto = (ManufacturerCMessageDto)messageBaseDto;
        
        new ManufacturerCMessageDtoValidator()
            .ValidateAndThrow(manufacturerCMessageDto);
        
        var newMessage = new ManufacturerCMessage
        {
            DeviceId = manufacturerCMessageDto.DeviceId,
            VIN = manufacturerCMessageDto.VIN,
            EventCode = Enum.Parse<ManufacturerCConstants.EventCodeType>(manufacturerCMessageDto.EventCode),
            Latitude = manufacturerCMessageDto.Latitude.ToDouble(),
            Longitude = manufacturerCMessageDto.Longitude.ToDouble(),
            Timestamp = manufacturerCMessageDto.Timestamp.ToDateTimeOffset(),
            Speed = manufacturerCMessageDto.Speed.ToDouble(),
            Direction = manufacturerCMessageDto.Direction.ToInt(),
            Idletime = manufacturerCMessageDto.Idletime.ToInt(),
            MaxSpeed = manufacturerCMessageDto.MaxSpeed!.ToDouble()
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
            SpeedUnits = ManufacturerCConstants.SpeedUnits,
            VIN = VIN
        };

        CombinedMessage = combinedMessage;
        
        return combinedMessage;
    }

    public sealed class ManufacturerCMessageDtoValidator : AbstractValidator<ManufacturerCMessageDto>
    {
        public ManufacturerCMessageDtoValidator()
        {
            RuleFor(msg => msg.VIN)
                .NotEmpty()
                .Must(vin =>
                    vin.Equals(ManufacturerCConstants.VinLocationIdle) ||
                    vin.Equals(ManufacturerCConstants.VinSpeeding))
                .WithMessage("Invalid VIN code");
            RuleFor(msg => msg.DeviceId)
                .Length(4,15)
                .Must(deviceId => deviceId.StartsWith("DEV"))
                .NotNull()
                .WithMessage("Invalid {PropertyName}");
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
                .When(msg => msg.MaxSpeed.IsNotNullOrWhiteSpace())
                .WithMessage("Invalid max speed");
        }

        private static bool IsValidEventCode(string eventCode)
        {
            return int.TryParse(eventCode, out var code) 
                   && Enum.IsDefined(typeof(ManufacturerCConstants.EventCodeType), code);
        }
    }

 
    // public void ParseMessage(Dictionary<int, string> fields)
    // {
    //     var dto = ManufacturerCMessageDto.CreateManufacturerCMessageDto(fields);
    //     // ParsedMessage = MapToMessageFromDto(dto);
    // }

    public override IMessageType GenerateMessage(Dictionary<int, string> fields)
    {
        var dto = ManufacturerCMessageDto.CreateManufacturerCMessageDto(fields);
        // ParsedMessage = MapToMessageFromDto(dto);
        
        return MapToMessageFromDto(dto);
    }

    // private ManufacturerCMessage ParsedMessage { get; set; }
}
