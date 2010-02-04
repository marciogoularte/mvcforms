using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcForms.Widgets;
using System.Collections.Generic;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for ChoiceHelpersTest and is intended
    ///to contain all ChoiceHelpersTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChoiceHelperTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Range
        ///</summary>
        [TestMethod()]
        public void RangeTest1()
        {
            List<Choice> expected = new List<Choice>();
            for (int i = 1; i <= 9; i++)
            {
                expected.Add(new Choice(i));
            }

            int index = 0;
            foreach (var c in ChoiceHelper.Range(1, 9))
            {
                Assert.AreEqual(expected[index++].Value, c.Value);
            }
        }

        /// <summary>
        ///A test for Range
        ///</summary>
        [TestMethod()]
        public void RangeTest()
        {
            List<Choice> expected = new List<Choice>();
            for (int i = 1; i <= 9; i += 2)
            {
                expected.Add(new Choice(i));
            }

            int index = 0;
            foreach (var c in ChoiceHelper.Range(1, 9, 2))
            {
                Assert.AreEqual(expected[index++].Value, c.Value);
            }
        }
    }
}
