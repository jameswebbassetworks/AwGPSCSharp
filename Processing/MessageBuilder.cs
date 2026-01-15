using System;
using System.Collections.Generic;
using AwGPSCSharp.Domain;
using AwGPSCSharp.SampleMessages.Vendors;
using AwGPSCSharp.Vendors;
using CSharpInterviewMessageProcessor;

namespace AwGPSCSharp.Processing;

public class MessageBuilder
{
    private readonly Dictionary<int, IMessage> _builders = new()
    {
        { 0, new MessageTypeZero() },
        { 1, new MessageTypeOne() },
        { 3, new MessageTypeThree() }
    };

    public VehicleEvent Build(Message message)
    {
        if (!_builders.TryGetValue(message.MessageType, out var builder))
            throw new NotSupportedException(
                $"No translator registered for MessageType {message.MessageType}");

        return builder.Translate(message);
    }
}
