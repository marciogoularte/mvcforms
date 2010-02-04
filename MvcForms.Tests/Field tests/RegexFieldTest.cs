using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;
using System.Text.RegularExpressions;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for RegexFieldTest and is intended
    ///to contain all RegexFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RegexFieldTest
    {
        const string TestRegex = @"^[-a-z]+$"; // Test Regex filters input by a-z and - chars

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for RegularExpression
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MvcForms.dll")]
        public void RegularExpressionTest()
        {
            RegexFieldConstructorTest3();
        }

        /// <summary>
        ///A test for Pattern
        ///</summary>
        [TestMethod()]
        public void PatternTest()
        {
            RegexFieldConstructorTest2();
        }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            var target = new RegexField(TestRegex);

            // Default test
            Assert.AreEqual("test", target.Clean("test"));
            Assert.AreEqual("test-one", target.Clean("test-one"));

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean(null);
            }).WithMessage("This field is required.");

            target.Required = false;
            Assert.AreEqual(null, target.Clean(null));

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean("Bad Value");
            }).WithMessage("Enter a valid value.");
        }

        /// <summary>
        ///A test for RegexField Constructor
        ///</summary>
        [TestMethod()]
        public void RegexFieldConstructorTest3()
        {
            var re = new Regex(TestRegex);
            var target = new RegexField(re);
            Assert.AreEqual(re, target.RegularExpression);
        }

        /// <summary>
        ///A test for RegexField Constructor
        ///</summary>
        [TestMethod()]
        public void RegexFieldConstructorTest2()
        {
            var target = new RegexField(TestRegex);
            Assert.AreEqual(TestRegex, target.Pattern);
        }

        /// <summary>
        ///A test for RegexField Constructor
        ///</summary>
        [TestMethod()]
        public void RegexFieldConstructorTest1()
        {
            var re = new Regex(TestRegex);
            var target = new RegexField(re, new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }

        /// <summary>
        ///A test for RegexField Constructor
        ///</summary>
        [TestMethod()]
        public void RegexFieldConstructorTest()
        {
            var target = new RegexField(TestRegex, new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }
    }
}
