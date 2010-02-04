using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;
using System;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for FloatFieldTest and is intended
    ///to contain all FloatFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FloatFieldTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            FloatField target = new FloatField();

            // Default test
            Assert.AreEqual((float?)12.2, (float?)target.Clean("12.2"));

            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean(null); 
            }).WithMessage("This field is required.");
            AssertExtras.Raises<ValidationException>(delegate {
                target.Clean("abc");
            }).WithMessage("Enter a number.");

            target.MinValue = (float?)5;
            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean("4.65"); 
            }).WithMessage("Ensure this value is greater than or equal to 5.");
            Assert.AreEqual((float?)5.5, (float?)target.Clean("5.5"));

            target.MaxValue = (float?)10.3;
            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean("15"); 
            }).WithMessage("Ensure this value is less than or equal to 10.3.");
            Assert.AreEqual((float?)10.2, (float?)target.Clean("10.2"));

            target.Required = false;
            Assert.IsNull(target.Clean(null));
        }

        /// <summary>
        ///A test for FloatField Constructor
        ///</summary>
        [TestMethod()]
        public void FloatFieldConstructorTest1()
        {
            FloatField target = new FloatField();
        }

        /// <summary>
        ///A test for FloatField Constructor
        ///</summary>
        [TestMethod()]
        public void FloatFieldConstructorTest()
        {
            IWidget widget = null;
            FloatField target = new FloatField(widget);
        }
    }
}
