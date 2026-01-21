using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using CSharpInterviewMessageProcessor;

namespace CSharpInterviewMessageProcessor.MSTests
{
    [TestClass]
    public class MessageQueueTests
    {
        [TestMethod]
        public void EnqueueDequeue_ShouldWorkCorrectly()
        {
            var queue = new MessageQueue();
            var msg = new Message { MessageType = 1 }; // uses existing property

            queue.Enqueue(msg);
            var dequeued = queue.Dequeue(CancellationToken.None);

            Assert.AreEqual(msg, dequeued);
        }

        [TestMethod]
        public void Dequeue_ShouldThrow_WhenCancelled()
        {
            var queue = new MessageQueue();
            var cts = new CancellationTokenSource();
            cts.Cancel();

            // Correct MSTest syntax
            object value = Microsoft.VisualStudio.TestTools.UnitTesting.Assert.Throws<OperationCanceledException>(
                () => queue.Dequeue(cts.Token)
            );
        }
    }
}