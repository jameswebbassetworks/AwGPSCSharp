using System.Collections.Generic;

namespace CSharpInterviewMessageProcessor.MessageTypes.Common;

public abstract class BaseMessageHandler : IMessageHandler
{
    protected abstract int MessageTypeId { get; }
    public abstract IMessageType GenerateMessage(Dictionary<int, string> fields);

    public virtual void RegisterSelf()
    {
        MessageTypeHandler.RegisterHandler(MessageTypeId, this);
    }
}
