using MvcForms.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for ConversionHelperTest and is intended
    ///to contain all ConversionHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ConversionHelperTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Boolean
        ///</summary>
        [TestMethod()]
        public void BooleanTest()
        {
            Assert.IsFalse(ConversionHelper.Boolean(null));
            Assert.IsFalse(ConversionHelper.Boolean(false));
            Assert.IsFalse(ConversionHelper.Boolean(string.Empty));
            Assert.IsFalse(ConversionHelper.Boolean(bool.FalseString));
            Assert.IsFalse(ConversionHelper.Boolean("0"));
            Assert.IsFalse(ConversionHelper.Boolean(0));
            Assert.IsFalse(ConversionHelper.Boolean(" "));
            Assert.IsFalse(ConversionHelper.Boolean("off"));
            Assert.IsTrue(ConversionHelper.Boolean(true));
            Assert.IsTrue(ConversionHelper.Boolean(bool.TrueString));
            Assert.IsTrue(ConversionHelper.Boolean("1"));
            Assert.IsTrue(ConversionHelper.Boolean(1));
            Assert.IsTrue(ConversionHelper.Boolean("on"));
            Assert.IsTrue(ConversionHelper.Boolean("asdfakjsdfaksjdhf"));
        }

        /// <summary>
        ///A test for SplitString
        ///</summary>
        [TestMethod()]
        public void SplitStringTest()
        {
            List<object> empty = new List<object>();
            List<object> single = new List<object>() { "test1" };
            List<object> multiple = new List<object>() { "test1", "test2", "test3" };

            AssertExtras.ListIsEqual(empty, ConversionHelper.SplitString(null));
            AssertExtras.ListIsEqual(empty, ConversionHelper.SplitString(string.Empty));
            AssertExtras.ListIsEqual(single, ConversionHelper.SplitString("test1"));
            AssertExtras.ListIsEqual(multiple, ConversionHelper.SplitString("test1,test2,test3"));
        }

        /// <summary>
        ///A test for ObjectList
        ///</summary>
        [TestMethod()]
        public void ObjectListTest()
        {
            Collection<object> empty = new Collection<object>();
            string[] stringArray = new string[] { "a", "b", "c" };
            List<object> stringList = new List<object>() { "a", "b", "c" };
            List<object> single = new List<object>() { "test1" };

            Assert.IsNull(ConversionHelper.ObjectList(null));
            Assert.AreSame(empty, ConversionHelper.ObjectList(empty));
            AssertExtras.ListIsEqual(stringList, ConversionHelper.ObjectList(stringArray));
            AssertExtras.ListIsEqual(single, ConversionHelper.ObjectList("test1"));
        }

        /// <summary>
        ///A test for NullBoolean
        ///</summary>
        [TestMethod()]
        public void NullBooleanTest()
        {
            Assert.AreEqual(false, ConversionHelper.NullBoolean(false));
            Assert.AreEqual(false, ConversionHelper.NullBoolean(bool.FalseString));
            Assert.AreEqual(false, ConversionHelper.NullBoolean("0"));
            Assert.AreEqual(false, ConversionHelper.NullBoolean(0));
            Assert.AreEqual(false, ConversionHelper.NullBoolean("off"));

            Assert.AreEqual(true, ConversionHelper.NullBoolean(true));
            Assert.AreEqual(true, ConversionHelper.NullBoolean(bool.TrueString));
            Assert.AreEqual(true, ConversionHelper.NullBoolean("1"));
            Assert.AreEqual(true, ConversionHelper.NullBoolean(1));
            Assert.AreEqual(true, ConversionHelper.NullBoolean("on"));

            Assert.IsNull(ConversionHelper.NullBoolean(null));
            Assert.IsNull(ConversionHelper.NullBoolean("asdfakjsdfaksjdhf"));
            Assert.IsNull(ConversionHelper.NullBoolean(string.Empty));
            Assert.IsNull(ConversionHelper.NullBoolean(" "));
        }

        /// <summary>
        ///A test for IsEmpty
        ///</summary>
        [TestMethod()]
        public void IsEmptyTest()
        {         
            Assert.IsTrue(ConversionHelper.IsEmpty(string.Empty));
            Assert.IsTrue(ConversionHelper.IsEmpty(null));
            Assert.IsTrue(ConversionHelper.IsEmpty(" "));
            Assert.IsFalse(ConversionHelper.IsEmpty("asdf"));
            Assert.IsFalse(ConversionHelper.IsEmpty(123));
            Assert.IsFalse(ConversionHelper.IsEmpty(true));
        }
    }
}
