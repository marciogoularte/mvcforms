using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for IPAddressFieldTest and is intended
    ///to contain all IPAddressFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IPAddressFieldTest
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
            var target = new IPAddressField();

            // OK values
            target.Clean("127.0.0.1");
            target.Clean("192.168.11.1");

            // Bad values
            AssertExtras.Raises<ValidationException>(delegate() { target.Clean("blah"); });
        }

        /// <summary>
        ///A test for IPAddressField Constructor
        ///</summary>
        [TestMethod()]
        public void IPAddressFieldConstructorTest1()
        {
            IPAddressField target = new IPAddressField();
            // No error is a pass
        }

        /// <summary>
        ///A test for IPAddressField Constructor
        ///</summary>
        [TestMethod()]
        public void IPAddressFieldConstructorTest()
        {
            var target = new IPAddressField(new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }
    }
}
