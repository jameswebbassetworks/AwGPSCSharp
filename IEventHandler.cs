using System.Threading.Tasks;

namespace CSharpInterviewMessageProcessor
{
    public interface IEventHandler
    {
        Task HandleAsync(DeviceData data);
    }
}