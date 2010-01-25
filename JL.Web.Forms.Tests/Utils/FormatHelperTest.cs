using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms.Utils;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    /// This is a test class for FormatHelperTest and is intended
    /// to contain all FormatHelperTest Unit Tests
    /// </summary>
    [TestClass()]
    public class FormatHelperTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for BeautifyName
        /// </summary>
        [TestMethod()]
        public void BeautifyNameTest1()
        {
            Assert.AreEqual("This Is My Label", FormatHelper.BeautifyName("ThisIsMyLabel", true));
        }

        /// <summary>
        /// A test for BeautifyName
        /// </summary>
        [TestMethod()]
        public void BeautifyNameTest()
        {
            Assert.AreEqual("This is my label", FormatHelper.BeautifyName("ThisIsMyLabel"));
            Assert.AreEqual("This is my label", FormatHelper.BeautifyName("this_is_my_label"));
        }
    }
}
