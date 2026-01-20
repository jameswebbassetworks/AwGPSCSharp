namespace CSharpInterviewMessageProcessor.MessageTypes.Common;

public interface IMessageType
{
    CombinedMessage CombinedMessage { get; set; }
    CombinedMessage ToCombinedMessage();
}