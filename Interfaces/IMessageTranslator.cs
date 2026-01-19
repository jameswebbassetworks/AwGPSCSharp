using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpInterviewMessageProcessor.Models;

namespace CSharpInterviewMessageProcessor.Interfaces
{
    public interface IMessageTranslator<TMessage>
    {
        TMessage Translate(Message message);
    }
}
