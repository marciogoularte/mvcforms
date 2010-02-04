using System.IO;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcForms.Fields;

namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for ImageFieldTest and is intended
    ///to contain all ImageFieldTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImageFieldTest
    {
        string testFileValid = @"..\..\..\MvcForms.Tests\TestFolder\item5.png";
        string testFileInvalid = @"..\..\..\MvcForms.Tests\TestFolder\item1.txt";

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for MaxWidth
        ///</summary>
        [TestMethod()]
        public void MaxWidthTest()
        {
            var target = new ImageField
            {
                MaxWidth = 640
            };
            Assert.AreEqual(640, target.MaxWidth);
        }

        /// <summary>
        ///A test for MaxHeight
        ///</summary>
        [TestMethod()]
        public void MaxHeightTest()
        {
            var target = new ImageField
            {
                MaxHeight = 480
            };
            Assert.AreEqual(480, target.MaxHeight);
        }

        /// <summary>
        ///A test for CleanFile
        ///</summary>
        [TestMethod()]
        public void CleanFileTest1()
        {
            using (var file = File.Open(testFileInvalid, FileMode.Open, FileAccess.Read))
            {
                var postedFile = new Mock<HttpPostedFileBase>();
                postedFile.Setup(foo => foo.FileName).Returns(Path.GetFileName(file.Name));
                postedFile.Setup(foo => foo.ContentType).Returns("text/plain");
                postedFile.Setup(foo => foo.ContentLength).Returns((int)file.Length);
                postedFile.Setup(foo => foo.InputStream).Returns(file);

                var target = new ImageField();

                // Test MaxSize property
                AssertExtras.Raises<ValidationException>(delegate()
                {
                    target.Clean(postedFile.Object);
                }).WithMessage("Upload a valid image. The file you uploaded was either not an image or a corrupted image.");
            }
        }

        /// <summary>
        ///A test for CleanFile
        ///</summary>
        [TestMethod()]
        public void CleanFileTest()
        {
            using (var file = File.Open(testFileValid, FileMode.Open, FileAccess.Read))
            {
                var postedFile = new Mock<HttpPostedFileBase>();
                postedFile.Setup(foo => foo.FileName).Returns(Path.GetFileName(file.Name));
                postedFile.Setup(foo => foo.ContentType).Returns("image/png");
                postedFile.Setup(foo => foo.ContentLength).Returns((int)file.Length);
                postedFile.Setup(foo => foo.InputStream).Returns(file);

                var target = new ImageField();

                Assert.AreSame(postedFile.Object, target.Clean(postedFile.Object));

                // Test MaxWidth
                target.MaxWidth = 600;
                AssertExtras.Raises<ValidationException>(delegate()
                {
                    target.Clean(postedFile.Object);
                }).WithMessage("Ensure this image is no more than 600px wide (it is 640px).");

                target.MaxWidth = 640;
                Assert.AreSame(postedFile.Object, target.Clean(postedFile.Object));

                // Test MaxHeight
                target.MaxHeight = 400;
                AssertExtras.Raises<ValidationException>(delegate()
                {
                    target.Clean(postedFile.Object);
                }).WithMessage("Ensure this image is no more than 400px high (it is 480px).");

                target.MaxHeight = 480;
                Assert.AreSame(postedFile.Object, target.Clean(postedFile.Object));
            }
        }

        /// <summary>
        ///A test for ImageField Constructor
        ///</summary>
        [TestMethod()]
        public void ImageFieldConstructorTest1()
        {
            ImageField target = new ImageField();
            // No error is a pass
        }

        // Test for non default widget type
        class FakeImageInput : Widgets.FileInput { }

        /// <summary>
        ///A test for ImageField Constructor
        ///</summary>
        [TestMethod()]
        public void ImageFieldConstructorTest()
        {
            ImageField target = new ImageField(new FakeImageInput());
            Assert.IsInstanceOfType(target.Widget, typeof(FakeImageInput));
        }
    }
}
