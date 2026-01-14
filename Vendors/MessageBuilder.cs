using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
uint 



namespace AwGPSCSharp.Vendors
{
    public Enum EventCode
    {
        LOCATION, START_SPEEDING, END_SPEEDING, IDLE_START, IDLE_END
    }
    
}
    public class MessageBuilder
    {

 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;
 public string DeviceID {get; set;} = string.Empty;


        //responseible for instances creation 

        /*
        ### Type 0 Messages (Manufacturer A)
- Field 0: Device ID
- Field 1: Event Code (1=location, 2=start speeding, 3=end speeding, 4=idle start, 5=idle end)
- Field 4: Latitude
- Field 5: Longitude
- Field 6: Timestamp
- Field 7: Speed (m/s)
- Field 8: Direction
- Field 9: Idle time (optional)?
- Field 10: Max Speed (optional)

### Type 1 Messages (Manufacturer B)
- Field 0: Event Code (50=location, 62=start speeding, 63=end speeding, 97=idle start, 98=idle end)
- Field 1: Device ID
- Field 2: Latitude, Longitude (comma-separated)
- Field 3: Timestamp
- Field 4: Speed (m/s)
- Field 5: Direction
- Field 12: Idle time (optional)

### Type 3 Messages (Manufacturer C)
- Field 0: VIN (uses two different VINs: `19XFB2F52DE079283` for location/idle events, `1FT7W2BT2EEB76476` for speeding events)
- Field 1: Device ID
- Field 2: Date
- Field 3: Time
- Field 10: Latitude
- Field 11: Longitude
- Field 12: Event Code (15=location, 26=idle start, 28=idle end, 51=start speeding, 52=end speeding)
- Field 25: Speed (km/h)
- Field 26: Direction
- Field 27: Max Speed (optional)
- Field 30: Idle time (optional)

        */
    }
}