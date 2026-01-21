using System;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.EventHandlers
{
    public class SpeedingStopHandler : IEventHandler
    {
        public Task HandleAsync(DeviceData data)
        {
            try
            {
            Console.WriteLine($"Speeding Stop Event at {data.Timestamp}");
            Console.WriteLine($"Location: ({data.Latitude}, {data.Longitude})");
            Console.WriteLine($"Current Speed: {data.Speed} km/h");
            Console.WriteLine($"Max Speed: {(data.MaxSpeed.HasValue ? $"{data.MaxSpeed} km/h\n" : "Not Available")}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling Speeding Stop event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }
}