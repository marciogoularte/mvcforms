using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for BooleanFieldTest and is intended
    ///to contain all BooleanFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BooleanFieldTest
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
            BooleanField target = new BooleanField(); // TODO: Initialize to an appropriate value
            
            // Required
            AssertExtras.Raises<ValidationException>(
                () => target.Clean(null)
            ).WithMessage("This field is required.");
            
            Assert.IsTrue((bool)target.Clean(true));
            Assert.IsTrue((bool)target.Clean(bool.TrueString));
            Assert.IsTrue((bool)target.Clean("1"));
            Assert.IsTrue((bool)target.Clean(1));
            Assert.IsTrue((bool)target.Clean("on"));
            Assert.IsTrue((bool)target.Clean("asdfakjsdfaksjdhf"));

            target.Required = false;
            Assert.IsFalse((bool)target.Clean(null));
            Assert.IsFalse((bool)target.Clean(false));
            Assert.IsFalse((bool)target.Clean(string.Empty));
            Assert.IsFalse((bool)target.Clean(bool.FalseString));
            Assert.IsFalse((bool)target.Clean("0"));
            Assert.IsFalse((bool)target.Clean(0));
            Assert.IsFalse((bool)target.Clean(" "));
            Assert.IsFalse((bool)target.Clean("off"));
        }

        /// <summary>
        ///A test for BooleanField Constructor
        ///</summary>
        [TestMethod()]
        public void BooleanFieldConstructorTest1()
        {
            BooleanField target = new BooleanField();
        }

        /// <summary>
        ///A test for BooleanField Constructor
        ///</summary>
        [TestMethod()]
        public void BooleanFieldConstructorTest()
        {
            IWidget widget = null;
            BooleanField target = new BooleanField(widget);
        }
    }
}
