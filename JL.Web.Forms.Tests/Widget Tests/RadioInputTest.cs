using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using JL.Web.Forms.Extensions;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for RadioInputTest and is intended
    ///to contain all RadioInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RadioInputTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            var attributes = new ElementAttributesDictionary();
            attributes.AppendObject(new { id = "TestID" }); 
            IChoice choice = new Choice("Test1");
            int index = 4;

            RadioInput target = new RadioInput("TestVal", "Test", attributes, choice, index);
            Assert.AreEqual("<label for=\"TestID_4\">Test1 <input id=\"TestID_4\" type=\"radio\" name=\"TestVal\" value=\"Test1\" /></label>", target.ToString());

            target = new RadioInput("TestVal", "Test1", attributes, choice, index);
            Assert.AreEqual("<label for=\"TestID_4\">Test1 <input id=\"TestID_4\" type=\"radio\" name=\"TestVal\" value=\"Test1\" checked=\"checked\" /></label>", target.ToString());
        }

        /// <summary>
        ///A test for RadioInput Constructor
        ///</summary>
        [TestMethod()]
        public void RadioInputConstructorTest()
        {
            string name = "Test1";
            string value = "TestVal";
            ElementAttributesDictionary attributes = new ElementAttributesDictionary();
            IChoice choice = new Choice("test");
            int index = 4;
            RadioInput target = new RadioInput(name, value, attributes, choice, index);

            Assert.AreEqual(name, target.Name);
            Assert.AreEqual(value, target.Value);
            Assert.AreEqual(index, target.Index);
            Assert.AreSame(choice, target.Choice);
            AssertExtras.DictionaryIsEqual(attributes, target.Attributes);
        }
    }
}
