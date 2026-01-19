using System;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.Models;

/// <summary>
/// Handles processing of different event types from translated messages
/// </summary>
public class EventProcessor
{
    private readonly Dictionary<string, Action<object>> _eventActions;

    public EventProcessor()
    {
        /// Initialize the mapping of event types to their handlers
        _eventActions = new Dictionary<string, Action<object>>
        {
            { "Location", EventTypeLocation },
            { "SpeedingStart", EventTypeSpeedingStart },
            { "SpeedingStop", EventTypeSpeedingStop },
            { "IdleStart", EventTypeStart },
            { "IdleEnd", EventTypeEnd }
        };
    }

    /// <summary>
    /// Processes the translated message based on its event type
    /// </summary>
    /// <param name="translatedMessage"></param>

    public void ProcessEvent(object translatedMessage)
    {
        string eventType = GetEventType(translatedMessage);

        if (eventType != null && _eventActions.TryGetValue(eventType, out var action))
        {
            action(translatedMessage);
        }
        else
        {
            Console.WriteLine("Unknown event type or unsupported message type");
        }
    }
    /// <summary>
    /// EventType getter based on message type and event code
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    private string GetEventType(object msg)
    {
        switch (msg)
        {
            case Type0Message t0:
                return t0.EventCode switch
                {
                    "1" => "Location",
                    "2" => "SpeedingStart",
                    "3" => "SpeedingStop",
                    "4" => "IdleStart",
                    "5" => "IdleEnd",
                    _ => null
                };

            case Type1Message t1:
                return t1.EventCode switch
                {
                    "50" => "Location",
                    "62" => "SpeedingStart",
                    "63" => "SpeedingStop",
                    "97" => "IdleStart",
                    "98" => "IdleEnd",
                    _ => null
                };

            case Type3Message t3:
                return t3.EventCode switch
                {
                    15 => "Location",
                    51 => "SpeedingStart",
                    52 => "SpeedingStop",
                    26 => "IdleStart",
                    28 => "IdleEnd",
                    _ => null
                };

            default:
                return null;
        }
    }

    #region Handlers
    /// <summary>
    /// Processes location-related messages of various types and outputs relevant location and device information to the
    /// console.
    /// </summary>
    /// <param name="msg">The message object containing location event data.</param>
    private void EventTypeLocation(object msg)
    {
        switch (msg)
        {
            case Type0Message t0:
                Console.WriteLine($"[Location] {t0.Timestamp} Device:{t0.DeviceID} at {t0.Latitude},{t0.Longitude}");
                break;
            case Type1Message t1:
                var latLong1 = t1.Longitude;
                Console.WriteLine($"[Location] {t1.Timestamp} Device:{t1.DeviceID} at {t1.Longitude}");
                break;
            case Type3Message t3:
                if (t3.VIN != null)
                {   var vehicleInfo = ProcesswithVin(t3.VIN);
                    Console.WriteLine($"Vehicle Info for VIN {t3.VIN}: {vehicleInfo}");
                }
                Console.WriteLine($"[Location] {t3.Date} {t3.Time} Device:{t3.DeviceID} at {t3.Latitude},{t3.Longitude} VIN:{t3.VIN}");
                break;

        }
    }
    /// <summary>
    /// event handler for speeding start events
    /// </summary>
    /// <param name="msg"></param>

    private void EventTypeSpeedingStart(object msg)
    {
        switch (msg)
        {
            case Type0Message t0:
                Console.WriteLine($"[Speeding Start] {t0.Timestamp} Device:{t0.DeviceID} at {t0.Latitude},{t0.Longitude} Speed:{t0.Speed} m/s");
                break;
            case Type1Message t1:
                var latLong1 = t1.Longitude;
                Console.WriteLine($"[Speeding Start] {t1.Timestamp} Device:{t1.DeviceID} at {t1.Longitude} Speed:{t1.Speed} m/s");
                break;
            case Type3Message t3:
                if (t3.VIN != null)
                {
                    var vehicleInfo = ProcesswithVin(t3.VIN);
                    Console.WriteLine($"Vehicle Info for VIN {t3.VIN}: {vehicleInfo}");
                }
                Console.WriteLine($"[Speeding Start] {t3.Date} {t3.Time} Device:{t3.DeviceID} at {t3.Latitude},{t3.Longitude} Speed:{t3.Speed} km/h VIN:{t3.VIN}");
                break;
        }
    }
    /// <summary>
    /// Handles and logs speeding stop events based on the type of message received.
    /// </summary>
    /// <param name="msg">The message object containing speeding stop event data.</param>
    private void EventTypeSpeedingStop(object msg)
    {
        switch (msg)
        {
            case Type0Message t0:
                Console.WriteLine($"[Speeding Stop] {t0.Timestamp} Device:{t0.DeviceID} at {t0.Latitude},{t0.Longitude} Speed:{t0.Speed} MaxSpeed:{t0.MaxSpeed}");
                break;
            case Type1Message t1:
                var latLong1 = t1.Longitude;
                Console.WriteLine($"[Speeding Stop] {t1.Timestamp} Device:{t1.DeviceID} at {t1.Longitude} Speed:{t1.Speed} MaxSpeed:{t1.Speed}");
                break;
            case Type3Message t3:
                if (t3.VIN != null)
                {
                    var vehicleInfo = ProcesswithVin(t3.VIN);
                    Console.WriteLine($"Vehicle Info for VIN {t3.VIN}: {vehicleInfo}");
                }
                Console.WriteLine($"[Speeding Stop] {t3.Date} {t3.Time} Device:{t3.DeviceID} at {t3.Latitude},{t3.Longitude} Speed:{t3.Speed} MaxSpeed:{t3.MaxSpeed} VIN:{t3.VIN}");
                break;
        }
    }
    /// <summary>
    /// event handler for idle start events
    /// </summary>
    /// <param name="msg"></param>
    private void EventTypeStart(object msg)
    {
        switch (msg)
        {
            case Type0Message t0:
                Console.WriteLine($"[Idle Start] {t0.Timestamp} Device:{t0.DeviceID} at {t0.Latitude},{t0.Longitude}");
                break;
            case Type1Message t1:
                var latLong1 = t1.Longitude;
                Console.WriteLine($"[Idle Start] {t1.Timestamp} Device:{t1.DeviceID} at {"longitude"}");
                break;
            case Type3Message t3:
                if (t3.VIN != null)
                {
                    var vehicleInfo = ProcesswithVin(t3.VIN);
                    Console.WriteLine($"Vehicle Info for VIN {t3.VIN}: {vehicleInfo}");
                }
                Console.WriteLine($"[Idle Start] {t3.Date} {t3.Time} Device:{t3.DeviceID} at {t3.Latitude},{t3.Longitude} VIN:{t3.VIN}");
                break;
        }
    }
    /// <summary>
    /// event handler for idle end events
    /// </summary>
    /// <param name="msg"></param>

    private void EventTypeEnd(object msg)
    {
        switch (msg)
        {
            case Type0Message t0:
                Console.WriteLine($"[Idle End] {t0.Timestamp} Device:{t0.DeviceID} at {t0.Latitude},{t0.Longitude} Speed:{t0.Speed} IdleTime:{t0.IdleTime}");
                break;
            case Type1Message t1:
                var latLong1 = t1.Longitude;
                Console.WriteLine($"[Idle End] {t1.Timestamp} Device:{t1.DeviceID} at {t1.Longitude} Speed:{t1.Speed} IdleTime:{t1.IdleTime}");
                break;
            case Type3Message t3:
                if (t3.VIN != null)
                {
                    var vehicleInfo = ProcesswithVin(t3.VIN);
                    Console.WriteLine($"Vehicle Info for VIN {t3.VIN}: {vehicleInfo}");
                }
                Console.WriteLine($"[Idle End] {t3.Date} {t3.Time} Device:{t3.DeviceID} at {t3.Latitude},{t3.Longitude} Speed:{t3.Speed} IdleTime:{t3.IdleTime} VIN:{t3.VIN}");
                break;
        }
    }
    /// <summary>
    /// Process vehicle information using VIN from NHTSA API
    /// </summary>
    /// <param name="vin"></param>
    /// <returns></returns>
    public static string ProcesswithVin(string vin)
    {
        if (string.IsNullOrEmpty(vin))
            return "No VIN provided";

        string url = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{vin}?format=json";

        try
        {
            using (var client = new System.Net.WebClient())
            {
                string json = client.DownloadString(url);
                // You can deserialize and pick specific fields if needed
                return json;
            }
        }
        catch (Exception ex)
        {
            return $"Error fetching vehicle info: {ex.Message}";
        }
    }
    #endregion
}
