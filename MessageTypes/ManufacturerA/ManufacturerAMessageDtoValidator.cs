using System;
using CSharpInterviewMessageProcessor.Extensions;
using FluentValidation;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerA;

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
