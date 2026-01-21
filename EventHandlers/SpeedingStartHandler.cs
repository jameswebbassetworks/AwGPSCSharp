using System;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.EventHandlers
{
    public class SpeedingStartHandler : IEventHandler
    {
        public Task HandleAsync(DeviceData data)
        {
            try
            {
            Console.WriteLine($"Speeding Start Event at {data.Timestamp}");
            Console.WriteLine($"Location: ({data.Latitude}, {data.Longitude})");
            Console.WriteLine($"Current Speed: {data.Speed} km/h\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling Speeding Start event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }
}
