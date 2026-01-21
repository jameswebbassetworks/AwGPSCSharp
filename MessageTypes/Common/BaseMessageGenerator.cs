using System.Collections.Generic;
using FluentValidation.Results;

namespace CSharpInterviewMessageProcessor.MessageTypes.Common;

public abstract class BaseMessageGenerator : IMessageGenerator
{
    public abstract int MessageTypeId { get; }
    
    public abstract bool HasErrors { get; set; }
    public abstract ValidationResult ValidationResults { get; set; }
    public abstract IMessageType GenerateMessage(Dictionary<int, string> fields);
}
