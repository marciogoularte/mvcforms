using JL.Web.Forms.Widgets.Choices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using System.Collections.ObjectModel;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for AustralianStatesTest and is intended
    ///to contain all AustralianStatesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AustralianStatesTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Full
        ///</summary>
        [TestMethod()]
        public void FullTest()
        {
            Assert.AreEqual(7, AustralianStates.Full.Count);
        }

        /// <summary>
        ///A test for Abbreviated
        ///</summary>
        [TestMethod()]
        public void AbbreviatedTest()
        {
            Assert.AreEqual(7, AustralianStates.Abbreviated.Count);
        }
    }
}
