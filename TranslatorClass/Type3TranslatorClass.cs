using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Interfaces;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor.TranslatorClass
{
    public class Type3Translator : IMessageTranslator<Type3Message>
    {
        public Type3Message Translate(Message message)
        {
            var files = message.Fields;

            return new Type3Message
            {
                VIN = files[0],                                       
                DeviceID = files[1],                                
                Date = files[2],                                      
                Time = files[3],                                      
                Latitude = double.Parse(files[10]),                  
                Longitude = double.Parse(files[11]),                 
                EventCode = int.Parse(files[12]),                   
                Speed = files.ContainsKey(25) ? double.Parse(files[25]) : 0,      
                Direction = files.ContainsKey(26) ? int.Parse(files[26]) : 0,     
                MaxSpeed = files.ContainsKey(27) ? double.Parse(files[27]) : null, 
                IdleTime = files.ContainsKey(30) ? int.Parse(files[30]) : null     
            };
        }
    }
}
