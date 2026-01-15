using System.Collections.Generic;
using System.Xml.Linq;
using AwGPSCSharp.Domain;
namespace CSharpInterviewMessageProcessor;

public class MessageParser
{
    public List<Message> ParseAll(string xmlContent)
    {
        var messages = new List<Message>();
        var doc = XDocument.Parse(xmlContent);
        
        var messageElements = doc.Descendants("message");
        foreach (var msgElement in messageElements)
        {
            var message = new Message();

            int messageType = -1;
            var typeAttr = msgElement.Attribute("type")?.Value;
            if(typeAttr != null)
            {
                if(!int.TryParse(typeAttr, out messageType))
                {
                    messageType = -1;
                }
            }

            message.MessageType = messageType;

            var fields = msgElement.Descendants("field");
            foreach (var field in fields)
            {
                var numAttr = field.Attribute("number")?.Value;
                if (numAttr != null && int.TryParse(numAttr, out int fieldNum))
                {
                    message.Fields[fieldNum] = field.Value;
                }
            }
            messages.Add(message);
        }
        
        return messages;
    }
}
