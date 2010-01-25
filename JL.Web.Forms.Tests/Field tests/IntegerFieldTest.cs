using JL.Web.Forms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using System;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for IntegerFieldTest and is intended
    ///to contain all IntegerFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IntegerFieldTest
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
            IntegerField target = new IntegerField();

            // Default test
            Assert.AreEqual(5, (int?)target.Clean("5"));
            Assert.AreEqual(-5, (int?)target.Clean("-5"));

            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean(null); 
            }).WithMessage("This field is required.");
            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean("abc"); 
            }).WithMessage("Enter a whole number.");
            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean("4.1"); 
            }).WithMessage("Enter a whole number.");

            target.MinValue = (int?)5;
            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean("3"); 
            }).WithMessage("Ensure this value is greater than or equal to 5.");
            Assert.AreEqual(6, (int?)target.Clean("6"));

            target.MaxValue = (int?)24;
            AssertExtras.Raises<ValidationException>(delegate { 
                target.Clean("27"); 
            }).WithMessage("Ensure this value is less than or equal to 24.");
            Assert.AreEqual(23, (int?)target.Clean("23"));

            target.Required = false;
            Assert.IsNull(target.Clean(null));
            Assert.IsNull(target.Clean(""));
            Assert.IsNull(target.Clean(" "));
        }

        /// <summary>
        ///A test for IntegerField Constructor
        ///</summary>
        [TestMethod()]
        public void IntegerFieldConstructorTest1()
        {
            IntegerField target = new IntegerField();
        }

        /// <summary>
        ///A test for IntegerField Constructor
        ///</summary>
        [TestMethod()]
        public void IntegerFieldConstructorTest()
        {
            IWidget widget = null;
            IntegerField target = new IntegerField(widget);
        }
    }
}
