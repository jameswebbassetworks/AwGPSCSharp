using AwGPSCSharp.Domain;
using CSharpInterviewMessageProcessor;

namespace AwGPSCSharp.Vendors;

public interface IMessage
{
    int MessageType { get; }
    VehicleEvent Translate(Message message);
}
