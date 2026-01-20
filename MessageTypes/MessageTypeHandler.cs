using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using CSharpInterviewMessageProcessor.MessageTypes.Common;

namespace CSharpInterviewMessageProcessor.MessageTypes;

public static class MessageTypeHandler
{
    private static readonly ConcurrentDictionary<int, IMessageHandler> Handlers = new();
    public static void RegisterHandler(int messageType, IMessageHandler messageHandler)
    {
        Handlers.TryAdd(messageType, messageHandler);
    }

    public static IMessageType RunHandler(int messageType, Dictionary<int, string> fields)
    {
        return Handlers.TryGetValue(messageType, out IMessageHandler? messageHandler) 
            ? messageHandler.GenerateMessage(fields)
            : throw new Exception($"No handler found for message type {messageType}");
    }
    

}