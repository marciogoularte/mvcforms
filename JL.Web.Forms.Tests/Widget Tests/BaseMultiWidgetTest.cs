using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Web;
using System.Collections;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for BaseMultiWidgetTest and is intended
    ///to contain all BaseMultiWidgetTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BaseMultiWidgetTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for GetValueFromDataCollection
        ///</summary>
        [TestMethod()]
        public void GetValueFromDataCollectionTest()
        {
            BaseMultiWidget target = CreateBaseMultiWidget(); // TODO: Initialize to an appropriate value
            var data = new NameValueCollection {
                {"Test10", "Test1"},
                {"Test11", bool.TrueString},
            };

            AssertExtras.AreEqual(new object[] { "Test1", bool.TrueString },
                target.GetValueFromDataCollection(data, null, "Test1") as IEnumerable);
        }

        /// <summary>
        ///A test for IdForLabel
        ///</summary>
        [TestMethod()]
        public void IdForLabelTest()
        {
            BaseMultiWidget target = CreateBaseMultiWidget();
            Assert.AreEqual("Test1_0", target.IdForLabel("Test1"));
        }

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            BaseMultiWidget target = CreateBaseMultiWidget();

            Assert.AreEqual("<input type=\"text\" name=\"Test10\" value=\"1\" />\n<input type=\"checkbox\" name=\"Test11\" checked=\"checked\" value=\"True\" />",
                target.Render("Test1", null));
            Assert.AreEqual("<input id=\"Test1_0\" type=\"text\" name=\"Test10\" value=\"1\" />\n<input id=\"Test1_1\" type=\"checkbox\" name=\"Test11\" checked=\"checked\" value=\"True\" />",
                target.Render("Test1", null, new ElementAttributesDictionary { { "id", "Test1" } }));
       }

        class TestBaseMultiWidget : BaseMultiWidget
        {
            public TestBaseMultiWidget()
                : base(new TextInput(), new CheckBoxInput())
            {
            }

            protected override Tuple Decompress(object value)
            {
                return new Tuple("1", bool.TrueString);
            }
        }

        internal virtual BaseMultiWidget CreateBaseMultiWidget()
        {
            return new TestBaseMultiWidget();
        }

        /// <summary>
        ///A test for Widgets
        ///</summary>
        [TestMethod()]
        public void WidgetsTest()
        {
            BaseMultiWidget target = CreateBaseMultiWidget();
            Assert.IsInstanceOfType(target.Widgets[0], typeof(TextInput));
            Assert.IsInstanceOfType(target.Widgets[1], typeof(CheckBoxInput));
        }
    }
}
