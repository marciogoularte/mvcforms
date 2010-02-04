using MvcForms.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for HumaniseHelperTest and is intended
    ///to contain all HumaniseHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HumaniseHelperTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for FileSize
        ///</summary>
        [TestMethod()]
        public void FileSizeTest()
        {
            Assert.AreEqual("0 bytes", HumaniseHelper.FileSize(null));
            Assert.AreEqual("0 bytes", HumaniseHelper.FileSize(0));
            Assert.AreEqual("1 KB", HumaniseHelper.FileSize(1024));
            Assert.AreEqual("1 MB", HumaniseHelper.FileSize(1048576));
            Assert.AreEqual("1 GB", HumaniseHelper.FileSize(1073741824));
            Assert.AreEqual("1 TB", HumaniseHelper.FileSize(1099511627776));
            Assert.AreEqual("1024 TB", HumaniseHelper.FileSize(1125899906842624));
        }
    }
}
