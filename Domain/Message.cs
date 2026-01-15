using System.Collections.Generic;

namespace AwGPSCSharp.Domain;

public class Message
{
    public Dictionary<int, string> Fields { get; set; } = new();
    public int MessageType { get; set; }
}
