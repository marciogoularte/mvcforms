using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for PasswordInputTest and is intended
    ///to contain all PasswordInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PasswordInputTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext{get; set;}

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            PasswordInput target = new PasswordInput();
            Assert.AreEqual("<input type=\"password\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"password\" name=\"test\" />",
                target.Render("test", "test1"));

            target.RenderValue = true;
            Assert.AreEqual("<input type=\"password\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"password\" name=\"test\" value=\"test1\" />",
                target.Render("test", "test1"));
        }

        /// <summary>
        ///A test for PasswordInput Constructor
        ///</summary>
        [TestMethod()]
        public void PasswordInputConstructorTest()
        {
            PasswordInput target = new PasswordInput();
        }
    }
}
