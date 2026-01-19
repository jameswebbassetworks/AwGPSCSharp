using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.Models
{
    public class Type0Message
    {
        public string DeviceID { get; set; }
        public string EventCode { get; set; } = string.Empty;
        public string Latitude { get; set; }

        public string Longitude { get; set; } = string.Empty;

        public DateTime Timestamp { get; set; }
       

        public double? Speed { get; set; }

        public string Direction { get; set; } = string.Empty;
        public int? IdleTime { get; set; }
        public double? MaxSpeed { get; set; }        

    }
}
