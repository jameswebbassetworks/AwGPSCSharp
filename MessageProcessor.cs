using System;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor;
using CSharpInterviewMessageProcessor.Interfaces;
using CSharpInterviewMessageProcessor.TranslatorClass;

public class MessageProcessor
{
    // Dictionary maps message type to translator
    private readonly Dictionary<int, Func<Message, object>> _translatorMap;
    EventProcessor _eventProcessor = new EventProcessor();

    public MessageProcessor()
    {
        // Initialize the translator map
        _translatorMap = new Dictionary<int, Func<Message, object>>
        {
            { 0, msg => new Type0TranslatorClass().Translate(msg) },
            { 1, msg => new Type1TranslatorClass().Translate(msg) },
            { 2, msg => new Type2TranslatorClass().Translate(msg) },
            { 3, msg => new Type3TranslatorClass().Translate(msg) }
        };

    }
    /// <summary>
    /// Processes the incoming message by translating it and passing it to the event processor.
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public object Process(Message message)
    {
        try
        {
            var translatedMessages = new List<object>();
            foreach (var mess in _translatorMap)
            {
                if (!_translatorMap.TryGetValue(mess.Key, out var translatorFunc))
                    throw new NotSupportedException($"Message type {message.MessageType} is not supported");

                /// Translate the message
                var translatedMessage = translatorFunc(message);

                /// Pass the translated message to the event processor
                _eventProcessor.ProcessEvent(translatedMessage);
                translatedMessages.Add(translatedMessage);
            }

            // Return all translated messages for further processing if needed
            return translatedMessages;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
            throw;
        }
    }
}