using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JL.Web.Forms;
using System;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for SplitDateTimeTest and is intended
    ///to contain all SplitDateTimeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SplitDateTimeTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for TimeFormat
        ///</summary>
        [TestMethod()]
        public void TimeFormatTest()
        {
            var target = new SplitDateTime();

            target.TimeFormat = "hh:MM";
            Assert.AreEqual("hh:MM", (target.Widgets[1] as TimeInput).Format);

            (target.Widgets[1] as TimeInput).Format = "MM:hh:ss";
            Assert.AreEqual("MM:hh:ss", target.TimeFormat);
        }

        /// <summary>
        ///A test for DateFormat
        ///</summary>
        [TestMethod()]
        public void DateFormatTest()
        {
            var target = new SplitDateTime();

            target.DateFormat = "yy-mm-dd";
            Assert.AreEqual("yy-mm-dd", (target.Widgets[0] as DateInput).Format);

            (target.Widgets[0] as DateInput).Format = "mmm-dd-yyyy";
            Assert.AreEqual("mmm-dd-yyyy", target.DateFormat);
        }

        /// <summary>
        ///A test for Decompress
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void DecompressTest()
        {
            var target = new SplitDateTime_Accessor();

            var value = new DateTime?(DateTime.Now);
            var result = target.Decompress(value);
            Assert.IsInstanceOfType(result, typeof(Tuple));
            Assert.AreEqual(value.Value.Date, result[0]);
            Assert.AreEqual(value.Value.TimeOfDay, result[1]);

            result = target.Decompress(null);
            Assert.IsInstanceOfType(result, typeof(Tuple));
            Assert.IsNull(result[0]);
            Assert.IsNull(result[1]);

        }

        /// <summary>
        ///A test for SplitDateTime Constructor
        ///</summary>
        [TestMethod()]
        public void SplitDateTimeConstructorTest()
        {
            SplitDateTime target = new SplitDateTime();
            // Pass
        }
    }
}
