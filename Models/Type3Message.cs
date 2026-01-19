using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.Models
{
    public class Type3Message
    {
        // Required fields
        public string VIN { get; set; }             // Field 0
        public string DeviceID { get; set; }        // Field 1
        public string Date { get; set; }            // Field 2 (yyyy-MM-dd)
        public string Time { get; set; }            // Field 3 (HH:mm:ss)
        public double Latitude { get; set; }        // Field 10
        public double Longitude { get; set; }       // Field 11
        public int EventCode { get; set; }          // Field 12
        public double Speed { get; set; }           // Field 25
        public int Direction { get; set; }          // Field 26

        // Optional fields
        public double? MaxSpeed { get; set; }       // Field 27
        public int? IdleTime { get; set; }          // Field 30

   
      
    }
}
