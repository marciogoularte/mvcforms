using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JL.Web.Forms.Widgets.Choices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for FilePathCollectionTest and is intended
    ///to contain all FilePathCollectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FilePathCollectionTest
    {
        string testFolderPath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\JL.Web.Forms.Tests\TestFolder\");
        
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for SearchPattern
        ///</summary>
        [TestMethod()]
        public void SearchPatternTest()
        {
            FilePathCollection target = new FilePathCollection
            {
                SearchPattern = "*.txt",
            };
            Assert.AreEqual("*.txt", target.SearchPattern);
        }

        /// <summary>
        ///A test for Recursive
        ///</summary>
        [TestMethod()]
        public void RecursiveTest()
        {
            FilePathCollection target = new FilePathCollection
            {
                Recursive = true,
            };
            Assert.AreEqual(true, target.Recursive);
        }

        /// <summary>
        ///A test for Path
        ///</summary>
        [TestMethod()]
        public void PathTest()
        {
            FilePathCollection target = new FilePathCollection
            {
                Path = testFolderPath,
            };
            Assert.AreEqual(testFolderPath, target.Path);
        }

        /// <summary>
        ///A test for System.Collections.IEnumerable.GetEnumerator
        ///</summary>
        [TestMethod()]
        [DeploymentItem("JL.Web.Forms.dll")]
        public void GetEnumeratorTest1()
        {
            IEnumerable target = new FilePathCollection();
            AssertExtras.Raises<ArgumentException>(delegate()
            {
                foreach (var choice in target) { }
            });
        }

        /// <summary>
        ///A test for GetEnumerator
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorTest()
        {
            FilePathCollection target = new FilePathCollection();
            AssertExtras.Raises<ArgumentException>(delegate()
            {
                foreach (var choice in target) { }
            });

            // Test path
            target.Path = testFolderPath;
            var expected = new List<string> { 
                testFolderPath + "item1.txt", 
                testFolderPath + "item2.txt", 
                testFolderPath + "item3.log",
                testFolderPath + "item5.png",
            };
            var actual = new List<string>();
            foreach (var choice in target) actual.Add(choice.Value.ToString());
            AssertExtras.ListIsEqual(expected, actual);

            // Test pattern
            target.SearchPattern = "*.txt";
            expected = new List<string> { 
                testFolderPath + "item1.txt", 
                testFolderPath + "item2.txt" 
            };
            actual = new List<string>();
            foreach (var choice in target) actual.Add(choice.Value.ToString());
            AssertExtras.ListIsEqual(expected, actual);

            // Test recursive
            target.Recursive = true;
            expected = new List<string> { 
                testFolderPath + "item1.txt", 
                testFolderPath + "item2.txt", 
                testFolderPath + "SubFolder\\item4.txt" 
            };
            actual = new List<string>();
            foreach (var choice in target) actual.Add(choice.Value.ToString());
            AssertExtras.ListIsEqual(expected, actual);
        }

        /// <summary>
        ///A test for FilePathCollection Constructor
        ///</summary>
        [TestMethod()]
        public void FilePathCollectionConstructorTest()
        {
            FilePathCollection target = new FilePathCollection();
            // Pass
        }
    }
}
