using System;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.EventHandlers
{
    public class LocationEventHandler : IEventHandler
    {
        public Task HandleAsync(DeviceData data)
        {
            try
            {
                Console.WriteLine($"Location Event at {data.Timestamp}");
                Console.WriteLine($"Coordinates: {data.Latitude}, {data.Longitude}\n");
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling Location event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }
}