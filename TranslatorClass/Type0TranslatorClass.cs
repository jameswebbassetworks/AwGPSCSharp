using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Interfaces;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor.TranslatorClass
{
    public class Type0TranslatorClass : IMessageTranslator
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
           "0" => "Location",
           "1" => "SpeedingStart",
           "3" => "SpeedingStop",
           "4" => "IdleStart",
           "5" => "IdleEnd",
           _ => "Unknown"
       };

    }
}
