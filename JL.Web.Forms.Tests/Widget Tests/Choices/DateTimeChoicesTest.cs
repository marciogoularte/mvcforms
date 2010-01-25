using JL.Web.Forms.Widgets.Choices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using System.Collections.ObjectModel;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for DateTimeChoicesTest and is intended
    ///to contain all DateTimeChoicesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DateTimeChoicesTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for MonthsShort
        ///</summary>
        [TestMethod()]
        public void MonthsShortTest()
        {
            Assert.AreEqual(12, DateAndTime.MonthsShort.Count);
        }

        /// <summary>
        ///A test for MonthsLong
        ///</summary>
        [TestMethod()]
        public void MonthsLongTest()
        {
            Assert.AreEqual(12, DateAndTime.MonthsLong.Count);
        }

        /// <summary>
        ///A test for DaysShort
        ///</summary>
        [TestMethod()]
        public void DaysShortTest()
        {
            Assert.AreEqual(7, DateAndTime.DaysShort.Count);
        }

        /// <summary>
        ///A test for DaysLong
        ///</summary>
        [TestMethod()]
        public void DaysLongTest()
        {
            Assert.AreEqual(7, DateAndTime.DaysLong.Count);
        }
    }
}
