using System;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.EventHandlers
{
    public class DefaultEventHandler : IEventHandler
    {
        public Task HandleAsync(DeviceData data)
        {
            Console.WriteLine($"Default Event {data.EventCode} at {data.Timestamp}");
            Console.WriteLine($"Location: ({data.Latitude}, {data.Longitude})  Speed: {data.Speed}");
            return Task.CompletedTask;
        }
    }
}
