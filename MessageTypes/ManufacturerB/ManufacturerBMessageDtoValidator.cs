using System;
using System.Linq;
using CSharpInterviewMessageProcessor.Extensions;
using FluentValidation;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerB;

public sealed class ManufacturerBMessageDtoValidator : AbstractValidator<ManufacturerBMessageDto>
{
    public ManufacturerBMessageDtoValidator()
    {
        RuleFor(msg => msg.DeviceId)
            .Length(4,15)
            .Must(deviceId => deviceId.StartsWith("DEV"))
            .WithMessage("Invalid {PropertyName}: {PropertyValue}")
            .NotNull();
        RuleFor(msg => msg.EventCode)
            .NotNull()
            .Must(IsValidEventCode)
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");
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
            .WithMessage("Invalid {PropertyName}: {PropertyValue}")
            .NotNull();
        RuleFor(msg => msg.Speed)
            .Must(speed => double.TryParse(speed, out var parsedValue) && parsedValue >= 0)
            .WithMessage("Invalid {PropertyName}: {PropertyValue}")
            .NotNull();
        RuleFor(msg => msg.Direction)
            .Must(direction => int.TryParse(direction, out _))
            .WithMessage("Invalid {PropertyName}: {PropertyValue}")
            .NotNull();
        RuleFor(msg => msg.Idletime)
            .Must(idleTime => double.TryParse(idleTime, out _))
            .When(msg => msg.Idletime.IsNotNullOrWhiteSpace())
            .WithMessage("Invalid {PropertyName}: {PropertyValue}");
    }

    private static bool IsValidEventCode(string eventCode)
    {
        return int.TryParse(eventCode, out var code) 
               && Enum.IsDefined(typeof(EventCodeType), code);
    }
}
