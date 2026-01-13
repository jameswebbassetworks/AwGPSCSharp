using CSharpInterviewMessageProcessor;

namespace CSharpInterviewMessageProcessorUnitTests
{
    //Sample unit test for speeding stop event with max speed as there wasn't one in all-messages.xml (but there was one in the type3-speeding-end.xml that I found after starting this)
    [TestClass]
    public sealed class EventTests
    {
        [DataRow("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<message type=\"3\">\r\n  <field number=\"0\">1FT7W2BT2EEB76476</field>\r\n  <field number=\"1\">DEV99999</field>\r\n  <field number=\"2\">2025-01-20</field>\r\n  <field number=\"3\">17:30:00</field>\r\n  <field number=\"10\">51.085</field>\r\n  <field number=\"11\">-114.095</field>\r\n  <field number=\"12\">52</field>\r\n  <field number=\"25\">90.0</field>\r\n  <field number=\"26\">95</field>\r\n  <field number=\"27\">135.0</field>\r\n</message>\r\n")]
        [TestMethod]
        public void SpeedingStopEventWithMaxSpeed(string xmlContent)
        {
            var parser = new MessageParser();
            var message = parser.ParseAll(xmlContent).FirstOrDefault();

            Event ev = new Event();

            MessageEventMapper.MapEventType(message, ev);
            MessageEventMapper.MapLocation(message, ev);
            MessageEventMapper.MapTimestamp(message, ev);
            MessageEventMapper.MapCurrentSpeed(message, ev);
            MessageEventMapper.MapMaxSpeed(message, ev);
            MessageEventMapper.MapIdleTime(message, ev);
            MessageEventMapper.MapVIN(message, ev);
            MessageEventMapper.SetDisplayValues(ev);

            var maxSpeed = Double.Parse(ev.MaxSpeed);

            Assert.AreEqual(ev.EventType, Event.EventTypeCode.SpeedingStop);
            Assert.AreEqual<double>(maxSpeed, 135.0);
            Assert.IsTrue(ev.DisplayMaxSpeed);
                
        }
    }
}
