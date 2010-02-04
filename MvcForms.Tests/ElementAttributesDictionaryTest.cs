using MvcForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for ElementAttributesDictionaryTest and is intended
    ///to contain all ElementAttributesDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ElementAttributesDictionaryTest
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
            ElementAttributesDictionary target = new ElementAttributesDictionary();
            target.Add("Test1", "Test1");
            target.Add("Test2", "Test2");
            Assert.AreEqual(" Test1=\"Test1\" Test2=\"Test2\"", target.ToString());
        }

        /// <summary>
        ///A test for AddCssClass
        ///</summary>
        [TestMethod()]
        public void AddCssClassTest()
        {
            ElementAttributesDictionary target = new ElementAttributesDictionary();

            target.AddCssClass(null);
            Assert.IsFalse(target.ContainsKey("class"));

            target.AddCssClass(string.Empty);
            Assert.IsFalse(target.ContainsKey("class"));

            target.AddCssClass("class1");
            Assert.IsTrue(target.ContainsKey("class"));
            Assert.AreEqual("class1", target["class"]);

            target.AddCssClass("class2");
            Assert.AreEqual("class1 class2", target["class"]);

            target.AddCssClass("class1");
            Assert.AreEqual("class1 class2", target["class"]);

            target.AddCssClass("class2");
            Assert.AreEqual("class1 class2", target["class"]);
        }

        /// <summary>
        ///A test for ElementAttributesDictionary Constructor
        ///</summary>
        [TestMethod()]
        public void ElementAttributesDictionaryConstructorTest2()
        {
            ElementAttributesDictionary target = new ElementAttributesDictionary();
            // If no exception is thrown this passes
        }

        /// <summary>
        ///A test for ElementAttributesDictionary Constructor
        ///</summary>
        [TestMethod()]
        public void ElementAttributesDictionaryConstructorTest1()
        {
            ElementAttributesDictionary source = new ElementAttributesDictionary();
            source.Add("Test1", "Test1");
            source.Add("Test2", "Test2");
            ElementAttributesDictionary target = new ElementAttributesDictionary(source);

            Assert.AreEqual<int>(2, target.Count);
            Assert.IsTrue(target.ContainsKey("Test1"));
            Assert.AreEqual("Test1", target["Test1"]);
            Assert.IsTrue(target.ContainsKey("Test2"));
            Assert.AreEqual("Test2", target["Test2"]);
        }

        /// <summary>
        ///A test for ElementAttributesDictionary Constructor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("MvcForms.dll")]
        public void ElementAttributesDictionaryConstructorTest()
        {
            SerializationInfo info = null;
            StreamingContext context = new StreamingContext();
            ElementAttributesDictionary_Accessor target = new ElementAttributesDictionary_Accessor(info, context);
            // If no exception is thrown this passes
        }
    }
}
