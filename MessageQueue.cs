using System.Collections.Concurrent;
using System.Threading;

namespace CSharpInterviewMessageProcessor;

public class MessageQueue
{
    private readonly BlockingCollection<Message> _queue = new();

    public void Enqueue(Message message)
    {
        _queue.Add(message);
    }

    public Message Dequeue(CancellationToken cancellationToken)
    {
        return _queue.Take(cancellationToken);
    }

    public void CompleteAdding()
    {
        _queue.CompleteAdding();
    }

    public bool IsCompleted => _queue.IsCompleted;
}
