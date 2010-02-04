using System;
using System.IO;
using MvcForms.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for FileFieldTest and is intended
    ///to contain all FileFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FileFieldTest
    {
        string testFile = @"..\..\..\MvcForms.Tests\TestFolder\item1.txt";

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for MaxLength
        ///</summary>
        [TestMethod()]
        public void MaxLengthTest()
        {
            var target = new FileField {
                MaxLength = 5
            };
            Assert.AreEqual(5, target.MaxLength);
        }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest1()
        {
            var fileEmpty = new Mock<HttpPostedFileBase>();
            fileEmpty.Setup(foo => foo.FileName).Returns(string.Empty);
            fileEmpty.Setup(foo => foo.ContentType).Returns(string.Empty);
            fileEmpty.Setup(foo => foo.ContentLength).Returns(0);
            fileEmpty.Setup(foo => foo.InputStream).Returns((Stream)null);

            FileField target = new FileField();

            // Test required empty file
            AssertExtras.Raises<ValidationException>(delegate() {
                target.Clean(fileEmpty.Object);
            }).WithMessage("This field is required.");

            // Test non required empty file
            target.Required = false;
            AssertExtras.Raises<ValidationException>(delegate() {
                target.Clean(fileEmpty.Object);
            }).WithMessage("The submitted file is empty.");
        }

        /// <summary>
        ///A test for Clean
        ///</summary>
        [TestMethod()]
        public void CleanTest()
        {
            using (var file = File.Open(testFile, FileMode.Open, FileAccess.Read))
            {
                var fileOK = new Mock<HttpPostedFileBase>();
                fileOK.Setup(foo => foo.FileName).Returns(Path.GetFileName(file.Name));
                fileOK.Setup(foo => foo.ContentType).Returns("text/plain");
                fileOK.Setup(foo => foo.ContentLength).Returns((int)file.Length);
                fileOK.Setup(foo => foo.InputStream).Returns(file);

                FileField target = new FileField();

                Assert.AreSame(fileOK.Object, target.Clean(fileOK.Object));

                // Test MaxSize property
                target.MaxSize = 20;
                AssertExtras.Raises<ValidationException>(delegate()
                {
                    target.Clean(fileOK.Object);
                }).WithMessage("Ensure this file is at most 20 bytes (submitted file is 37 bytes).");

                target.MaxSize = 40;
                Assert.AreSame(fileOK.Object, target.Clean(fileOK.Object));

                // Test MaxLength property
                target.MaxLength = 5;
                AssertExtras.Raises<ValidationException>(delegate()
                {
                    target.Clean(fileOK.Object);
                }).WithMessage("Ensure this file name has at most 5 characters (it has 9).");

                target.MaxLength = 10;
                Assert.AreSame(fileOK.Object, target.Clean(fileOK.Object));
            }
        }

        /// <summary>
        ///A test for FileField Constructor
        ///</summary>
        [TestMethod()]
        public void FileFieldConstructorTest1()
        {
            var target = new FileField();
            // No error is a pass
        }

        // Test for non default widget type
        class FakeFileInput : Widgets.FileInput { }

        /// <summary>
        ///A test for FileField Constructor
        ///</summary>
        [TestMethod()]
        public void FileFieldConstructorTest()
        {
            var target = new FileField(new FakeFileInput());
            Assert.IsInstanceOfType(target.Widget, typeof(FakeFileInput));
        }
    }
}
