using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for UrlFieldTest and is intended
    ///to contain all UrlFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class UrlFieldTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Clean method
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            var target = new UrlField();

            // OK values
            target.Clean("http://www.example.com/");

            // Bad values
            AssertExtras.Raises<ValidationException>(delegate() { target.Clean("blah"); });
        }

        /// <summary>
        ///A test for UrlField Constructor
        ///</summary>
        [TestMethod()]
        public void UrlFieldConstructorTest1()
        {
            UrlField target = new UrlField();
            // No error is a pass
        }

        /// <summary>
        ///A test for UrlField Constructor
        ///</summary>
        [TestMethod()]
        public void UrlFieldConstructorTest()
        {
            var target = new UrlField(new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }
    }
}
