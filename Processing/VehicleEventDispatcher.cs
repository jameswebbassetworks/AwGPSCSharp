using System;
using System.Collections.Generic;
using AwGPSCSharp.Domain;

namespace AwGPSCSharp.Processing;

public class VehicleEventDispatcher
{
    private readonly Dictionary<VehicleEventType, Action<VehicleEvent>> _handlers;

    public VehicleEventDispatcher()
    {
        _handlers = new()
        {
            [VehicleEventType.Location] = e =>
                Console.WriteLine($"[Location] {e.Timestamp:u} | Lat:{e.Latitude}, Lon:{e.Longitude}"),

            [VehicleEventType.SpeedingStart] = e =>
                Console.WriteLine($"[Speeding Start] {e.Timestamp:u} | Speed:{e.Speed}"),

            [VehicleEventType.SpeedingEnd] = e =>
                Console.WriteLine($"[Speeding End] {e.Timestamp:u} | Speed:{e.Speed}, Max:{e.MaxSpeed}"),

            [VehicleEventType.IdleStart] = e =>
                Console.WriteLine($"[Idle Start] {e.Timestamp:u}"),

            [VehicleEventType.IdleEnd] = e =>
                Console.WriteLine($"[Idle End] {e.Timestamp:u} | Idle:{e.IdleTimeSeconds}s")
        };
    }

    public void Dispatch(VehicleEvent vehicleEvent)
    {
        if (!_handlers.TryGetValue(vehicleEvent.EventType, out var handler))
            throw new NotSupportedException(
                $"No handler for event type {vehicleEvent.EventType}");

        handler(vehicleEvent);
    }
}
