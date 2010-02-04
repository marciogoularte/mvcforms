using MvcForms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System.Web;
using MvcForms;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for NullBooleanSelectTest and is intended
    ///to contain all NullBooleanSelectTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NullBooleanSelectTest
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
            NullBooleanSelect target = new NullBooleanSelect();

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\" selected=\"selected\">Unknown</option>\n<option value=\"2\">Yes</option>\n<option value=\"3\">No</option>\n</select>", 
                target.Render("Test1", null));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\" selected=\"selected\">Unknown</option>\n<option value=\"2\">Yes</option>\n<option value=\"3\">No</option>\n</select>",
                target.Render("Test1", ""));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\" selected=\"selected\">Unknown</option>\n<option value=\"2\">Yes</option>\n<option value=\"3\">No</option>\n</select>",
                target.Render("Test1", "1"));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">Unknown</option>\n<option value=\"2\" selected=\"selected\">Yes</option>\n<option value=\"3\">No</option>\n</select>",
                target.Render("Test1", bool.TrueString));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">Unknown</option>\n<option value=\"2\" selected=\"selected\">Yes</option>\n<option value=\"3\">No</option>\n</select>",
                target.Render("Test1", true));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">Unknown</option>\n<option value=\"2\" selected=\"selected\">Yes</option>\n<option value=\"3\">No</option>\n</select>",
                target.Render("Test1", "2"));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">Unknown</option>\n<option value=\"2\">Yes</option>\n<option value=\"3\" selected=\"selected\">No</option>\n</select>",
                target.Render("Test1", bool.FalseString));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">Unknown</option>\n<option value=\"2\">Yes</option>\n<option value=\"3\" selected=\"selected\">No</option>\n</select>",
                target.Render("Test1", false));

            Assert.AreEqual("<select name=\"Test1\">\n<option value=\"1\">Unknown</option>\n<option value=\"2\">Yes</option>\n<option value=\"3\" selected=\"selected\">No</option>\n</select>",
                target.Render("Test1", "3"));
        }

        /// <summary>
        ///A test for GetValueFromDataCollection
        ///</summary>
        [TestMethod()]
        public void GetValueFromDataCollectionTest()
        {
            var data = new NameValueCollection();
            var target = new NullBooleanSelect();

            Assert.AreEqual(null, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = "";
            Assert.AreEqual(null, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = "1";
            Assert.AreEqual(null, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = bool.TrueString;
            Assert.AreEqual(true, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = "2";
            Assert.AreEqual(true, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = bool.FalseString;
            Assert.AreEqual(false, target.GetValueFromDataCollection(data, null, "test"));

            data["test"] = "3";
            Assert.AreEqual(false, target.GetValueFromDataCollection(data, null, "test"));
        }

        /// <summary>
        ///A test for NullBooleanSelect Constructor
        ///</summary>
        [TestMethod()]
        public void NullBooleanSelectConstructorTest()
        {
            NullBooleanSelect target = new NullBooleanSelect();
            // Pass
        }
    }
}
