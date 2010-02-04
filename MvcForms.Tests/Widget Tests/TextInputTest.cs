namespace MvcForms.Tests
{
    using MvcForms.Widgets;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    
    /// <summary>
    ///This is a test class for TextInputTest and is intended
    ///to contain all TextInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TextInputTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for TextInput Constructor
        ///</summary>
        [TestMethod()]
        public void TextInputConstructorTest()
        {
            TextInput target = new TextInput();
        }

        [TestMethod()]
        public void TextInputRenderTest()
        {
            var target = new TextInput();
            Assert.AreEqual("<input type=\"text\" name=\"test\" />", target.Render("test", null));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"test1\" />", target.Render("test", "test1"));
        }
    }
}
