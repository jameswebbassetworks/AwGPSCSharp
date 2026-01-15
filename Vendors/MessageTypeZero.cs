using System;
using System.Collections.Generic;
using System.Globalization;
using AwGPSCSharp.Domain;
using AwGPSCSharp.Vendors;
using CSharpInterviewMessageProcessor;

namespace AwGPSCSharp.SampleMessages.Vendors
{
    public class MessageTypeZero : IMessage
    {
        // Manufacturer A
        public int MessageType => 0;

        public VehicleEvent Translate(Message msg)
        {
            if (msg == null)
                throw new ArgumentNullException(nameof(msg));

            var evt = new VehicleEvent();

            // -------- Required fields --------
            evt.DeviceId = GetRequired(msg, 0);

            evt.Timestamp = DateTime.Parse(
                GetRequired(msg, 6),
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            );

            evt.Latitude = double.Parse(
                GetRequired(msg, 4),
                CultureInfo.InvariantCulture
            );

            evt.Longitude = double.Parse(
                GetRequired(msg, 5),
                CultureInfo.InvariantCulture
            );

            // Event code (Field 1)
            var eventCode = GetRequired(msg, 1);

            evt.EventType = eventCode switch
            {
                "1" => VehicleEventType.Location,
                "2" => VehicleEventType.SpeedingStart,
                "3" => VehicleEventType.SpeedingEnd,
                "4" => VehicleEventType.IdleStart,
                "5" => VehicleEventType.IdleEnd,
                _ => throw new NotSupportedException(
                        $"Unknown event code '{eventCode}' for MessageType 0")
            };

            // -------- Optional fields --------
            if (msg.Fields.TryGetValue(7, out var speed))
                evt.Speed = double.Parse(speed, CultureInfo.InvariantCulture);

            if (msg.Fields.TryGetValue(10, out var maxSpeed))
                evt.MaxSpeed = double.Parse(maxSpeed, CultureInfo.InvariantCulture);

            if (msg.Fields.TryGetValue(9, out var idle))
                evt.IdleTimeSeconds = int.Parse(idle, CultureInfo.InvariantCulture);

            return evt;
        }

        private static string GetRequired(Message msg, int fieldNumber)
        {
            if (!msg.Fields.TryGetValue(fieldNumber, out var value))
                throw new KeyNotFoundException(
                    $"Required field {fieldNumber} is missing for MessageType 0");

            return value;
        }
    }
}
