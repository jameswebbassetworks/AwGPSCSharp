using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using CSharpInterviewMessageProcessor;

namespace CSharpInterviewMessageProcessor.MSTests
{
    [TestClass]
    public class MessageParserTests
    {
        [TestMethod]
        public void Parse_ShouldReturnValidMessage_WhenXmlIsValid()
        {
            var parser = new MessageParser();
            var xml = File.ReadAllText("SampleMessages/type3-speeding-end.xml");

            var message = parser.ParseAll(xml);

            Assert.IsNotNull(message);
            // If your Message class doesn't have Type, just check for not-null result
            Assert.IsTrue(message != null);
        }

        [TestMethod]
        public void Parse_ShouldThrow_WhenXmlIsInvalid()
        {
            var parser = new MessageParser();

            // Use Assert.ThrowsException instead of [ExpectedException]
            Assert.Throws<XmlException>(() =>
                parser.ParseAll("<InvalidXml>")
            );
        }

    }
}