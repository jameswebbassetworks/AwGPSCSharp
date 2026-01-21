using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor
{
    public class MessageProcessor
    {
        private readonly Dictionary<int, IMessageTranslator> _translators;
        private readonly Dictionary<string, IEventHandler> _eventHandlers;
        private readonly WebRequestHelper _webHelper = new WebRequestHelper();

        public MessageProcessor()
        {
            // Register translators for each manufacturer (message type)
            _translators = new Dictionary<int, IMessageTranslator>
            {
                { 0, new DynamicTranslator("ManufacturerA") },
                { 1, new DynamicTranslator("ManufacturerB") },
                { 3, new DynamicTranslator("ManufacturerC") }
            };

            // Register event handlers for each event code
            _eventHandlers = new Dictionary<string, IEventHandler>
            {
                { "1", new EventHandlers.LocationEventHandler() },
                { "2", new EventHandlers.SpeedingStartHandler() },
                { "3", new EventHandlers.SpeedingStopHandler() },
                { "4", new EventHandlers.IdleStartHandler() },
                { "5", new EventHandlers.IdleEndHandler() },
                { "50", new EventHandlers.LocationEventHandler() },
                { "62", new EventHandlers.SpeedingStartHandler() },
                { "63", new EventHandlers.SpeedingStopHandler() },
                { "97", new EventHandlers.IdleStartHandler() },
                { "98", new EventHandlers.IdleEndHandler() },
                { "12", new EventHandlers.LocationEventHandler() },
                { "34", new EventHandlers.IdleStartHandler() },
                { "35", new EventHandlers.IdleEndHandler() },
                { "56", new EventHandlers.SpeedingStartHandler() },
                { "57", new EventHandlers.SpeedingStopHandler() }
            };
        }

public async Task Process(Message message)
{
    try
    {
                if (!_translators.TryGetValue(message.MessageType, out var translator))
                {
                    Console.WriteLine($"No translator found for message type {message.MessageType}");
                    return;
                }

                var data = translator.Translate(message);
                if (data == null)
        {
            Console.WriteLine("Failed to translate message.");
            return;
        }
                // VIN lookup
                if (message.MessageType == 3 && !string.IsNullOrEmpty(data.VIN))
                {
            var url = $"https://vpic.nhtsa.dot.gov/api/vehicles/DecodeVin/{data.VIN}?format=json";
            try
            {
                var json = _webHelper.Get(url);
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                var vehicle = result.Results[0];
                Console.WriteLine("=== Vehicle Information ===");
                Console.WriteLine($"Model Year : {vehicle.ModelYear}");
                Console.WriteLine($"Make       : {vehicle.Make}");
                Console.WriteLine($"Model      : {vehicle.Model}");
                Console.WriteLine($"Fuel Type  : {vehicle.FuelTypePrimary}");
                Console.WriteLine("============================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"VIN lookup failed: {ex}");
            }
        }

                if (_eventHandlers.TryGetValue(data.EventCode, out var handler))
        {
            await handler.HandleAsync(data);
        }
        else
        {
            Console.WriteLine($"Unknown event code: {data.EventCode}. Using default handler.");
            await new EventHandlers.DefaultEventHandler().HandleAsync(data);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error processing message: {ex}");
    }
}
    }
}