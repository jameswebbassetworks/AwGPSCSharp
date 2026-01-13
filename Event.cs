using System;

namespace CSharpInterviewMessageProcessor
{
    public class Event
    {
        public Enum EventType { get; set; }
        public string Timestamp { get; set; }
        public string LocationInformation { get; set; }
        public string? CurrentSpeed { get; set; }
        public bool DisplayCurrentSpeed { get; set; }
        public string? MaxSpeed { get; set; }
        public bool DisplayMaxSpeed { get; set; }
        public string? IdleTime { get; set; }
        public bool DisplayIdleTime { get; set; }
        public string? VIN { get; set; }

        public enum EventTypeCode
        {
            Location,
            SpeedingStart,
            SpeedingStop,
            IdleStart,
            IdleEnd
        }

        public Event()
        {
            DisplayCurrentSpeed = false;
            DisplayMaxSpeed = false;
            DisplayIdleTime = false;
        }

        override public string ToString()
        {
            string output = $"Event Type: {EventType}, Timestamp: {Timestamp}, Location: {LocationInformation}";

            if (CurrentSpeed != null && DisplayCurrentSpeed)
            {
                output += $", Current Speed: {CurrentSpeed}";
            }
            if (MaxSpeed != null && DisplayMaxSpeed)
            {
                output += $", Max Speed: {MaxSpeed}";
            }
            if (IdleTime != null && DisplayIdleTime)
            {
                output += $", Idle Time: {IdleTime}";
            }
            if (VIN != null)
            {
                output += $", {VIN}";
            }

            return output;
        }
    }

    
}
