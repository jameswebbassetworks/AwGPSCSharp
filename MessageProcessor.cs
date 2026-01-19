using System;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor;
using CSharpInterviewMessageProcessor.Interfaces;
using CSharpInterviewMessageProcessor.TranslatorClass;

public class MessageProcessor
{
    // Dictionary maps message type to translator
    private readonly Dictionary<int, Imess> _translators;

    public MessageProcessor()
    {
        _translators = new Dictionary<int, IMessageTranslator>
        {
            { 0, new Type0Translator() },
            { 1, new Type1Translator() },
            { 2, new Type2Translator() },
            { 3, new Type3Translator() }
        };
    }

    public object Process(Message message)
    {
        if (!_translators.TryGetValue(message.Type, out var translator))
        {
            throw new NotSupportedException($"Message type {message.Type} is not supported");
        }

        // Translate message using the correct translator
        return translator.Translate(message);
    }
}