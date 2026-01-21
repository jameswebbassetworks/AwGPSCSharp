using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes;

public interface IMessageHandler
{
    static int MessageTypeId { get; }
    IMessageType GenerateMessage(Dictionary<int, string> fields);
}
