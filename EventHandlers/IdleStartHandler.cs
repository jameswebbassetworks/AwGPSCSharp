using System;
using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor.EventHandlers
{
    public class IdleStartHandler : IEventHandler
    {
        public Task HandleAsync(DeviceData data)
        {
            Console.WriteLine($"Idle Start Event at {data.Timestamp}");
            Console.WriteLine($"Location: ({data.Latitude}, {data.Longitude})\n");
            return Task.CompletedTask;
        }
    }
}
