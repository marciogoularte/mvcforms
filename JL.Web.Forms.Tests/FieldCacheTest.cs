using JL.Web.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for DefaultFieldCacheTest and is intended
    ///to contain all DefaultFieldCacheTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FieldCacheTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Instance
        ///</summary>
        [TestMethod()]
        public void InstanceTest()
        {
            FieldCache actual;
            actual = FieldCache.Instance;
            Assert.IsNotNull(actual);
            Assert.AreSame(actual, FieldCache.Instance);
        }

        /// <summary>
        ///A test for DefaultFieldCache Constructor
        ///</summary>
        [TestMethod()]
        public void DefaultFieldCacheConstructorTest()
        {
            FieldCache target = new FieldCache();
            // No exception is a pass
        }
    }
}
