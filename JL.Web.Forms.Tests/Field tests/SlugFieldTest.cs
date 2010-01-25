using JL.Web.Forms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for SlugFieldTest and is intended
    ///to contain all SlugFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SlugFieldTest
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
            var target = new SlugField();

            // OK values
            target.Clean("this-is-slug");
            target.Clean("thisisok");

            // Bad values
            AssertExtras.Raises<ValidationException>(delegate() { target.Clean("this is not"); });
        }

        /// <summary>
        ///A test for SlugField Constructor
        ///</summary>
        [TestMethod()]
        public void SlugFieldConstructorTest1()
        {
            SlugField target = new SlugField();
            // No error is a pass
        }

        /// <summary>
        ///A test for SlugField Constructor
        ///</summary>
        [TestMethod()]
        public void SlugFieldConstructorTest()
        {
            var target = new SlugField(new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }
    }
}
