using JL.Web.Forms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for EmailFieldTest and is intended
    ///to contain all EmailFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EmailFieldTest
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
            var target = new EmailField();

            // OK values
            target.Clean("test_person@example.com");
            target.Clean("test_person@example.at");
            target.Clean("test.person@example.com");
            target.Clean("test+person@example.com");

            // Bad values
            AssertExtras.Raises<ValidationException>(delegate() { target.Clean("test+person"); });
            AssertExtras.Raises<ValidationException>(delegate() { target.Clean("test+person@"); });
            AssertExtras.Raises<ValidationException>(delegate() { target.Clean("test+person@example"); });
        }

        /// <summary>
        ///A test for EmailField Constructor
        ///</summary>
        [TestMethod()]
        public void EmailFieldConstructorTest1()
        {
            EmailField target = new EmailField();
            // No error is a pass
        }

        /// <summary>
        ///A test for EmailField Constructor
        ///</summary>
        [TestMethod()]
        public void EmailFieldConstructorTest()
        {
            var target = new EmailField(new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }
    }
}
