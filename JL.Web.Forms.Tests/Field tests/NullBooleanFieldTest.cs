using JL.Web.Forms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for NullBooleanFieldTest and is intended
    ///to contain all NullBooleanFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NullBooleanFieldTest
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
            NullBooleanField target = new NullBooleanField();

            Assert.AreEqual(false, target.Clean(false));
            Assert.AreEqual(false, target.Clean(bool.FalseString));
            Assert.AreEqual(false, target.Clean("0"));
            Assert.AreEqual(false, target.Clean(0));
            Assert.AreEqual(false, target.Clean("off"));

            Assert.AreEqual(true, target.Clean(true));
            Assert.AreEqual(true, target.Clean(bool.TrueString));
            Assert.AreEqual(true, target.Clean("1"));
            Assert.AreEqual(true, target.Clean(1));
            Assert.AreEqual(true, target.Clean("on"));

            Assert.IsNull(target.Clean(null));
            Assert.IsNull(target.Clean("asdfakjsdfaksjdhf"));
            Assert.IsNull(target.Clean(string.Empty));
            Assert.IsNull(target.Clean(" "));
        }

        /// <summary>
        ///A test for NullBooleanField Constructor
        ///</summary>
        [TestMethod()]
        public void NullBooleanFieldConstructorTest1()
        {
            NullBooleanField target = new NullBooleanField();
        }

        /// <summary>
        ///A test for NullBooleanField Constructor
        ///</summary>
        [TestMethod()]
        public void NullBooleanFieldConstructorTest()
        {
            IWidget widget = null;
            NullBooleanField target = new NullBooleanField(widget);
        }
    }
}
