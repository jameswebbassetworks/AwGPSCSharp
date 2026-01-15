using Xunit;
using CSharpInterviewMessageProcessor;
using AwGPSCSharp.SampleMessages.Vendors;
using AwGPSCSharp.Domain;

namespace AwGPSCSharp.Tests.Domain;

public class MessageValidationTests
{
    [Fact]
    public void Translate_MissingRequiredField_ShouldThrow()
    {
        var message = new Message
        {
            MessageType = 0,
            Fields =
            {
                [1] = "1",
                [4] = "51.1"
                // Missing DeviceId and Timestamp
            }
        };

        var translator = new MessageTypeZero();

        Assert.Throws<KeyNotFoundException>(() =>
            translator.Translate(message));
    }
}
