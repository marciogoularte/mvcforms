using System.Collections.ObjectModel;
using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for MultipleChoiceFieldTest and is intended
    ///to contain all MultipleChoiceFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MultipleChoiceFieldTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            var target = new MultipleChoiceField()
            {
                Choices = Widgets.Choices.AustralianStates.Abbreviated
            };

            AssertExtras.Raises<ValidationException>(delegate() {
                target.Clean(null);
            }).WithMessage("This field is required.");
            
            target.Required = false;

            var result = target.Clean(null) as Collection<object>;
            Assert.IsInstanceOfType(result, typeof(Collection<object>));
            Assert.AreEqual(0, result.Count);

            var validCollection = new Collection<object> { "QLD", "SA" };
            var invalidCollection = new Collection<object> { "QLD", "SA", "CA" };

            AssertExtras.Raises<ValidationException>(delegate()
            {
                target.Clean("123");
            }).WithMessage("Select a valid choice. \"123\" is not one of the available choices.");

            AssertExtras.ListIsEqual(validCollection, target.Clean(validCollection) as IList);
            AssertExtras.Raises<ValidationException>(delegate()
            {
                target.Clean(invalidCollection);
            }).WithMessage("Select a valid choice. \"CA\" is not one of the available choices.");
        }

        /// <summary>
        ///A test for MultipleChoiceField Constructor
        ///</summary>
        [TestMethod()]
        public void MultipleChoiceFieldConstructorTest1()
        {
            MultipleChoiceField target = new MultipleChoiceField();
            // Hitting this is a pass
        }

        /// <summary>
        ///A test for MultipleChoiceField Constructor
        ///</summary>
        [TestMethod()]
        public void MultipleChoiceFieldConstructorTest()
        {
            MultipleChoiceField target = new MultipleChoiceField(new Widgets.RadioSelect());
            Assert.IsInstanceOfType(target.Widget, typeof(Widgets.RadioSelect));
        }
    }
}
