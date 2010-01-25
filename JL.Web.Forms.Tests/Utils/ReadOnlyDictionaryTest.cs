using JL.Web.Forms.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Collections;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for ReadOnlyDictionaryTest and is intended
    ///to contain all ReadOnlyDictionaryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ReadOnlyDictionaryTest
    {
        Dictionary<string, string> testDictionary = new Dictionary<string, string>
        {
            {"Item1", "Test1"},
            {"Item2", "Test2"},
            {"Item3", "Test3"},
            {"Item4", "Test4"},
            {"Item5", "Test5"},
            {"Item6", "Test6"},
            {"Item7", "Test7"},
        };

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void IndexorTest()
        {
            var target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.AreEqual(testDictionary["Item1"], target["Item1"]);

            AssertExtras.Raises<NotSupportedException>(delegate() {
                target["sdf"] = "123";
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void IndexorTest1()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.AreEqual(testDictionary["Item1"], target["Item1"]);

            AssertExtras.Raises<NotSupportedException>(delegate()
            {
                target["sdf"] = "123";
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void ValuesTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.AreEqual(testDictionary.Values, target.Values);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void ValuesTest1()
        {
            var target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.AreEqual(testDictionary.Values, target.Values);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void KeysTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.AreEqual(testDictionary.Keys, target.Keys);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void KeysTest1()
        {
            var target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.AreEqual(testDictionary.Keys, target.Keys);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void IsReadOnlyTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.IsTrue(target.IsReadOnly);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void IsReadOnlyTest1()
        {
            ICollection<KeyValuePair<string, string>> target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.IsTrue(target.IsReadOnly);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void IsFixedSizeTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.IsTrue(target.IsFixedSize);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void SyncRootTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.AreEqual((testDictionary as IDictionary).SyncRoot, target.SyncRoot);
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void IsSynchronizedTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.AreEqual((testDictionary as IDictionary).IsSynchronized, target.IsSynchronized);
        }

        [TestMethod()]
        public void CountTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.AreEqual((testDictionary as IDictionary).Count, target.Count);
        }

        [TestMethod()]
        public void TryGetValueTest()
        {
            var target = new ReadOnlyDictionary<string, string>(testDictionary);
            string value;
            Assert.IsTrue(target.TryGetValue("Item1", out value));
            Assert.AreEqual("Test1", value);

            Assert.IsFalse(target.TryGetValue("Test1", out value));
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void RemoveTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate() {
                target.Remove("sdf");
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void RemoveTest1()
        {
            IDictionary<string, string> target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate()
            {
                target.Remove("sdf");
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void RemoveTest2()
        {
            ICollection<KeyValuePair<string, string>> target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate()
            {
                target.Remove(new KeyValuePair<string, string>("abc", "123"));
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void ClearTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate()
            {
                target.Clear();
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void ClearTest1()
        {
            ICollection<KeyValuePair<string, string>> target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate() {
                target.Clear();
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void AddTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate()
            {
                target.Add("123", "abc");
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void AddTest1()
        {
            IDictionary<string, string> target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate()
            {
                target.Add("123", "abc");
            });
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void AddTest2()
        {
            ICollection<KeyValuePair<string, string>> target = new ReadOnlyDictionary<string, string>(testDictionary);
            AssertExtras.Raises<NotSupportedException>(delegate() {
                target.Add(new KeyValuePair<string, string>("123", "abc"));
            });
        }
        

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void ContainsKeyTest()
        {
            IDictionary<string, string> target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.IsTrue(target.ContainsKey("Item1"));
            Assert.IsFalse(target.ContainsKey("Test1"));
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void ContainsTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.IsTrue(target.Contains("Item1"));
            Assert.IsFalse(target.Contains("Test1"));
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void ContainsTest1()
        {
            ICollection<KeyValuePair<string, string>> target = new ReadOnlyDictionary<string, string>(testDictionary);
            Assert.IsTrue(target.Contains(new KeyValuePair<string, string>("Item1", "Test1")));
            Assert.IsFalse(target.Contains(new KeyValuePair<string, string>("Test1", "Item1")));
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void CopyToTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            KeyValuePair<string, string>[] data = new KeyValuePair<string, string>[target.Count];
            target.CopyTo(data, 0);
            // Need to Assertion statement to handle this type of argument
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void CopyToTest1()
        {
            ICollection target = new ReadOnlyDictionary<string, string>(testDictionary);
            KeyValuePair<string, string>[] data = new KeyValuePair<string, string>[target.Count];
            target.CopyTo(data, 0);
            // Need to Assertion statement to handle this type of argument
        }

        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void CopyToTest2()
        {
            ICollection<KeyValuePair<string, string>> target = new ReadOnlyDictionary<string, string>(testDictionary);
            KeyValuePair<string, string>[] data = new KeyValuePair<string, string>[target.Count];
            target.CopyTo(data, 0);
            // Need to Assertion statement to handle this type of argument
        }

        [TestMethod()]
        public void ReadOnlyDictionaryConstructorTest()
        {
            IDictionary target = new ReadOnlyDictionary<string, string>(testDictionary);
            // Pass

            AssertExtras.Raises<ArgumentNullException>(delegate() {
                target = new ReadOnlyDictionary<string, string>(null);
            });
        }
    }
}
