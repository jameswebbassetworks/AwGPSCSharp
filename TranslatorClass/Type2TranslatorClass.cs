using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Interfaces;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor.TranslatorClass
{
    internal class Type2TranslatorClass :  IMessageTranslator<Type2Message>
    {
        public Type2Message Translate(Message message)
        {
            var files = message.Fields;

            // Parse latitude and longitude from field 2 if in "lat,lon" format
            string latitude = "";
            string longitude = "";
            if (files.ContainsKey(2) && files[2].Contains(","))
            {
                var parts = files[2].Split(',');
                latitude = parts[0];
                longitude = parts[1];
            }

            return new Type2Message
            {
                DeviceID = int.Parse(files[0]),                          
                EventCode = files[1], 
                Latitude = latitude,                                  
                Longitude = longitude,                                
                Timestamp = DateTime.Parse(files[3]),                     
                Speed = files.ContainsKey(4) ? double.Parse(files[4]) : null,
                Direction = files.ContainsKey(5) ? files[5] : string.Empty,  
                IdleTime = files.ContainsKey(6) ? int.Parse(files[6]) : null,
                MaxSpeed = files.ContainsKey(7) ? double.Parse(files[7]) : null 
            };
        }

    }
}
