using JL.Web.Forms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System.Web;
using JL.Web.Forms;

namespace JL.Web.Forms.Tests
{
    /// <summary>
    ///This is a test class for FileInputTest and is intended
    ///to contain all FileInputTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileInputTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for NeedsMultipartForm
        ///</summary>
        [TestMethod()]
        public void NeedsMultipartFormTest()
        {
            FileInput target = new FileInput(); // TODO: Initialize to an appropriate value
            Assert.IsTrue(target.NeedsMultipartForm);
        }

        /// <summary>
        ///A test for Render
        ///</summary>
        [TestMethod()]
        public void RenderTest()
        {
            FileInput target = new FileInput();
            Assert.AreEqual("<input type=\"file\" name=\"test\" />", target.Render("test", null));
            Assert.AreEqual("<input type=\"file\" name=\"test\" />", target.Render("test", "test1"));
        }

        /// <summary>
        ///A test for GetValueFromDataCollection
        ///</summary>
        [TestMethod()]
        public void GetValueFromDataCollectionTest()
        {
            var file = new Moq.Mock<HttpPostedFileBase>();
            var files = new Moq.Mock<HttpFileCollectionBase>();
            files.Setup(foo => foo.Get("test")).Returns(file.Object);
            NameValueCollection data = null;

            FileInput target = new FileInput();

            Assert.AreSame(file.Object, target.GetValueFromDataCollection(data, files.Object, "test"));
        }

        /// <summary>
        ///A test for FileInput Constructor
        ///</summary>
        [TestMethod()]
        public void FileInputConstructorTest()
        {
            FileInput target = new FileInput();
            // Pass
        }
    }
}
