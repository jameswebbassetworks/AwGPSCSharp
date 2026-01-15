using Xunit;
using CSharpInterviewMessageProcessor;
using AwGPSCSharp.Domain;
using AwGPSCSharp.SampleMessages.Vendors;

namespace AwGPSCSharp.Tests.Vendors;

public class MessageTypeThreeTests
{
    [Fact]
    public void Translate_SpeedingStart_ShouldIncludeVin()
    {
        var message = new Message
        {
            MessageType = 3,
            Fields =
            {
                [0] = "1FT7W2BT2EEB76476",
                [1] = "DEV999",
                [2] = "2025-01-20",
                [3] = "17:25:00",
                [10] = "51.08",
                [11] = "-114.09",
                [12] = "51",
                [25] = "130"
            }
        };

        var translator = new MessageTypeThree();

        var result = translator.Translate(message);

        Assert.Equal(VehicleEventType.SpeedingStart, result.EventType);
        Assert.Equal("1FT7W2BT2EEB76476", result.Vin);
        Assert.Equal(130, result.Speed);
    }
}
