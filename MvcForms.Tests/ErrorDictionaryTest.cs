using MvcForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;


namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for ErrorDictionaryTest and is intended
    ///to contain all ErrorDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ErrorDictionaryTest
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
            // ToString is a wrapper around AsUL assume AsUL test covers this.
            ErrorDictionary target = new ErrorDictionary();
            Assert.AreEqual(target.AsUL(), target.ToString());
        }

        /// <summary>
        ///A test for AsUL
        ///</summary>
        [TestMethod()]
        public void AsULTest1()
        {
            ErrorDictionary target = new ErrorDictionary();
            string actual;
            string errorClass = "errors";

            ErrorCollection field1 = new ErrorCollection();
            field1.Add("Test 1");
            target.Add("field1", field1);
            actual = target.AsUL(errorClass);
            Assert.AreEqual("<ul class=\"errors\"><li><ul class=\"errors\"><li><label for=\"id_field1\">Test 1</label></li></ul></li></ul>", actual);
        }

        /// <summary>
        ///A test for AsUL
        ///</summary>
        [TestMethod()]
        public void AsULTest()
        {
            ErrorDictionary target = new ErrorDictionary();
            string actual;

            actual = target.AsUL();
            Assert.AreEqual(string.Empty, actual);

            ErrorCollection field1 = new ErrorCollection();
            field1.Add("Test 1");
            target.Add("field1", field1);
            actual = target.AsUL();
            Assert.AreEqual("<ul class=\"errorlist\"><li><ul class=\"errorlist\"><li><label for=\"id_field1\">Test 1</label></li></ul></li></ul>", actual);

            ErrorCollection field2 = new ErrorCollection();
            field2.Add("Test 2");
            target.Add("field2", field2);
            actual = target.AsUL();
            Assert.AreEqual("<ul class=\"errorlist\"><li><ul class=\"errorlist\"><li><label for=\"id_field1\">Test 1</label></li></ul></li><li><ul class=\"errorlist\"><li><label for=\"id_field2\">Test 2</label></li></ul></li></ul>", actual);

            target["field1"].Add("Test 3");
            actual = target.AsUL();
            Assert.AreEqual("<ul class=\"errorlist\"><li><ul class=\"errorlist\"><li><label for=\"id_field1\">Test 1</label></li><li><label for=\"id_field1\">Test 3</label></li></ul></li><li><ul class=\"errorlist\"><li><label for=\"id_field2\">Test 2</label></li></ul></li></ul>", actual);
        }

        /// <summary>
        ///A test for AsText
        ///</summary>
        [TestMethod()]
        public void AsTextTest()
        {
            ErrorDictionary target = new ErrorDictionary();
            string actual;

            actual = target.AsText();
            Assert.AreEqual(string.Empty, actual);

            ErrorCollection field1 = new ErrorCollection();
            field1.Add("Test 1");
            target.Add("field1", field1);
            actual = target.AsText();
            Assert.AreEqual("* Test 1", actual);

            ErrorCollection field2 = new ErrorCollection();
            field2.Add("Test 2");
            target.Add("field2", field2);
            actual = target.AsText();
            Assert.AreEqual("* Test 1\n* Test 2", actual);

            target["field1"].Add("Test 3");
            actual = target.AsText();
            Assert.AreEqual("* Test 1\n* Test 3\n* Test 2", actual);
        }

        /// <summary>
        ///A test for ErrorDictionary Constructor
        ///</summary>
        [TestMethod()]
        public void ErrorDictionaryConstructorTest()
        {
            ErrorDictionary target = new ErrorDictionary();
            // If no exception is thrown this is a pass
        }

        /// <summary>
        ///A test for ErrorDictionary Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MvcForms.dll")]
        public void ErrorDictionaryConstructorTest1()
        {
            SerializationInfo info = null;
            StreamingContext context = new StreamingContext();
            ErrorDictionary_Accessor target = new ErrorDictionary_Accessor(info, context);
            // If no exception is thrown this is a pass
        }
    }
}
