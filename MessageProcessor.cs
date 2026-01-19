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
        _translatorMap = new Dictionary<int, Func<Message, object>>
        {
            { 0, msg => new Type0TranslatorClass().Translate(msg) },
            { 1, msg => new Type1TranslatorClass().Translate(msg) },
            { 2, msg => new Type2TranslatorClass().Translate(msg) },
            { 3, msg => new Type3TranslatorClass().Translate(msg) }
        };

    }

    public object Process(Message message)
    {
        var translatedMessages = new List<object>();
        foreach (var mess in _translatorMap)
        {
            if (!_translatorMap.TryGetValue(mess.Key, out var translatorFunc))
                throw new NotSupportedException($"Message type {message.MessageType} is not supported");

            var translatedMessage = translatorFunc(message);
            _eventProcessor.ProcessEvent(translatedMessage);
            translatedMessages.Add(translatedMessage);
        }

        // Return all translated messages for further processing if needed
        return translatedMessages;
    }
}