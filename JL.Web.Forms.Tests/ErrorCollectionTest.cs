using JL.Web.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for ErrorListTest and is intended
    ///to contain all ErrorListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ErrorCollectionTest
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
            // Is a wrapper around AsUL, assuming no exceptions this will be tested by AsULTest
            ErrorCollection target = new ErrorCollection();
            Assert.AreEqual(target.AsUL(), target.ToString());
        }

        /// <summary>
        ///A test for AsUL
        ///</summary>
        [TestMethod()]
        public void AsULTest1()
        {
            ErrorCollection target = new ErrorCollection();
            string errorClass = "errorlist";
            string actual;

            actual = target.AsUL(errorClass);
            Assert.AreEqual(string.Empty, actual);

            target.Add("Test 1");
            actual = target.AsUL(errorClass);
            Assert.AreEqual("<ul class=\"errorlist\"><li>Test 1</li></ul>", actual);
        }

        /// <summary>
        ///A test for AsUL
        ///</summary>
        [TestMethod()]
        public void AsULTest()
        {
            ErrorCollection target = new ErrorCollection();
            string actual;

            actual = target.AsUL();
            Assert.AreEqual(string.Empty, actual);

            target.Add("Test 1");
            actual = target.AsUL();
            Assert.AreEqual("<ul class=\"errors\"><li>Test 1</li></ul>", actual);

            target.Add("Test 2");
            actual = target.AsUL();
            Assert.AreEqual("<ul class=\"errors\"><li>Test 1</li><li>Test 2</li></ul>", actual);
        }

        /// <summary>
        ///A test for AsText
        ///</summary>
        [TestMethod()]
        public void AsTextTest()
        {
            ErrorCollection target = new ErrorCollection();
            string actual;

            actual = target.AsText();
            Assert.AreEqual(string.Empty, actual);

            target.Add("Test 1");
            actual = target.AsText();
            Assert.AreEqual("* Test 1\n", actual);

            target.Add("Test 2");
            actual = target.AsText();
            Assert.AreEqual("* Test 1\n* Test 2\n", actual);
        }

        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            ErrorCollection target = new ErrorCollection();
            string actual;

            target.Add("This is an {0} of {2} + {1}", "example", 2, 3);
            actual = target.AsText();
            Assert.AreEqual("* This is an example of 3 + 2\n", actual);
        }

        /// <summary>
        ///A test for ErrorList Constructor
        ///</summary>
        [TestMethod()]
        public void ErrorListConstructorTest()
        {
            ErrorCollection target = new ErrorCollection();
            // Provided there are no exceptions this is ok
        }
    }
}
