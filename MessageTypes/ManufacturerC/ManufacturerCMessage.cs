using System;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.Extensions;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using FluentValidation;
using FluentValidation.Results;

namespace CSharpInterviewMessageProcessor.MessageTypes.ManufacturerC;

public class ManufacturerCMessage : BaseMessageGenerator, IMessageType, IMessageGenerator
{
    private string DeviceId { get; init; }
    private string VIN { get; init; }
    private ManufacturerCConstants.EventCodeType EventCode { get; init; }
    private double Latitude { get; init; }
    private double Longitude { get; init; }
    private DateTimeOffset Timestamp { get; init; }
    private double Speed { get; init; }
    public int Direction { get; set; }
    private double? Idletime { get; init; }
    private double? MaxSpeed { get; init; }

    public override int MessageTypeId => 3;
    public override bool HasErrors { get; set; }
    public override ValidationResult ValidationResults { get; set; }


    public static ManufacturerCMessage MapToMessageFromDto(IDto messageBaseDto)
    {
        var manufacturerCMessageDto = (ManufacturerCMessageDto)messageBaseDto;
        
        var validationResults = new ManufacturerCMessageDtoValidator()
            .Validate(manufacturerCMessageDto);

        if (validationResults.IsValid.IsFalse())
        {
            return new ManufacturerCMessage
            {
                HasErrors = validationResults.IsValid.IsFalse(), 
                ValidationResults = validationResults
            };
        }
        
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

        return combinedMessage;
    }

 

    public override IMessageType GenerateMessage(Dictionary<int, string> fields)
    {
        var dto = ManufacturerCMessageDto.CreateManufacturerCMessageDto(fields);
        
        return MapToMessageFromDto(dto);
    }
}
