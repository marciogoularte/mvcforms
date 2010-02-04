using MvcForms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;
using System.Text;
using System.Collections.Generic;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for SelectTest and is intended
    ///to contain all SelectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SelectTest
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
            var target = new Select();

            Assert.AreEqual("<select name=\"Test1\">\n</select>", 
                target.Render("Test1", "1"));

            target.ShowDefault = true;
            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"\">Select...</option>\n</select>",
                target.Render("Test1", "1"));

            target.DefaultLabel = "Pick me!";
            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"\">Pick me!</option>\n</select>",
                target.Render("Test1", "1"));

            target.ShowDefault = false;
            target.Choices = ChoiceHelper.Range(1, 5);
            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">1</option>\n<option value=\"2\">2</option>\n<option value=\"3\">3</option>\n<option value=\"4\">4</option>\n<option value=\"5\">5</option>\n</select>",
                target.Render("Test1", null));
            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">1</option>\n<option value=\"2\" selected=\"selected\">2</option>\n<option value=\"3\">3</option>\n<option value=\"4\">4</option>\n<option value=\"5\">5</option>\n</select>",
                target.Render("Test1", "2"));
        }

        /// <summary>
        ///A test for Select Constructor
        ///</summary>
        [TestMethod()]
        public void SelectConstructorTest()
        {
            var target = new Select();
            // Pass
        }
    }
}
