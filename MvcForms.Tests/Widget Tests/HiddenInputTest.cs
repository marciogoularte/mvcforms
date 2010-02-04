namespace MvcForms.Tests
{
    using MvcForms.Widgets;
    using Microsoft.VisualStudio.TestTools.UnitTesting;    
    
    /// <summary>
    ///This is a test class for HiddenInputTest and is intended
    ///to contain all HiddenInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HiddenInputTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for IsHidden
        ///</summary>
        [TestMethod()]
        public void IsHiddenTest()
        {
            var target = new HiddenInput(); // TODO: Initialize to an appropriate value
            Assert.IsTrue(target.IsHidden);
        }

        /// <summary>
        ///A test for HiddenInput Constructor
        ///</summary>
        [TestMethod()]
        public void HiddenInputConstructorTest()
        {
            var target = new HiddenInput();
        }

        /// <summary>
        /// A test for Render
        /// </summary>
        [TestMethod()]
        public void HiddenInputRenderTest()
        {
            var target = new HiddenInput();
            Assert.AreEqual("<input type=\"hidden\" name=\"test\" />", target.Render("test", null));
            Assert.AreEqual("<input type=\"hidden\" name=\"test\" value=\"test1\" />", target.Render("test", "test1"));
        }
    }
}
