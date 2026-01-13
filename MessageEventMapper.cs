using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static CSharpInterviewMessageProcessor.Event;

namespace CSharpInterviewMessageProcessor
{
    public static class MessageEventMapper
    {
        public static Dictionary<int, Dictionary<int, EventTypeCode>> MessageTypeToEvents { get; set; }

        #region Mapping Dictionaries
        public static Dictionary<int, int> EventTypeFieldLocationByMessageType { get; set; } = new Dictionary<int, int>
        {
            { 0, 1 },
            { 1, 0 },
            { 3, 12 }
        };

        public static Dictionary<int, int[]> LocationFieldLocationByMessageType { get; set; } = new Dictionary<int, int[]>
        {
            { 0, [4,5] },
            { 1, [2] },
            { 3, [10,11] }
        };

        public static Dictionary<int, int[]> TimestampFieldLocationByMessageType { get; set; } = new Dictionary<int, int[]>
        {
            { 0, [6] },
            { 1, [3] },
            { 3, [2,3] }
        };

        public static Dictionary<int, int> CurrentSpeedFieldLocationByMessageType { get; set; } = new Dictionary<int, int>
        {
            { 0, 7 },
            { 1, 4 },
            { 3, 25 }
        };

        public static Dictionary<int, int> MaxSpeedFieldLocationByMessageType { get; set; } = new Dictionary<int, int>
        {
            { 3, 27 }
        };

        public static Dictionary<int, int> IdleTimeFieldLocationByMessageType { get; set; } = new Dictionary<int, int>
        {
            { 0, 9 },
            { 1, 12 },
            { 3, 30 }
        };

        public static Dictionary<int, int> VinFieldLocationByMessageType { get; set; } = new Dictionary<int, int>
        {
            { 3, 0 }
        };

        public static Dictionary<int, EventTypeCode> MessageType0Events { get; set; } = new Dictionary<int, EventTypeCode>
        {
            { 1, EventTypeCode.Location },
            { 2, EventTypeCode.SpeedingStart },
            { 3, EventTypeCode.SpeedingStop },
            { 4, EventTypeCode.IdleStart },
            { 5, EventTypeCode.IdleEnd }
        };

        public static Dictionary<int, EventTypeCode> MessageType1Events { get; set; } = new Dictionary<int, EventTypeCode>
        {
            { 50, EventTypeCode.Location },
            { 62, EventTypeCode.SpeedingStart },
            { 63, EventTypeCode.SpeedingStop },
            { 97, EventTypeCode.IdleStart },
            { 98, EventTypeCode.IdleEnd  }
        };
        public static Dictionary<int, EventTypeCode> MessageType3Events { get; set; } = new Dictionary<int, EventTypeCode>
        {
            { 15, EventTypeCode.Location },
            { 51, EventTypeCode.SpeedingStart },
            { 52, EventTypeCode.SpeedingStop },
            { 26, EventTypeCode.IdleStart },
            { 28, EventTypeCode.IdleEnd  }
        };

        public static Dictionary<EventTypeCode, string[]> VisibleInfoByEventType { get; set; } = new Dictionary<EventTypeCode, string[]>
        {
            { EventTypeCode.Location, [] },
            { EventTypeCode.SpeedingStart, [Constants.Display.CurrentSpeed] },
            { EventTypeCode.SpeedingStop, [Constants.Display.CurrentSpeed, Constants.Display.MaxSpeed] },
            { EventTypeCode.IdleStart, [] },
            { EventTypeCode.IdleEnd, [Constants.Display.CurrentSpeed, Constants.Display.IdleTime] }
        };
        #endregion

        #region Mapping Methods
        public static Event MapEventType(Message message, Event ev)
        {

            MessageTypeToEvents = new Dictionary<int, Dictionary<int, EventTypeCode>>
            {
                { 0, MessageType0Events },
                { 1, MessageType1Events },
                { 3, MessageType3Events }
            };

            if (EventTypeFieldLocationByMessageType.ContainsKey(message.MessageType))
            {
                int eventTypeFieldLocation = EventTypeFieldLocationByMessageType[message.MessageType];

                ev.EventType = MessageTypeToEvents[message.MessageType][int.Parse(message.Fields[eventTypeFieldLocation])];
            }

            return ev;
        }

        public static Event MapLocation(Message message, Event ev)
        {
            if (LocationFieldLocationByMessageType.ContainsKey(message.MessageType))
            {
                int[] locationFieldLocation = LocationFieldLocationByMessageType[message.MessageType];

                if (locationFieldLocation.Length > 1)
                {
                    ev.LocationInformation = message.Fields[locationFieldLocation[0]] + "," + message.Fields[locationFieldLocation[1]];
                }
                else
                {
                    ev.LocationInformation = message.Fields[locationFieldLocation[0]];
                }
            }

            return ev;
        }

        public static Event MapTimestamp(Message message, Event ev)
        {
            if (TimestampFieldLocationByMessageType.ContainsKey(message.MessageType))
            {
                int[] timestampFieldLocation = TimestampFieldLocationByMessageType[message.MessageType];

                if (timestampFieldLocation.Length > 1)
                {
                    ev.Timestamp = message.Fields[timestampFieldLocation[0]] + "T" + message.Fields[timestampFieldLocation[1]];
                }
                else
                {
                    ev.Timestamp = message.Fields[timestampFieldLocation[0]];
                }
            }

            return ev;
        }

        public static Event MapCurrentSpeed(Message message, Event ev)
        {
            if (CurrentSpeedFieldLocationByMessageType.ContainsKey(message.MessageType))
            {
                int currentSpeedFieldLocation = CurrentSpeedFieldLocationByMessageType[message.MessageType];

                ev.CurrentSpeed = message.Fields[currentSpeedFieldLocation];
            }

            return ev;
        }

        public static Event MapMaxSpeed(Message message, Event ev)
        {

            
            if (MaxSpeedFieldLocationByMessageType.ContainsKey(message.MessageType))
            {
                MaxSpeedFieldLocationByMessageType.TryGetValue(message.MessageType, out int maxSpeedFieldLocation);

                message.Fields.TryGetValue(maxSpeedFieldLocation, out string maxSpeedValue);

                if (maxSpeedValue != null)
                {
                    ev.MaxSpeed = maxSpeedValue;
                }
            }
           
            return ev;
        }

        public static Event MapIdleTime(Message message, Event ev)
        {

            if (IdleTimeFieldLocationByMessageType.ContainsKey(message.MessageType))
            {
                IdleTimeFieldLocationByMessageType.TryGetValue(message.MessageType, out int idleTimeFieldLocation);

                message.Fields.TryGetValue(idleTimeFieldLocation, out string idleTimeValue);

                if (idleTimeValue != null)
                {
                    ev.IdleTime = idleTimeValue;
                }
            }

            return ev;
        }

        public static Event MapVIN(Message message, Event ev)
        {
            if (VinFieldLocationByMessageType.ContainsKey(message.MessageType))
            {
                VinFieldLocationByMessageType.TryGetValue(message.MessageType, out int vinFieldLocation);

                message.Fields.TryGetValue(vinFieldLocation, out string vinValue);

                if (vinValue != null)
                {
                    ev.VIN = GetVinDisplayValue(vinValue);
                }
            }

            return ev;
        }

        #endregion

        //Set any values that we will display for the current event type to true
        public static void SetDisplayValues(Event ev)
        {
            var arrayOfVisibleInfo = VisibleInfoByEventType[(EventTypeCode)ev.EventType];

            foreach (string propertyName in arrayOfVisibleInfo)
            {
                var property = ev.GetType().GetProperty(propertyName);
                property.SetValue(ev, true);
            }
        }

        private static string? GetVinDisplayValue(string vinValue)
        {
            WebRequestHelper webRequestHelper = new WebRequestHelper();

            string requestUrl = $"https://vpic.nhtsa.dot.gov/api/vehicles/decodevinvalues/{vinValue}?format=json";
            string formattedVin = string.Empty;

            try
            {
                string response = webRequestHelper.Get(requestUrl);

                JObject jsonResponse = JObject.Parse(response);
                JArray resultsArray = (JArray)jsonResponse[Constants.Vin.Results];

                if (resultsArray != null && resultsArray.Count > 0)
                {
                    JObject firstResult = (JObject)resultsArray[0];
                    string? make = firstResult[Constants.Vin.Make]?.ToString();
                    string? model = firstResult[Constants.Vin.Model]?.ToString();
                    string? year = firstResult[Constants.Vin.ModelYear]?.ToString();
                    string? fuelType = firstResult[Constants.Vin.PrimaryFuelType]?.ToString();

                    formattedVin = $"Model Year: {year}, Make: {make}, Model: {model}, Fuel Type - Primary: {fuelType}";
                }
            }
            catch (Exception)
            {
                formattedVin = vinValue;
            }



            return formattedVin;
        }
    }
}
