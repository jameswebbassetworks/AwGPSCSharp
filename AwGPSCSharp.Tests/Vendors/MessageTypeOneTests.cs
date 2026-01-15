using Xunit;
using CSharpInterviewMessageProcessor;
using AwGPSCSharp.Domain;
using AwGPSCSharp.SampleMessages.Vendors;

namespace AwGPSCSharp.Tests.Vendors;

public class MessageTypeOneTests
{
    [Fact]
    public void Translate_LocationMessage_ShouldMapCorrectly()
    {
        var message = new Message
        {
            MessageType = 1,
            Fields =
            {
                [0] = "50",
                [1] = "DEV456",
                [2] = "51.05,-114.06",
                [3] = "2025-01-20T12:00:00Z"
            }
        };

        var translator = new MessageTypeOne();

        var result = translator.Translate(message);

        Assert.Equal(VehicleEventType.Location, result.EventType);
        Assert.Equal("DEV456", result.DeviceId);
        Assert.Equal(51.05, result.Latitude);
        Assert.Equal(-114.06, result.Longitude);
    }
}
