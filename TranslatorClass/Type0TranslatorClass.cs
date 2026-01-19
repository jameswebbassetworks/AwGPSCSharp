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
    /// Translator class for Type0Message
    /// </summary>
    public class Type0TranslatorClass : IMessageTranslator<Type0Message>
    {

        public Type0Message Translate(Message message)
        {
            var fields = message.Fields;

            return new Type0Message
            {
                DeviceID = fields[0],                    
                EventCode = fields[1]        ,                  
                Latitude = fields[4],                               
                Longitude = fields[5],                             
                Timestamp = DateTime.Parse(fields[6]),             
                Speed = fields.ContainsKey(7) ? double.Parse(fields[7]) : (double?)null,  
                Direction = fields.ContainsKey(8) ? fields[8] : string.Empty,             
                IdleTime = fields.ContainsKey(9) ? int.Parse(fields[9]) : (int?)null,      
                MaxSpeed = fields.ContainsKey(10) ? double.Parse(fields[10]) : (double?)null 
            };
        }
    }
}
