using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using JL.Web.Forms.Utils;

namespace JL.Web.Forms.Tests
{
    
    
    /// <summary>
    ///This is a test class for CheckBoxInputTest and is intended
    ///to contain all CheckBoxInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CheckBoxInputTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for CheckTest
        ///</summary>
        [TestMethod()]
        public void CheckTestTest()
        {
            var target = new CheckBoxInput();
            Assert.AreEqual(ConversionHelper.Boolean, target.CheckTest);
            target.CheckTest = v => false;
            Assert.IsFalse(target.CheckTest("eek"));
        }

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            var target = new CheckBoxInput();
            Assert.AreEqual("<input type=\"checkbox\" name=\"test\" />", target.Render("test", null));
            Assert.AreEqual("<input type=\"checkbox\" name=\"test\" value=\"False\" />", target.Render("test", false));
            Assert.AreEqual("<input type=\"checkbox\" name=\"test\" checked=\"checked\" value=\"test1\" />", target.Render("test", "test1"));
        }

        /// <summary>
        ///A test for CheckBoxInput Constructor
        ///</summary>
        [TestMethod()]
        public void CheckBoxInputConstructorTest()
        {
            var target = new CheckBoxInput();
        }
    }
}
