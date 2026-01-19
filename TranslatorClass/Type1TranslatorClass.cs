using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Interfaces;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor.TranslatorClass
{

    /// <summary>
    /// Type1TranslatorClass translates Message to Type1Message
    /// </summary>
    internal class Type1TranslatorClass : IMessageTranslator<Type1Message>
    {
        public Type1Message Translate(Message message)
        {
            var fields = message.Fields;

            // Location is in field 2 as "lat,lon"
            string latitude = "";
            string longitude = "";
            if (fields.ContainsKey(2) && fields[2].Contains(","))
            {
                var parts = fields[2].Split(',');
                latitude = parts[0];
                longitude = parts[1];
            }

            return new Type1Message
            {
                EventCode = fields[0],                
                DeviceID = fields[1],                           
                Longitude = longitude,                     
                Timestamp = DateTime.Parse(fields[3]),          
                Speed = fields.ContainsKey(4) ? double.Parse(fields[4]) : (double?)null,  
                Direction = fields.ContainsKey(5) ? fields[5] : string.Empty,              
                IdleTime = fields.ContainsKey(12) ? int.Parse(fields[12]) : (int?)null     
            };
        }
    }
}
