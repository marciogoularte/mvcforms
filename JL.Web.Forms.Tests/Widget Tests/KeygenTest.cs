namespace JL.Web.Forms.Tests
{
    using JL.Web.Forms.Widgets;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using JL.Web.Forms;
   
    
    /// <summary>
    ///This is a test class for KeygenTest and is intended
    ///to contain all KeygenTest Unit Tests
    ///</summary>
    [TestClass()]
    public class KeygenTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            var target = new Keygen();
            Assert.AreEqual("<keygen keytype=\"rsa\" name=\"test\" />", target.Render("test", null));
        }

        /// <summary>
        ///A test for Keygen Constructor
        ///</summary>
        [TestMethod()]
        public void KeygenConstructorTest()
        {
            var target = new Keygen();
        }
    }
}
