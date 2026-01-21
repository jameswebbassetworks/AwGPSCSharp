using System;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.EventHandlers
{
    public class IdleEndHandler : IEventHandler
    {
        public Task HandleAsync(DeviceData data)
        {
            try
            {
            Console.WriteLine($"Idle End Event at {data.Timestamp}");
            Console.WriteLine($"Location: ({data.Latitude}, {data.Longitude})");
            Console.WriteLine($"Current Speed: {data.Speed} km/h");
            Console.WriteLine($"Idle Time: {(data.IdleTime.HasValue ? $"{data.IdleTime} minutes\n" : "Not Available\n")}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling IdleEnd event: {ex.Message}");
            }
            return Task.CompletedTask;
        }
    }
}