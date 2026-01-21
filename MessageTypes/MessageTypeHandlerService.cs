using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.Logging;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using Microsoft.Extensions.Logging;

namespace CSharpInterviewMessageProcessor.MessageTypes;

public class MessageTypeHandlerService : IMessageTypeHandlerService
{
    private static readonly ILogger Logger = ConsoleAppLogging.CreateLogger(nameof(MessageTypeHandlerService));
    private static readonly ConcurrentDictionary<int, IMessageGenerator> Handlers = new();

    public void RegisterHandler(int messageType, IMessageGenerator messageGenerator)
    {
        Handlers.TryAdd(messageType, messageGenerator);
    }

    public IMessageType RunHandler(int messageType, Dictionary<int, string> fields)
    {
        Logger.LogInformation("Running Message Handler for message type: {MessageType}", messageType);
        if (Handlers.TryGetValue(messageType, out var messageHandler))
        {
            return messageHandler.GenerateMessage(fields);
        }
        Logger.LogError("No handler found for message type {MessageType}", messageType);
        throw new Exception($"No handler found for message type {messageType}");
    }
}
