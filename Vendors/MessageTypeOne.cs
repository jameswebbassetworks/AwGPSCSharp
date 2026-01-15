using System;
using System.Collections.Generic;
using System.Globalization;
using AwGPSCSharp.Domain;
using AwGPSCSharp.Vendors;
using CSharpInterviewMessageProcessor;

namespace AwGPSCSharp.SampleMessages.Vendors
{
    public class MessageTypeOne : IMessage
    {
        // Manufacturer B
        public int MessageType => 1;

        public VehicleEvent Translate(Message msg)
        {
            if (msg == null)
                throw new ArgumentNullException(nameof(msg));

            var evt = new VehicleEvent();

            // ---- Required fields (fail fast if missing) ----
            evt.DeviceId = GetRequired(msg, 1);
            evt.Timestamp = DateTime.Parse(
                GetRequired(msg, 3),
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal
            );

            // Latitude & Longitude (Field 2: "lat,lon")
            var latLon = GetRequired(msg, 2).Split(',');
            if (latLon.Length != 2)
                throw new FormatException("Invalid latitude/longitude format");

            evt.Latitude = double.Parse(latLon[0], CultureInfo.InvariantCulture);
            evt.Longitude = double.Parse(latLon[1], CultureInfo.InvariantCulture);

            // ---- Event code (Field 0) ----
            var eventCode = GetRequired(msg, 0);

            evt.EventType = eventCode switch
            {
                "50" => VehicleEventType.Location,
                "62" => VehicleEventType.SpeedingStart,
                "63" => VehicleEventType.SpeedingEnd,
                "97" => VehicleEventType.IdleStart,
                "98" => VehicleEventType.IdleEnd,
                _ => throw new NotSupportedException($"Unknown event code '{eventCode}' for MessageType 1")
            };

            // ---- Optional fields ----
            if (msg.Fields.TryGetValue(4, out var speed))
                evt.Speed = double.Parse(speed, CultureInfo.InvariantCulture);

            if (msg.Fields.TryGetValue(12, out var idle))
                evt.IdleTimeSeconds = int.Parse(idle, CultureInfo.InvariantCulture);

            return evt;
        }

        private static string GetRequired(Message msg, int fieldNumber)
        {
            if (!msg.Fields.TryGetValue(fieldNumber, out var value))
                throw new KeyNotFoundException($"Required field {fieldNumber} is missing");

            return value;
        }
    }
}
