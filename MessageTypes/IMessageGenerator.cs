using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes;

public interface IMessageGenerator
{
    IMessageType GenerateMessage(Dictionary<int, string> fields);
}
