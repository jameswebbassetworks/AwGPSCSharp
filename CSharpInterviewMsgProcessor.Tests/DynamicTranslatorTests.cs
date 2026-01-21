using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpInterviewMessageProcessor;

namespace CSharpInterviewMessageProcessor.MSTests
{
    [TestClass]
    public class DynamicTranslatorTests
    {
       
        [TestMethod]
        public void Translate_ShouldReturnNull_WhenMessageIsNull()
        {
            var translator = new DynamicTranslator("Honda");
            var result = translator.Translate(null);

            Assert.IsNull(result);
        }
    }
}