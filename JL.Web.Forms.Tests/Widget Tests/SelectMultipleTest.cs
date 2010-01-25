using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System.Web;
using JL.Web.Forms;
using System.Collections;
using System.Collections.Generic;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for SelectMultipleTest and is intended
    ///to contain all SelectMultipleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SelectMultipleTest
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
            SelectMultiple target = new SelectMultiple();
            target.Choices = ChoiceHelper.Range(1, 5);

            Assert.AreEqual("<select multiple=\"multiple\" name=\"Test1\">\n<option value=\"1\">1</option>\n<option value=\"2\">2</option>\n<option value=\"3\">3</option>\n<option value=\"4\">4</option>\n<option value=\"5\">5</option>\n</select>",
                target.Render("Test1", null));
            Assert.AreEqual("<select multiple=\"multiple\" name=\"Test1\">\n<option value=\"1\">1</option>\n<option value=\"2\" selected=\"selected\">2</option>\n<option value=\"3\">3</option>\n<option value=\"4\">4</option>\n<option value=\"5\">5</option>\n</select>",
                target.Render("Test1", "2"));
            Assert.AreEqual("<select multiple=\"multiple\" name=\"Test1\">\n<option value=\"1\">1</option>\n<option value=\"2\" selected=\"selected\">2</option>\n<option value=\"3\">3</option>\n<option value=\"4\" selected=\"selected\">4</option>\n<option value=\"5\" selected=\"selected\">5</option>\n</select>",
                target.Render("Test1", new List<object> { "2", "4", "5" }));
        }

        /// <summary>
        ///A test for GetValueFromDataCollection
        ///</summary>
        [TestMethod()]
        public void GetValueFromDataCollectionTest()
        {
            var data = new NameValueCollection();
            var target = new SelectMultiple();

            Assert.AreEqual(null, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = "";
            Assert.AreEqual(null, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = "1,2,3,4,5";
            AssertExtras.AreEqual(new string[] {"1","2","3","4","5"}, 
                target.GetValueFromDataCollection(data, null, "test") as IEnumerable);
        }

        /// <summary>
        ///A test for SelectMultiple Constructor
        ///</summary>
        [TestMethod()]
        public void SelectMultipleConstructorTest()
        {
            SelectMultiple target = new SelectMultiple();
            // Pass
        }
    }
}
