using System;
using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for TimeFieldTest and is intended
    ///to contain all TimeFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TimeFieldTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for InputFormats
        ///</summary>
        [TestMethod()]
        public void InputFormatsTest()
        {
            TimeField target = new TimeField
            {
                InputFormats = new string[] {
                    "HH:mm:ss"
                }
            };

            Assert.AreEqual(1, target.InputFormats.Length);
        }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            TimeField target = new TimeField();

            // Required
            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean(null);
            }).WithMessage("This field is required.");

            target.Required = false;

            Assert.IsNull(target.Clean(null));

            var expected = new TimeSpan(14, 30, 59);
            Assert.AreEqual(expected, target.Clean(new DateTime?(new DateTime(2006, 10, 25, 14, 30, 59))));
            Assert.AreEqual(expected, target.Clean("14:30:59"));
            Assert.AreEqual(new TimeSpan(14, 30, 00), target.Clean("14:30"));

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean("14:65:59");
            }).WithMessage("Enter a valid time.");
        }

        /// <summary>
        ///A test for TimeField Constructor
        ///</summary>
        [TestMethod()]
        public void TimeFieldConstructorTest1()
        {
            TimeField target = new TimeField();
            // No error is a pass
        }

        /// <summary>
        ///A test for TimeField Constructor
        ///</summary>
        [TestMethod()]
        public void TimeFieldConstructorTest()
        {
            var target = new TimeField(new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }
    }
}
