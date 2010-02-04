using MvcForms.Widgets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for MediaTest and is intended
    ///to contain all MediaTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MediaTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for Media Constructor
        ///</summary>
        [TestMethod()]
        public void MediaConstructorTest()
        {
            Media target = new Media();
            // Pass
        }

        /// <summary>
        ///A test for AddCss
        ///</summary>
        [TestMethod()]
        public void AddCssTest1()
        {
            Media_Accessor target = new Media_Accessor();
            const string cssExternalFile = "http://example.com/myfile.css";
            const string cssLocalFile = "local.css";

            target._cssDirty = false;
            target.AddCss(cssExternalFile, CssMediaTypes.None);
            Assert.AreEqual(1, target._css.Count);
            Assert.AreEqual(CssMediaTypes.None, target._css[cssExternalFile]);
            Assert.IsTrue(target._cssDirty);

            target._cssDirty = false;
            target.AddCss(cssExternalFile, CssMediaTypes.Projection);
            Assert.AreEqual(1, target._css.Count);
            Assert.AreEqual(CssMediaTypes.Screen|CssMediaTypes.Projection, target._css[cssExternalFile]);
            Assert.IsTrue(target._cssDirty);

            target._cssDirty = false;
            target.AddCss(cssLocalFile, CssMediaTypes.Handheld);
            Assert.AreEqual(2, target._css.Count);
            Assert.AreEqual(CssMediaTypes.Handheld, target._css[cssLocalFile]);
            Assert.IsTrue(target._cssDirty);

            target._cssDirty = false;
            target.AddCss(cssLocalFile, CssMediaTypes.Tty);
            Assert.AreEqual(2, target._css.Count);
            Assert.AreEqual(CssMediaTypes.Handheld|CssMediaTypes.Tty, target._css[cssLocalFile]);
            Assert.IsTrue(target._cssDirty);  
        }

        /// <summary>
        ///A test for AddCss
        ///</summary>
        [TestMethod()]
        public void AddCssTest()
        {
            Media_Accessor target = new Media_Accessor();

            target._cssDirty = false;
            target.AddCss("http://example.com/myfile.css");
            Assert.AreEqual(1, target._css.Count);
            Assert.IsTrue(target._cssDirty);

            target._cssDirty = false;
            target.AddCss("http://example.com/myfile.css");
            Assert.AreEqual(1, target._css.Count);
            Assert.IsTrue(target._cssDirty);

            target._cssDirty = false;
            target.AddCss("local.css");
            Assert.AreEqual(2, target._css.Count);
            Assert.IsTrue(target._cssDirty);  
        }

        /// <summary>
        ///A test for AddJS
        ///</summary>
        [TestMethod()]
        public void AddJSTest()
        {
            Media_Accessor target = new Media_Accessor();

            target._jsDirty = false;
            target.AddJS("http://example.com/myfile.js");
            Assert.AreEqual(1, target._js.Count);
            Assert.IsTrue(target._jsDirty);

            target._jsDirty = false;
            target.AddJS("http://example.com/myfile.js");
            Assert.AreEqual(1, target._js.Count);
            Assert.IsFalse(target._jsDirty);

            target._jsDirty = false;
            target.AddJS("local.js");
            Assert.AreEqual(2, target._js.Count);
            Assert.IsTrue(target._jsDirty);            
        }

        /// <summary>
        ///A test for Append
        ///</summary>
        [TestMethod()]
        public void AppendTest()
        {
            Media_Accessor target = new Media_Accessor();
            target.AddJS("http://example.com/myfile.js");
            target.AddJS("local.js");

            Media source = new Media();
            source.AddJS("local.js");
            source.AddJS("local2.js");
            source.AddCss("http://example.com/myfile.css");

            target.Append(source);

            Assert.AreEqual(3, target._js.Count);
            Assert.AreEqual(1, target._css.Count);
        }

        /// <summary>
        /// A test for BuildMediaType
        /// </summary>
        [TestMethod()]
        public void BuildMediaTypeTest()
        {
            Assert.AreEqual("", Media_Accessor.BuildMedia(CssMediaTypes.None));
            Assert.AreEqual(" media=\"all\"", Media_Accessor.BuildMedia(CssMediaTypes.All));
            Assert.AreEqual(" media=\"screen\"", Media_Accessor.BuildMedia(CssMediaTypes.Screen));
            Assert.AreEqual(" media=\"tty,tv,projection,handheld,print,braille,aural\"", 
                Media_Accessor.BuildMedia(
                    CssMediaTypes.Tty | CssMediaTypes.TV | CssMediaTypes.Projection |
                    CssMediaTypes.Handheld | CssMediaTypes.Print | CssMediaTypes.Braille |
                    CssMediaTypes.Aural
                )
            );
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            var target = new Media_Accessor();
            // External references used to avoid virtual path provider
            target
                .AddCss("http://example.com/test.css")
                .AddJS("http://example.com/test.js");

            Assert.AreEqual("<link href=\"http://example.com/test.css\" type=\"text/css\" rel=\"stylesheet\" />\n" + 
                "<script src=\"http://example.com/test.js\" type=\"text/javascript\"></script>",
                target.ToString());
        }

        /// <summary>
        ///A test for Css
        ///</summary>
        [TestMethod()]
        public void CssTest()
        {
            var target = new Media_Accessor();

            target._cssDirty = true;
            target._cssOutput = null;
            target.AddCss("http://example.com/test.css");
            Assert.AreEqual("<link href=\"http://example.com/test.css\" type=\"text/css\" rel=\"stylesheet\" />",
                target.Css);

            target._cssDirty = true;
            target._cssOutput = "Test";
            Assert.AreEqual("<link href=\"http://example.com/test.css\" type=\"text/css\" rel=\"stylesheet\" />",
                target.Css);

            target._cssDirty = false;
            target._cssOutput = "Test";
            Assert.AreEqual("Test",
                target.Css);

            target.AddCss("http://example.com/test2.css", CssMediaTypes.Print);
            target._cssDirty = false;
            target._cssOutput = null;
            Assert.AreEqual("<link href=\"http://example.com/test.css\" type=\"text/css\" rel=\"stylesheet\" />\n" +
                "<link href=\"http://example.com/test2.css\" type=\"text/css\" media=\"print\" rel=\"stylesheet\" />",
                target.Css);
        }

        /// <summary>
        ///A test for JS
        ///</summary>
        [TestMethod()]
        public void JSTest()
        {
            var target = new Media_Accessor();

            target._jsDirty = true;
            target._jsOutput = null;
            target.AddJS("http://example.com/test.js");
            Assert.AreEqual("<script src=\"http://example.com/test.js\" type=\"text/javascript\"></script>",
                target.JS);

            target._jsDirty = true;
            target._jsOutput = "Test";
            Assert.AreEqual("<script src=\"http://example.com/test.js\" type=\"text/javascript\"></script>",
                target.JS);

            target._jsDirty = false;
            target._jsOutput = "Test";
            Assert.AreEqual("Test",
                target.JS);

            target.AddJS("http://example.com/test2.js");
            target._jsDirty = false;
            target._jsOutput = null;
            Assert.AreEqual("<script src=\"http://example.com/test.js\" type=\"text/javascript\"></script>\n" +
                "<script src=\"http://example.com/test2.js\" type=\"text/javascript\"></script>",
                target.JS);
        }
    }
}
