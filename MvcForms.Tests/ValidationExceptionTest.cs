using System;
using MvcForms;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace MvcForms.Tests
{
    /// <summary>
    ///This is a test class for ValidationExceptionTest and is intended
    ///to contain all ValidationExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ValidationExceptionTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        ///A test for ValidationException Constructor
        ///</summary>
        [TestMethod()]
        public void ValidationExceptionConstructorTest3()
        {
            var ex = new Exception("Test1");
            var test1 = new ValidationException("Test2", ex);
        }

        /// <summary>
        ///A test for ValidationException Constructor
        ///</summary>
        [TestMethod()]
        public void ValidationExceptionConstructorTest2()
        {
            var test1 = new ValidationException();
        }

        /// <summary>
        ///A test for ValidationException Constructor
        ///</summary>
        [TestMethod()]
        public void ValidationExceptionConstructorTest1()
        {
            // Check for a null argument exception
            AssertExtras.Raises<ArgumentNullException>(delegate()
            {
                var test = new ValidationException(null as ErrorCollection);
            });

            ErrorCollection el = new ErrorCollection();
            var test2 = new ValidationException(el);
            Assert.AreSame(el, test2.Messages);
        }

        /// <summary>
        ///A test for ValidationException Constructor
        ///</summary>
        [TestMethod()]
        public void ValidationExceptionConstructorTest()
        {
            var test1 = ValidationException.Create("Test 1");
            Assert.AreEqual("Test 1", test1.Message);
            Assert.IsNotNull(test1.Messages);
            Assert.IsTrue(test1.Messages.Count == 1);
            Assert.AreEqual("Test 1", test1.Messages[0]);

            var test2 = ValidationException.Create("Test {0}", 2);
            Assert.AreEqual("Test 2", test2.Message);
            Assert.IsNotNull(test2.Messages);
            Assert.IsTrue(test2.Messages.Count == 1);
            Assert.AreEqual("Test 2", test2.Messages[0]);
        }

        /// <summary>
        ///A test for Messages
        ///</summary>
        [TestMethod()]
        public void MessagesTest()
        {
            var test1 = ValidationException.Create("Test 1");
            Assert.AreEqual(1, test1.Messages.Count);
        }
    }
}
