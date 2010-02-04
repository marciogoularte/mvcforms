using MvcForms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;

namespace MvcForms.Tests
{

    internal class TestInput : Input
    {
        internal TestInput() : base("test") { }
    }

    
    /// <summary>
    ///This is a test class for InputTest and is intended
    ///to contain all InputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InputTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        internal virtual Input CreateInput()
        {            
            Input target = new TestInput();
            return target;
        }

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            Input target = CreateInput(); 
            string name = "testname"; 
            ElementAttributesDictionary extraAttributes = new ElementAttributesDictionary();
            extraAttributes.Add("Test1", "Test1");

            Assert.AreEqual("<input type=\"test\" name=\"testname\" value=\"123\" />", target.Render(name, "123"));
            Assert.AreEqual("<input Test1=\"Test1\" type=\"test\" name=\"testname\" value=\"123\" />", target.Render(name, "123", extraAttributes));
        }
    }
}
