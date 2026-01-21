namespace CSharpInterviewMessageProcessor.MessageTypes.Common;

public interface IMessageType
{
    CombinedMessage ToCombinedMessage();
}
