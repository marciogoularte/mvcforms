using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for FilePathFieldTest and is intended
    ///to contain all FilePathFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FilePathFieldTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void ParameterTest()
        {
            var target = new FilePathField
            {
                Path = @"C:\windows\",
                SearchPattern = "*.txt",
                Recursive = true,
            };

            Assert.AreEqual(@"C:\windows\", target.Path);
            Assert.AreEqual("*.txt", target.SearchPattern);
            Assert.AreEqual(true, target.Recursive);
        }

        /// <summary>
        ///A test for FilePathField Constructor
        ///</summary>
        [TestMethod()]
        public void FilePathFieldConstructorTest1()
        {
            var target = new FilePathField();
            // No error is a pass
        }

        /// <summary>
        ///A test for FilePathField Constructor
        ///</summary>
        [TestMethod()]
        public void FilePathFieldConstructorTest()
        {
            var target = new FilePathField(new MvcForms.Widgets.SelectMultiple());
            Assert.IsInstanceOfType(target.Widget, typeof(MvcForms.Widgets.SelectMultiple));
        }
    }
}
