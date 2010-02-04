using MvcForms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for DateInputTest and is intended
    ///to contain all DateInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateInputTest
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
            var dt = new System.DateTime(2008, 11, 15);

            DateInput target = new DateInput();
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"2008-11-15\" />",
                target.Render("test", dt));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"123\" />",
                target.Render("test", 123));

            target.Format = "MM/dd/yyyy";
            Assert.AreEqual("<input type=\"text\" name=\"test\" />",
                target.Render("test", null));
            Assert.AreEqual("<input type=\"text\" name=\"test\" value=\"11/15/2008\" />",
                target.Render("test", dt));
        }

        /// <summary>
        ///A test for DateInput Constructor
        ///</summary>
        [TestMethod()]
        public void DateInputConstructorTest()
        {
            DateInput target = new DateInput();
        }
    }
}
