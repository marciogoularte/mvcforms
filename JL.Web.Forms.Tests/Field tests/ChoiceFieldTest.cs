using System.Collections.Generic;
using JL.Web.Forms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for ChoiceFieldTest and is intended
    ///to contain all ChoiceFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChoiceFieldTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Widget
        ///</summary>
        [TestMethod()]
        public void WidgetTest()
        {
            ChoiceField target = new ChoiceField();
            Assert.IsInstanceOfType(target.Widget, typeof(JL.Web.Forms.Widgets.Select));
        }

        /// <summary>
        ///A test for ShowDefault
        ///</summary>
        [TestMethod()]
        public void ShowDefaultTest()
        {
            ChoiceField target = new ChoiceField();

            target.ShowDefault = false;
            Assert.IsFalse(target.Widget.ShowDefault);

            target.ShowDefault = true;
            Assert.IsTrue(target.Widget.ShowDefault);

            target.Widget.ShowDefault = false;
            Assert.IsFalse(target.ShowDefault);

            target.Widget.ShowDefault = true;
            Assert.IsTrue(target.ShowDefault);

        }

        /// <summary>
        ///A test for DefaultLabel
        ///</summary>
        [TestMethod()]
        public void DefaultLabelTest()
        {
            ChoiceField target = new ChoiceField();

            target.DefaultLabel = "Test1";
            Assert.AreEqual("Test1", target.Widget.DefaultLabel);

            target.Widget.DefaultLabel = "Test2";
            Assert.AreEqual("Test2", target.DefaultLabel);
        }

        /// <summary>
        ///A test for Choices
        ///</summary>
        [TestMethod()]
        public void ChoicesTest()
        {
            ChoiceField target = new ChoiceField
            {
                Choices = JL.Web.Forms.Widgets.Choices.AustralianStates.Abbreviated
            };

            Assert.AreSame(target.Choices, JL.Web.Forms.Widgets.Choices.AustralianStates.Abbreviated);
        }

        /// <summary>
        ///A test for ValidValue
        ///</summary>
        [TestMethod()]
        public void ValidValueTest()
        {
            ChoiceField target = new ChoiceField
            {
                Choices = JL.Web.Forms.Widgets.Choices.AustralianStates.Abbreviated
            };

            Assert.IsTrue(target.ValidValue("QLD"));
            Assert.IsFalse(target.ValidValue("CA"));
        }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            ChoiceField target = new ChoiceField
            {
                Choices = JL.Web.Forms.Widgets.Choices.AustralianStates.Abbreviated
            };

            AssertExtras.Raises<ValidationException>(delegate() {
                target.Clean(null);
            }).WithMessage("This field is required.");

            target.Required = false;
            Assert.IsNull(target.Clean(null));
            Assert.AreEqual("QLD", target.Clean("QLD"));
            AssertExtras.Raises<ValidationException>(delegate()
            {
                target.Clean("CA");
            });
        }

        /// <summary>
        ///A test for ChoiceField Constructor
        ///</summary>
        [TestMethod()]
        public void ChoiceFieldConstructorTest1()
        {
            ChoiceField target = new ChoiceField();
            // Assume ok if no exception thrown
        }

        /// <summary>
        ///A test for ChoiceField Constructor
        ///</summary>
        [TestMethod()]
        public void ChoiceFieldConstructorTest()
        {
            ChoiceField target = new ChoiceField(new JL.Web.Forms.Widgets.SelectMultiple());
            Assert.IsInstanceOfType(target.Widget, typeof(JL.Web.Forms.Widgets.SelectMultiple));
        }
    }
}
