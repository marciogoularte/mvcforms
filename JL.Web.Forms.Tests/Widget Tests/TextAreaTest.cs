namespace JL.Web.Forms.Tests
{
    using JL.Web.Forms.Widgets;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using JL.Web.Forms;
    
    
    /// <summary>
    ///This is a test class for TextAreaTest and is intended
    ///to contain all TextAreaTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TextAreaTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            var target = new TextArea();
            Assert.AreEqual("<textarea cols=\"40\" rows=\"10\" name=\"test\"></textarea>", 
                target.Render("test", null));
            Assert.AreEqual("<textarea cols=\"40\" rows=\"10\" name=\"test\">test1</textarea>", 
                target.Render("test", "test1"));
        }

        /// <summary>
        ///A test for TextArea Constructor
        ///</summary>
        [TestMethod()]
        public void TextAreaConstructorTest()
        {
            var target = new TextArea();
        }
    }
}
