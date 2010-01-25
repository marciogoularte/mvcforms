using JL.Web.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for TupleTest and is intended
    ///to contain all TupleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TupleTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }
        
        /// <summary>
        ///A test for Item
        ///</summary>
        [TestMethod()]
        public void ItemTest()
        {
            Tuple target = new Tuple("123", "321", 42);
            Assert.AreEqual("123", target[0]);
            Assert.AreEqual("321", target[1]);
            Assert.AreEqual(42, target[2]);
            AssertExtras.Raises<ArgumentOutOfRangeException>(delegate { var test = target[4]; });
        }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void GetEnumeratorTest()
        {
            Tuple target = new Tuple("123", "321", 42);
            AssertExtras.AreEqual(new object[3] { "123", "321", 42 }, target.AsWeakEnumerable());
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void GetTest()
        {
            Tuple target = new Tuple("123", "321", 42);
            Assert.AreEqual("123", target.Get(0));
            Assert.AreEqual("321", target.Get(1));
            Assert.AreEqual(42, target.Get(2));
            Assert.IsNull(target.Get(4));
        }

        /// <summary>
        ///A test for Get
        ///</summary>
        [TestMethod()]
        public void Get1Test()
        {
            Tuple target = new Tuple("123", "321", 42);
            Assert.AreEqual("123", target.Get<string>(0));
            Assert.AreEqual("321", target.Get<string>(1));
            Assert.AreEqual(42, target.Get<int?>(2));
            Assert.IsNull(target.Get<int?>(4));
        }

        /// <summary>
        ///A test for Tuple Constructor
        ///</summary>
        [TestMethod()]
        public void TupleConstructorTest3()
        {
            ICollection items = new Collection<object>() { "123", "321", 42 };
            Tuple target = new Tuple(items);
            Assert.AreEqual(3, target.Length);
            Assert.AreEqual("123", target[0]);
            Assert.AreEqual("321", target[1]);
            Assert.AreEqual(42, target[2]);
        }

        /// <summary>
        ///A test for Tuple Constructor
        ///</summary>
        [TestMethod()]
        public void TupleConstructorTest2()
        {
            Tuple target = new Tuple((IEnumerable)new object[] { "123", "321", 42 });
            Assert.AreEqual(3, target.Length);
            Assert.AreEqual("123", target[0]);
            Assert.AreEqual("321", target[1]);
            Assert.AreEqual(42, target[2]);
        }

        /// <summary>
        ///A test for Tuple Constructor
        ///</summary>
        [TestMethod()]
        public void TupleConstructorTest1()
        {
            Tuple tuple = new Tuple("123", "321", 42);
            Tuple target = new Tuple(tuple);
            Assert.AreEqual(3, target.Length);
            Assert.AreEqual("123", target[0]);
            Assert.AreEqual("321", target[1]);
            Assert.AreEqual(42, target[2]);
        }

        /// <summary>
        ///A test for Tuple Constructor
        ///</summary>
        [TestMethod()]
        public void TupleConstructorTest()
        {
            Tuple target = new Tuple("123", "321", 42);
            Assert.AreEqual(3, target.Length);
            Assert.AreEqual("123", target[0]);
            Assert.AreEqual("321", target[1]);
            Assert.AreEqual(42, target[2]);
        }
    }
}
