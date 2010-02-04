using MvcForms.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for DictionaryExtensionsTest and is intended
    ///to contain all DictionaryExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DictionaryExtensionsTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// Test for update
        /// </summary>
        [TestMethod()]
        public void UpdateTest()
        {
            Dictionary<string, string> target = new Dictionary<string, string>()
            {
                {"test1", "test1"},
                {"test2", "test2"},
                {"test3", "test3"}
            };
            Dictionary<string, string> append = new Dictionary<string, string>()
            {
                {"test4", "test4"},
                {"test5", "test5"},
                {"test2", "test6"}
            };
            Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"test1", "test1"},
                {"test2", "test2"},
                {"test3", "test3"},
                {"test4", "test4"},
                {"test5", "test5"}
            };

            target.Update(append);
            AssertExtras.DictionaryIsEqual(result, target);
            target.Update(null);
            AssertExtras.DictionaryIsEqual(result, target);
        }

        /// <summary>
        /// Test for SetDefault
        /// </summary>
        [TestMethod()]
        public void SetDefaultTest()
        {
            Dictionary<string, string> testStr = new Dictionary<string, string>()
            {
                {"test1", "test1"},
                {"test2", "test2"},
                {"test3", "test3"}
            };
            Dictionary<string, int> testInt = new Dictionary<string, int>()
            {
                {"test1", 1},
                {"test2", 2},
                {"test3", 3}
            };

            Assert.AreEqual("test1", testStr.SetDefault("test1", "abc"));
            Assert.AreEqual("abc", testStr.SetDefault("test4", "abc"));
            Assert.AreEqual("abc", testStr.SetDefault("test4", "cba"));
            Assert.AreEqual(2, testInt.SetDefault("test2", 4));
            Assert.AreEqual(4, testInt.SetDefault("test4", 4));
            Assert.AreEqual(4, testInt.SetDefault("test4", 5));
        }

        /// <summary>
        /// Test for Get
        /// </summary>
        [TestMethod()]
        public void GetTest1()
        {
            Dictionary<string, string> testStr = new Dictionary<string, string>()
            {
                {"test1", "test1"},
                {"test2", "test2"},
                {"test3", "test3"}
            };
            Dictionary<string, int> testInt = new Dictionary<string, int>()
            {
                {"test1", 1},
                {"test2", 2},
                {"test3", 3}
            };

            Assert.AreEqual("test1", testStr.Get("test1"));
            Assert.AreEqual(null, testStr.Get("test4"));
            Assert.AreEqual(null, testStr.Get("test4"));
            Assert.AreEqual(2, testInt.Get("test2"));
            Assert.AreEqual(0, testInt.Get("test4"));
            Assert.AreEqual(0, testInt.Get("test4"));
        }

        /// <summary>
        /// Test for get
        /// </summary>
        [TestMethod()]
        public void GetTest()
        {
            Dictionary<string, string> testStr = new Dictionary<string, string>()
            {
                {"test1", "test1"},
                {"test2", "test2"},
                {"test3", "test3"}
            };
            Dictionary<string, int> testInt = new Dictionary<string, int>()
            {
                {"test1", 1},
                {"test2", 2},
                {"test3", 3}
            };

            Assert.AreEqual("test1", testStr.Get("test1", "abc"));
            Assert.AreEqual("abc", testStr.Get("test4", "abc"));
            Assert.AreEqual("cba", testStr.Get("test4", "cba"));
            Assert.AreEqual(2, testInt.Get("test2", 4));
            Assert.AreEqual(4, testInt.Get("test4", 4));
            Assert.AreEqual(5, testInt.Get("test4", 5));
            Assert.AreEqual(6, testInt.Get(null, 6));
        }

        /// <summary>
        /// Test for append
        /// </summary>
        [TestMethod()]
        public void AppendTest()
        {
            Dictionary<string, string> target = new Dictionary<string, string>()
            {
                {"test1", "test1"},
                {"test2", "test2"},
                {"test3", "test3"}
            };
            Dictionary<string, string> append = new Dictionary<string, string>()
            {
                {"test4", "test4"},
                {"test5", "test5"},
                {"test2", "test6"}
            };
            Dictionary<string, string> result = new Dictionary<string, string>()
            {
                {"test1", "test1"},
                {"test2", "test6"},
                {"test3", "test3"},
                {"test4", "test4"},
                {"test5", "test5"}
            };

            target.Append(append);
            AssertExtras.DictionaryIsEqual(result, target);
        }
    }
}
