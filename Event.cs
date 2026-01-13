using System;
using System.Collections.Generic;

namespace CSharpInterviewMessageProcessor
{
    internal class Event
    {
        public Enum EventType { get; set; }
        public string Timestamp { get; set; }
        public string LocationInformation { get; set; }
        public string? CurrentSpeed { get; set; }
        public string? MaxSpeed { get; set; }
        public string? IdleTime { get; set; }
        public string? VIN { get; set; }

        public enum EventTypeCode
        {
            Location,
            SpeedingStart,
            SpeedingStop,
            IdleStart,
            IdleEnd
        }

        public Dictionary<int, Dictionary<int, EventTypeCode>> MessageTypeToEvents { get; set; }

        public Dictionary<int, int> EventTypeFieldLocationByMessageType { get; set; } = new Dictionary<int, int>
        {
            { 0, 1},
            { 1, 0 },
            { 3, 12 }
        };

        public Dictionary<int, EventTypeCode> MessageType0Events { get; set; } = new Dictionary<int, EventTypeCode>
        {
            { 1, EventTypeCode.Location },
            { 2, EventTypeCode.SpeedingStart },
            { 3, EventTypeCode.SpeedingStop },
            { 4, EventTypeCode.IdleStart },
            { 5, EventTypeCode.IdleEnd }
        };
        public Dictionary<int, EventTypeCode> MessageType1Events { get; set; } = new Dictionary<int, EventTypeCode>
        {
            { 50, EventTypeCode.Location },
            { 62, EventTypeCode.SpeedingStart },
            { 63, EventTypeCode.SpeedingStop },
            { 97, EventTypeCode.IdleStart },
            { 98, EventTypeCode.IdleEnd  }
        };
        public Dictionary<int, EventTypeCode> MessageType3Events { get; set; } = new Dictionary<int, EventTypeCode>
        {
            { 15, EventTypeCode.Location },
            { 51, EventTypeCode.SpeedingStart },
            { 52, EventTypeCode.SpeedingStop },
            { 26, EventTypeCode.IdleStart },
            { 28, EventTypeCode.IdleEnd  }
        };
        
        public Event(Message message)
        {
            MessageTypeToEvents = new Dictionary<int, Dictionary<int, EventTypeCode>>
            {
                { 0, MessageType0Events },
                { 1, MessageType1Events },
                { 3, MessageType3Events }
            };

            int eventTypeFieldLocation = EventTypeFieldLocationByMessageType[message.MessageType];

            MapEventData(message, eventTypeFieldLocation, ) // Needs additional mapping of properties by message type/GPS manufacturer

        }

        public void MapEventData(Message message, int eventTypeFieldLocation, )
        {
            // Implement mapping of data to object properties based on event type & message type
        }
    }

    
}
