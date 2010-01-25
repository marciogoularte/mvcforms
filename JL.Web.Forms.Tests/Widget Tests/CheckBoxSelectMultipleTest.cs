using System.Collections.ObjectModel;
using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for CheckBoxSelectMultipleTest and is intended
    ///to contain all CheckBoxSelectMultipleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CheckBoxSelectMultipleTest
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
            var target = new CheckBoxSelectMultiple();
            target.Choices = ChoiceHelper.Range(1, 5);

            Assert.AreEqual("<ul>\n<li><label><input name=\"Test1\" type=\"checkbox\" value=\"1\" /> 1</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" value=\"2\" /> 2</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" value=\"3\" /> 3</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" value=\"4\" /> 4</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" value=\"5\" /> 5</label></li>\n</ul>",
                target.Render("Test1", null));
            Assert.AreEqual("<ul>\n<li><label><input name=\"Test1\" type=\"checkbox\" value=\"1\" /> 1</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" checked=\"checked\" value=\"2\" /> 2</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" value=\"3\" /> 3</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" checked=\"checked\" value=\"4\" /> 4</label></li>\n<li><label><input name=\"Test1\" type=\"checkbox\" checked=\"checked\" value=\"5\" /> 5</label></li>\n</ul>",
                target.Render("Test1", new object [] { 2, 4, 5 }));

            ElementAttributesDictionary extraAttributes = new ElementAttributesDictionary{
                {"id", "Test1"}
            };
            Assert.AreEqual("<ul>\n<li><label for=\"Test1_0\"><input id=\"Test1_0\" name=\"Test1\" type=\"checkbox\" value=\"1\" /> 1</label></li>\n<li><label for=\"Test1_1\"><input id=\"Test1_1\" name=\"Test1\" type=\"checkbox\" checked=\"checked\" value=\"2\" /> 2</label></li>\n<li><label for=\"Test1_2\"><input id=\"Test1_2\" name=\"Test1\" type=\"checkbox\" value=\"3\" /> 3</label></li>\n<li><label for=\"Test1_3\"><input id=\"Test1_3\" name=\"Test1\" type=\"checkbox\" checked=\"checked\" value=\"4\" /> 4</label></li>\n<li><label for=\"Test1_4\"><input id=\"Test1_4\" name=\"Test1\" type=\"checkbox\" checked=\"checked\" value=\"5\" /> 5</label></li>\n</ul>",
                target.Render("Test1", new object[] { 2, 4, 5 }, extraAttributes));
        }

        /// <summary>
        ///A test for IdForLabel
        ///</summary>
        [TestMethod()]
        public void IdForLabelTest()
        {
            var target = new CheckBoxSelectMultiple();
            Assert.AreEqual("Test1_0", target.IdForLabel("Test1"));
        }

        /// <summary>
        ///A test for CheckBoxSelectMultiple Constructor
        ///</summary>
        [TestMethod()]
        public void CheckBoxSelectMultipleConstructorTest()
        {
            CheckBoxSelectMultiple target = new CheckBoxSelectMultiple();
            // Pass
        }
    }
}
