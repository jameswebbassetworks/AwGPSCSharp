using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor.TranslatorClass
{
    internal class Type2TranslatorClass
    {
        public VehicleEvent Translate(Message message)
        {
            var fields = message.Fields;

            return new VehicleEvent
            {
                EventType = MapEvent(fields[1]),
                Timestamp = DateTime.Parse(fields[2]),
                Location = fields[3],
                Speed = fields.ContainsKey(4) ? double.Parse(fields[4]) : null
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
