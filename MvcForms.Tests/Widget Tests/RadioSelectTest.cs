using MvcForms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms;
using System.Collections.Generic;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for RadioSelectTest and is intended
    ///to contain all RadioSelectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RadioSelectTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for IdForLabel
        ///</summary>
        [TestMethod()]
        public void IdForLabelTest()
        {
            var target = new RadioSelect();
            Assert.AreEqual("Test1_0", target.IdForLabel("Test1"));
        }

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            var target = new RadioSelect();
            target.Choices = ChoiceHelper.Range(1, 5);

            Assert.AreEqual("<ul>\n<li><label>1 <input type=\"radio\" name=\"Test1\" value=\"1\" /></label></li>\n<li><label>2 <input type=\"radio\" name=\"Test1\" value=\"2\" /></label></li>\n<li><label>3 <input type=\"radio\" name=\"Test1\" value=\"3\" /></label></li>\n<li><label>4 <input type=\"radio\" name=\"Test1\" value=\"4\" /></label></li>\n<li><label>5 <input type=\"radio\" name=\"Test1\" value=\"5\" /></label></li>\n</ul>",
                target.Render("Test1", null));

            Assert.AreEqual("<ul>\n<li><label>1 <input type=\"radio\" name=\"Test1\" value=\"1\" /></label></li>\n<li><label>2 <input type=\"radio\" name=\"Test1\" value=\"2\" checked=\"checked\" /></label></li>\n<li><label>3 <input type=\"radio\" name=\"Test1\" value=\"3\" /></label></li>\n<li><label>4 <input type=\"radio\" name=\"Test1\" value=\"4\" /></label></li>\n<li><label>5 <input type=\"radio\" name=\"Test1\" value=\"5\" /></label></li>\n</ul>",
                target.Render("Test1", "2"));
        }

        /// <summary>
        ///A test for Renderer
        ///</summary>
        [TestMethod()]
        public void RendererTest()
        {
            var target = new RadioSelect();
            target.Renderer = delegate(string name, string value, ElementAttributesDictionary attributes, IEnumerable<Choice> choices)
            {
                return "Test1";
            };

            Assert.AreEqual("Test1", target.Renderer(null, null, null, null));
        }

        /// <summary>
        ///A test for RadioSelect Constructor
        ///</summary>
        [TestMethod()]
        public void RadioSelectConstructorTest()
        {
            var target = new RadioSelect();
            // Pass
        }
    }
}
