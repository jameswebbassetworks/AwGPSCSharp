using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor.TranslatorClass
{
    internal class Type1TranslatorClass
    {
        public VehicleEvent Translate(Message message)
        {
            var fields = message.Fields;

            return new VehicleEvent
            {
                DeviceID = (int)fields[1],
                EventCode = MapEvent(fields[1]),
                Latitude = MapEvent(fields[1]),
                Longitude = MapEvent(fields[1]),
                Timestamp = DateTime.Parse(fields[2]),
                Speed = MapEvent(fields[1]),
                Direction = MapEvent(fields[1]),
                IdleTime = MapEvent(fields[1]),
                MaxSpeed = fields.ContainsKey(4) ? double.Parse(fields[4]) : null
            };
        }
        private string MapEvent(string code) =>
       code switch
       {
           "1" => "Location",
           "2" => "SpeedingStart",
           "3" => "SpeedingStop",
           "4" => "IdleStart",
           "5" => "IdleEnd",
           _ => "Unknown"
       };

    }
}
