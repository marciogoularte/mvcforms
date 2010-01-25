using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for DateTimeInputTest and is intended
    ///to contain all DateTimeInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateTimeInputTest
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

            DateTimeInput target = new DateTimeInput();
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"2008-11-15 10:02:45\" />",
                target.Render("test", dt));
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", 123));

            target.Format = "HH:mm:ss.fff MM/dd/yyyy";
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"10:02:45.234 11/15/2008\" />",
                target.Render("test", dt));
        }

        /// <summary>
        ///A test for DateTimeInput Constructor
        ///</summary>
        [TestMethod()]
        public void DateTimeInputConstructorTest()
        {
            DateTimeInput target = new DateTimeInput();
        }
    }
}
