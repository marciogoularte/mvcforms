using System;
using JL.Web.Forms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for DateFieldTest and is intended
    ///to contain all DateFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateFieldTest
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
                    "yyyy-MM-dd", "yy-MM-dd"
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
            DateField target = new DateField();
            var expected = new DateTime(2006, 10, 25);

            // Required
            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean(null);
            }).WithMessage("This field is required.");

            target.Required = false;

            Assert.IsNull(target.Clean(null));

            Assert.AreEqual(expected, target.Clean(new DateTime?(expected)));
            Assert.AreEqual(expected, target.Clean("2006-10-25"));
            Assert.AreEqual(expected, target.Clean("06-10-25"));
            Assert.AreEqual(expected, target.Clean("10/25/2006"));
            Assert.AreEqual(expected, target.Clean("10/25/06"));
            Assert.AreEqual(expected, target.Clean("Oct 25 2006"));
            Assert.AreEqual(expected, target.Clean("Oct 25, 2006"));
            Assert.AreEqual(expected, target.Clean("25 Oct 2006"));
            Assert.AreEqual(expected, target.Clean("25 Oct, 2006"));
            Assert.AreEqual(expected, target.Clean("October 25 2006"));
            Assert.AreEqual(expected, target.Clean("October 25, 2006"));
            Assert.AreEqual(expected, target.Clean("25 October 2006"));
            Assert.AreEqual(expected, target.Clean("25 October, 2006"));

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean("25 Octc, 2006");
            }).WithMessage("Enter a valid date.");
        }

        /// <summary>
        ///A test for DateField Constructor
        ///</summary>
        [TestMethod()]
        public void DateFieldConstructorTest1()
        {
            DateField target = new DateField();
            // Assume no exception is a pass
        }

        /// <summary>
        ///A test for DateField Constructor
        ///</summary>
        [TestMethod()]
        public void DateFieldConstructorTest()
        {
            DateField target = new DateField(new Widgets.SplitDateTime());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.SplitDateTime));
        }
    }
}
