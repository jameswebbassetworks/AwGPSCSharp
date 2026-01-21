namespace CSharpInterviewMessageProcessor
{
    public interface IMessageTranslator
    {
        DeviceData Translate(Message message);
    }
}
