# Sample Messages

This folder contains sample XML messages for testing the Vehicle Message Processor.

## Message Types

Each message type represents messages from a different GPS device manufacturer. Each manufacturer uses different field mappings and event codes for the same types of events.

### Type 0 Messages (Manufacturer A)
- Field 0: Device ID
- Field 1: Event Code (1=location, 2=start speeding, 3=end speeding, 4=idle start, 5=idle end)
- Field 4: Latitude
- Field 5: Longitude
- Field 6: Timestamp
- Field 7: Speed (m/s)
- Field 8: Direction
- Field 9: Idle time (optional)
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

## Files

- `all-messages.xml` - Combined file with all 15 sample messages for testing multiple message processing
- `type0-location.xml` - Type 0 GPS location update
- `type0-speeding-start.xml` - Type 0 speeding event start
- `type0-speeding-end.xml` - Type 0 speeding event end
- `type0-idle-start.xml` - Type 0 idle start
- `type0-idle-end.xml` - Type 0 idle end
- `type1-location.xml` - Type 1 GPS location update
- `type1-speeding-start.xml` - Type 1 speeding event start
- `type1-speeding-end.xml` - Type 1 speeding event end
- `type1-idle-start.xml` - Type 1 idle start
- `type1-idle-end.xml` - Type 1 idle end
- `type3-location.xml` - Type 3 GPS location update (with VIN)
- `type3-idle-start.xml` - Type 3 idle start (with VIN)
- `type3-idle-end.xml` - Type 3 idle end (with VIN)
- `type3-speeding-start.xml` - Type 3 speeding event start (with VIN)
- `type3-speeding-end.xml` - Type 3 speeding event end (with VIN)
