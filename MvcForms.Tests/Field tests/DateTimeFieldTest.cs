using System;
using MvcForms;
using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for DateTimeFieldTest and is intended
    ///to contain all DateTimeFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateTimeFieldTest
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
            DateField target = new DateField
            {
                InputFormats = new string[] {
                    "yyyy-MM-dd HH:mm:ss", 
                    "yyyy-MM-dd HH:mm"
                }
            };

            Assert.AreEqual(2, target.InputFormats.Length);
        }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            DateTimeField target = new DateTimeField();
            var expectedDate = new DateTime(2006, 10, 25);
            var expectedTime = new DateTime(2006, 10, 25, 14, 30, 00);
            var expectedTime2 = new DateTime(2006, 10, 25, 14, 30, 59);

            // Required
            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean(null);
            }).WithMessage("This field is required.");

            target.Required = false;

            Assert.IsNull(target.Clean(null));
            Assert.AreEqual(expectedTime2, target.Clean(new DateTime?(expectedTime2)));
            Assert.AreEqual(expectedTime2, target.Clean("2006-10-25 14:30:59"));
            Assert.AreEqual(expectedTime, target.Clean("2006-10-25 14:30"));
            Assert.AreEqual(expectedDate, target.Clean("2006-10-25"));
            Assert.AreEqual(expectedTime2, target.Clean("10/25/2006 14:30:59"));
            Assert.AreEqual(expectedTime, target.Clean("10/25/2006 14:30"));
            Assert.AreEqual(expectedDate, target.Clean("10/25/2006"));
            Assert.AreEqual(expectedTime2, target.Clean("10/25/06 14:30:59"));
            Assert.AreEqual(expectedTime, target.Clean("10/25/06 14:30"));
            Assert.AreEqual(expectedDate, target.Clean("10/25/06"));

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean("2006-10-25 14:30:61");
            }).WithMessage("Enter a valid date.");

            Assert.AreEqual(expectedTime2, target.Clean(new Tuple("2006-10-25", "14:30:59")));

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean(new Tuple("2006-10-25", "14:30:59", "abc"));
            }).WithMessage("Enter a valid date.");

        }

        /// <summary>
        ///A test for DateTimeField Constructor
        ///</summary>
        [TestMethod()]
        public void DateTimeFieldConstructorTest1()
        {
            DateTimeField target = new DateTimeField();
            // Assume no exception is a pass
        }

        /// <summary>
        ///A test for DateTimeField Constructor
        ///</summary>
        [TestMethod()]
        public void DateTimeFieldConstructorTest()
        {
            DateTimeField target = new DateTimeField(new Widgets.SplitDateTime());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.SplitDateTime));
        }
    }
}
