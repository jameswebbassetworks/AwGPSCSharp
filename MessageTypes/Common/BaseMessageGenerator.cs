using System.Collections.Generic;

namespace CSharpInterviewMessageProcessor.MessageTypes.Common;

public abstract class BaseMessageGenerator : IMessageGenerator
{
    public abstract int MessageTypeId { get; }
    public abstract IMessageType GenerateMessage(Dictionary<int, string> fields);
}
