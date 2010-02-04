using MvcForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for NameObjectDictionaryTest and is intended
    ///to contain all NameObjectDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NameObjectDictionaryTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }


        [TestMethod()]
        public void TryGetValueTest()
        {
            var target = new NameObjectDictionary {
                {"Test1", 42},
                {"Test2", "Test2Value"},
                {"Test3", null},
                {"Test4", string.Empty},
            };

            int varInt;
            Assert.IsTrue(target.TryGetValue("Test1", out varInt));
            Assert.AreEqual(42, varInt);

            string varString;
            Assert.IsTrue(target.TryGetValue("Test2", out varString));
            Assert.AreEqual("Test2Value", varString);

            object varObject;
            Assert.IsTrue(target.TryGetValue("Test3", out varObject));
            Assert.IsNull(varObject);

            Assert.IsTrue(target.TryGetValue("Test4", out varObject));
            Assert.AreEqual(string.Empty, varObject);

            Assert.IsFalse(target.TryGetValue("Test5", out varObject));
        }

        /// <summary>
        ///A test for NameObjectDictionary Constructor
        ///</summary>
        [TestMethod()]
        public void NameObjectDictionaryConstructorTest1()
        {
            var target = new NameObjectDictionary();
            // Pass
        }
    }
}
