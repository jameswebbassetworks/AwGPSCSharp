using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.Logging;
using CSharpInterviewMessageProcessor.MessageTypes.Common;
using Microsoft.Extensions.Logging;

namespace CSharpInterviewMessageProcessor.MessageTypes;

public static class MessageTypeHandler
{
    private static readonly ILogger Logger = ConsoleAppLogging.CreateLogger(nameof(MessageTypeHandler));
    private static readonly ConcurrentDictionary<int, IMessageHandler> Handlers = new();

    public static void RegisterHandler(int messageType, IMessageHandler messageHandler)
    {
        Handlers.TryAdd(messageType, messageHandler);
    }

    public static IMessageType RunHandler(int messageType, Dictionary<int, string> fields)
    {
        Logger.LogInformation("Running Message Handler for message type: {MessageType}", messageType);
        return Handlers.TryGetValue(messageType, out var messageHandler)
            ? messageHandler.GenerateMessage(fields)
            : throw new Exception($"No handler found for message type {messageType}");
    }
}
