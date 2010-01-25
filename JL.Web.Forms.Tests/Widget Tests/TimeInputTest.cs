using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for TimeInputTest and is intended
    ///to contain all TimeInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TimeInputTest
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
            var dt = new System.DateTime(2008, 11, 15, 10, 2, 45, 234);

            TimeInput target = new TimeInput();
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"10:02:45\" />",
                target.Render("test", dt));
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", 123));

            target.Format = "HH:mm:ss.fff";
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"10:02:45.234\" />",
                target.Render("test", dt));
        }

        /// <summary>
        ///A test for TimeInput Constructor
        ///</summary>
        [TestMethod()]
        public void TimeInputConstructorTest()
        {
            TimeInput target = new TimeInput();
        }
    }
}
