using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor;

namespace CSharpInterviewMessageProcessor.MSTests
{
    [TestClass]
    public class MessageProcessorTests
    {
        [TestMethod]
        public async Task ProcessMessage_ShouldNotThrow_WhenMessageIsValid()
        {
            var processor = new MessageProcessor();
            var message = new Message
            {
                MessageType = 1, // matches your existing property
                Fields = { [1] = "SampleData" }
            };

            // If your MessageProcessor has a synchronous Process() method, call that
            // Otherwise, adjust to whatever method exists in your code
            processor.Process(message);

            Assert.IsTrue(true); // no exception expected
        }

        [TestMethod]
        public void Process_ShouldHandleNullMessageGracefully()
        {
            var processor = new MessageProcessor();

            try
            {
                processor.Process(null);
                Assert.IsTrue(true); // no exception thrown
            }
            catch
            {
                Assert.Fail("Process() should handle null messages gracefully.");
            }
        }
    }
}
