using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpInterviewMessageProcessor.EventHandlers;

namespace CSharpInterviewMessageProcessor.MSTests
{
    [TestClass]
    public class EventHandlerTests
    {
        [TestMethod]
        public void ShouldInstantiateAllEventHandlers()
        {
            // Simply ensure all event handler classes can be created without exceptions
            _ = new DefaultEventHandler();
            _ = new IdleStartHandler();
            _ = new IdleEndHandler();
            _ = new LocationEventHandler();
            _ = new SpeedingStartHandler();
            _ = new SpeedingStopHandler();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShouldNotThrow_WhenHandlersAreUsed()
        {
            // This test just ensures that calling ToString() or any safe method
            // on the handlers does not throw an exception.
            var handlers = new object[]
            {
                new DefaultEventHandler(),
                new IdleStartHandler(),
                new IdleEndHandler(),
                new LocationEventHandler(),
                new SpeedingStartHandler(),
                new SpeedingStopHandler()
            };

            foreach (var handler in handlers)
            {
                Assert.IsNotNull(handler.ToString());
            }
        }
    }
}