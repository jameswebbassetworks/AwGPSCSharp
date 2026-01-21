using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes;

public interface IMessageTypeHandlerService
{
    void RegisterHandler(int messageType, IMessageGenerator messageGenerator);
    IMessageType RunHandler(int messageType, Dictionary<int, string> fields);
}
