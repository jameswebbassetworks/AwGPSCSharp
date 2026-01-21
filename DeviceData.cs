using System;

namespace CSharpInterviewMessageProcessor
{
    public class DeviceData
    {
        public string VIN { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Speed { get; set; }
        public double? MaxSpeed { get; set; }
        public double? IdleTime { get; set; }
        public DateTime Timestamp { get; set; }
        public string EventCode { get; set; }
    }
}