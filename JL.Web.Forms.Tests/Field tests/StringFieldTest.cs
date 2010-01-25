using JL.Web.Forms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using JL.Web.Forms.Extensions;
using System;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for StringFieldTest and is intended
    ///to contain all StringFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringFieldTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for MinLength
        ///</summary>
        [TestMethod()]
        public void MinLengthTest()
        {
            var target = new StringField
            {
                MinLength = 5
            };
            Assert.AreEqual(5, target.MinLength);
        }

        /// <summary>
        ///A test for MaxLength
        ///</summary>
        [TestMethod()]
        public void MaxLengthTest()
        {
            var target = new StringField
            {
                MaxLength = 5
            };
            Assert.AreEqual(5, target.MaxLength);
        }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            var target = new StringField();

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean(null);
            }).WithMessage("This field is required.");

            target.Required = false;
            Assert.AreEqual(null, target.Clean(null));

            target = new StringField
            {
                MinLength = 10,
                MaxLength = 15
            };

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean("Too Short");
            }).WithMessage("Ensure this value has at least 10 characters (it has 9).");

            AssertExtras.Raises<ValidationException>(delegate
            {
                target.Clean("This is too Long");
            }).WithMessage("Ensure this value has at most 15 characters (it has 16).");

            Assert.AreEqual("This is ok.", target.Clean("This is ok."));
        }

        /// <summary>
        ///A test for AppendWidgetAttributes
        ///</summary>
        [TestMethod()]
        public void AppendWidgetAttributesTest()
        {
            var target = new StringField();

            var test1 = new ElementAttributesDictionary();
            target.AppendWidgetAttributes(new Widgets.TextInput(), test1);
            Assert.AreEqual(null, test1.Get("maxlength", null));

            target.MaxLength = 10;

            var test2 = new ElementAttributesDictionary();
            target.AppendWidgetAttributes(new Widgets.TextArea(), test2);
            Assert.AreEqual(null, test2.Get("maxlength", null));

            var test3 = new ElementAttributesDictionary();
            target.AppendWidgetAttributes(new Widgets.TextInput(), test3);
            Assert.AreEqual("10", test3.Get("maxlength", null));
        }

        /// <summary>
        ///A test for StringField Constructor
        ///</summary>
        [TestMethod()]
        public void StringFieldConstructorTest1()
        {
            StringField target = new StringField();
            // All good if no exception thrown
        }

        /// <summary>
        ///A test for StringField Constructor
        ///</summary>
        [TestMethod()]
        public void StringFieldConstructorTest()
        {
            StringField target = new StringField(new Widgets.TextArea());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.TextArea));
        }
    }
}
