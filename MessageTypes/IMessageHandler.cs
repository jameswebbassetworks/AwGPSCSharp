using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes;

public interface IMessageHandler
{
    IMessageType GenerateMessage(Dictionary<int, string> fields);
    static int MessageTypeId { get; }

    void RegisterSelf();
}