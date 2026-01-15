using System;
using System.Collections.Generic;
using System.Globalization;
using AwGPSCSharp.Domain;
using AwGPSCSharp.Vendors;
using CSharpInterviewMessageProcessor;

namespace AwGPSCSharp.SampleMessages.Vendors
{
    public class MessageTypeThree : IMessage
    {
        // Manufacturer C
        public int MessageType => 3;


        public VehicleEvent Translate(Message msg)
        {
            if (msg == null)
                throw new ArgumentNullException(nameof(msg));

            var evt = new VehicleEvent();

            // -------- Required fields --------
            evt.Vin = GetRequired(msg, 0);
            evt.DeviceId = GetRequired(msg, 1);

            // Date + Time → single DateTime (UTC)
            var date = GetRequired(msg, 2);
            var time = GetRequired(msg, 3);

            evt.Timestamp = DateTime.Parse(
                $"{date} {time}",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            );

            evt.Latitude = double.Parse(
                GetRequired(msg, 10),
                CultureInfo.InvariantCulture
            );

            evt.Longitude = double.Parse(
                GetRequired(msg, 11),
                CultureInfo.InvariantCulture
            );

            // Event code (Field 12)
            var eventCode = GetRequired(msg, 12);

            evt.EventType = eventCode switch
            {
                "15" => VehicleEventType.Location,
                "26" => VehicleEventType.IdleStart,
                "28" => VehicleEventType.IdleEnd,
                "51" => VehicleEventType.SpeedingStart,
                "52" => VehicleEventType.SpeedingEnd,
                _ => throw new NotSupportedException(
                        $"Unknown event code '{eventCode}' for MessageType 3")
            };

            // -------- Optional fields --------

            // Speed (km/h)
            if (msg.Fields.TryGetValue(25, out var speed))
                evt.Speed = double.Parse(speed, CultureInfo.InvariantCulture);

            // Max Speed (optional)
            if (msg.Fields.TryGetValue(27, out var maxSpeed))
                evt.MaxSpeed = double.Parse(maxSpeed, CultureInfo.InvariantCulture);

            // Idle Time (optional)
            if (msg.Fields.TryGetValue(30, out var idle))
                evt.IdleTimeSeconds = int.Parse(idle, CultureInfo.InvariantCulture);

            return evt;
        }

        private static string GetRequired(Message msg, int fieldNumber)
        {
            if (!msg.Fields.TryGetValue(fieldNumber, out var value))
                throw new KeyNotFoundException(
                    $"Required field {fieldNumber} is missing for MessageType 3");

            return value;
        }
    }
}
