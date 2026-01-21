using System;
using System.Collections.Generic;
using System.Linq;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using FluentValidation;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerB;

public class ManufacturerBMessage : BaseMessageHandler, IMessageType, IMessageHandler
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
            SpeedUnits = ManufacturerBConstants.SpeedUnits
        };

        CombinedMessage = combinedMessage;
        
        return combinedMessage;
    }

    public sealed class ManufacturerBMessageDtoValidator : AbstractValidator<ManufacturerBMessageDto>
    {
        public ManufacturerBMessageDtoValidator()
        {
            RuleFor(msg => msg.DeviceId)
                .Length(4,15)
                .Must(deviceId => deviceId.StartsWith("DEV"))
                .NotNull();
            RuleFor(msg => msg.EventCode)
                .NotNull()
                .Must(IsValidEventCode)
                .WithMessage("Invalid event code");
            RuleFor(msg => msg.LatitudeLongitude)
                .Must(latlong => latlong.Count(ll => ll == ',') == 1)
                .WithMessage("Invalid Latitude and Longitude (commas): {PropertyValue}")
                .Must(latlong =>
                {
                    var splitArray = latlong.Split(',');
                    var lat = splitArray[0];
                    var lng = splitArray[1];
                    return double.TryParse(lat, out _) && double.TryParse(lng, out _);
                })
                .WithMessage("Invalid Latitude and Longitude: {PropertyValue}")
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
        }

        private static bool IsValidEventCode(string eventCode)
        {
            return int.TryParse(eventCode, out var code) 
                   && Enum.IsDefined(typeof(EventCodeType), code);
        }
    }

 
    // public void ParseMessage(Dictionary<int, string> fields)
    // {
    //     var dto = ManufacturerBMessageDto.CreateManufacturerBMessageDto(fields);
    //     // ParsedMessage = MapToMessageFromDto(dto);
    // }

    public override IMessageType GenerateMessage(Dictionary<int, string> fields)
    {
        var dto = ManufacturerBMessageDto.CreateManufacturerBMessageDto(fields);
        // ParsedMessage = MapToMessageFromDto(dto);
        
        return MapToMessageFromDto(dto);
    }

    // private ManufacturerBMessage ParsedMessage { get; set; }
}
