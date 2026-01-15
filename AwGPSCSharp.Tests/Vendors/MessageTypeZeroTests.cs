using Xunit;
using CSharpInterviewMessageProcessor;
using AwGPSCSharp.Domain;
using AwGPSCSharp.SampleMessages.Vendors;

namespace AwGPSCSharp.Tests.Vendors;

public class MessageTypeZeroTests
{

    [Fact]
    public void Translate_LocationMessage_ShouldMapCorrectly()
    {
        var message = new Message
        {
            MessageType = 0,
            Fields =
            {
                [0] = "DEV123",
                [1] = "1",
                [4] = "51.1",
                [5] = "-114.1",
                [6] = "2025-01-20T10:00:00Z"
            }
        };

        var translator = new MessageTypeZero();

        var result = translator.Translate(message);

        Assert.Equal(VehicleEventType.Location, result.EventType);
        Assert.Equal("DEV123", result.DeviceId);
        Assert.Equal(51.1, result.Latitude);
        Assert.Equal(-114.1, result.Longitude);
    }
}
