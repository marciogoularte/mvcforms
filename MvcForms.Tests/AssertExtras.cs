namespace MvcForms.Tests
{
    using System;
    using System.Collections;

    using Microsoft.VisualStudio.TestTools.UnitTesting;


    /// <summary>
    /// Additional Assets for MSTest
    /// </summary>
    public static class AssertExtras
    {
        #region Enumerable Asserts

        /// <summary>
        /// Extension method for testing IEnumerable items.
        /// </summary>
        /// <param name="source">Enumerable source.</param>
        /// <returns>Yields values from source.</returns>
        public static IEnumerable AsWeakEnumerable(this IEnumerable source)
        {
            foreach (object o in source)
            {
                yield return o;
            }
        }

        /// <summary>
        /// Assert that the expected and actual lists are the same.
        /// </summary>
        /// <param name="expected">Expected list.</param>
        /// <param name="actual">Actual list.</param>
        public static void AreEqual(IEnumerable expected, IEnumerable actual)
        {
            AreEqual(expected, actual, Comparer.Default);
        }

        /// <summary>
        /// Assert that the expected and actual lists are the same.
        /// </summary>
        /// <param name="expected">Expected list.</param>
        /// <param name="actual">Actual list.</param>
        /// <param name="comparer">Comparer to use for comparisons.</param>
        public static void AreEqual(IEnumerable expected, IEnumerable actual, Comparer comparer)
        {
            // Check if both are null
            if (expected == null && actual == null) return;

            // Step through each item and compare
            var expectedEnumerator = expected.GetEnumerator();
            var actualEnumerator = actual.GetEnumerator();

            while (expectedEnumerator.MoveNext())
            {
                if (!actualEnumerator.MoveNext())
                {
                    throw new AssertFailedException("Enumerations are different lengths..");
                }

                int result = comparer.Compare(
                    expectedEnumerator.Current,
                    actualEnumerator.Current
                );

                if (result != 0)
                {
                    throw new AssertFailedException(string.Format(
                        "Enumerations do not match. Expected: <{0}>; Actual: <{1}>",
                        expectedEnumerator.Current, actualEnumerator.Current));
                }
            }
        }

        #endregion

        #region List Asserts

        /// <summary>
        /// Assert that the expected and actual lists are the same.
        /// </summary>
        /// <param name="expected">Expected list.</param>
        /// <param name="actual">Actual list.</param>
        public static void ListIsEqual(IList expected, IList actual)
        {
            ListIsEqual(expected, actual, Comparer.Default);
        }

        /// <summary>
        /// Assert that the expected and actual lists are the same.
        /// </summary>
        /// <param name="expected">Expected list.</param>
        /// <param name="actual">Actual list.</param>
        /// <param name="comparer">Comparer to use for comparisons.</param>
        public static void ListIsEqual(IList expected, IList actual, Comparer comparer)
        {
            // Check if both are null
            if (expected == null && actual == null) return;

            // Check if both are same length
            if (expected.Count != actual.Count) throw new AssertFailedException("List count differs.");

            // Step through each item and compare
            for (int idx = 0; idx < expected.Count; idx++)
            {
                int result = comparer.Compare(expected[idx], actual[idx]);
                if (result != 0)
                {
                    throw new AssertFailedException(string.Format(
                        "Item at {0} in lists does not match. Expected: <{1}>; Actual: <{2}>",
                        idx, expected[idx], actual[idx]));
                }
            }
        }

        #endregion

        #region Dictionary Asserts

        /// <summary>
        /// Assert that the expected and actual dictionaries are the same.
        /// </summary>
        /// <param name="expected">Expected dictionary.</param>
        /// <param name="actual">Actual dictionary.</param>
        public static void DictionaryIsEqual(IDictionary expected, IDictionary actual)
        {
            DictionaryIsEqual(expected, actual, Comparer.Default);
        }

        /// <summary>
        /// Assert that the expected and actual dictionaries are the same.
        /// </summary>
        /// <param name="expected">Expected dictionary.</param>
        /// <param name="actual">Actual dictionary.</param>
        /// <param name="comparer">Comparer to use for comparisons.</param>
        public static void DictionaryIsEqual(IDictionary expected, IDictionary actual, Comparer comparer)
        {
            // Check if both are null
            if (expected == null && actual == null) return;

            // Check if both are same length
            if (expected.Count != actual.Count) throw new AssertFailedException("Dictionary count differs.");

            // Step through each item and compare
            foreach (object key in expected.Keys)
            {
                if (!actual.Contains(key))
                {
                    throw new AssertFailedException(string.Format(
                        "Key {0} was not found in actual result.", key));
                }

                int result = comparer.Compare(expected[key], actual[key]);
                if (result != 0)
                {
                    throw new AssertFailedException(string.Format(
                        "Item with key \"{0}\" does not match. Expected: <{1}>; Actual: <{2}>",
                        key, expected[key], actual[key]));
                }
            }
        }

        #endregion

        #region Exception Asserts

        /// <summary>
        /// Method to call with Raises Assert.
        /// </summary>
        public delegate void CallMethod();

        /// <summary>
        /// Assert that a particular method raises the expected exception.
        /// </summary>
        /// <param name="expectedException">The exception that is expected.</param>
        /// <param name="method">Method to call (use anonymous delegate).</param>
        public static Exception Raises<TExpected>(CallMethod method)
            where TExpected : Exception
        {
            try { method(); }
            catch (TExpected ex) { return ex; }

            // Throw Assert exception if the expected exception was not thrown
            throw new AssertFailedException(string.Format(
                "Expected exception <{0}> was not thrown", typeof(TExpected).ToString()));
        }

        /// <summary>
        /// Assert that an exception throws a particular message.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="message">Expected message.</param>
        /// <example>
        /// AssertEx.Throws&lt;ParamException&gt;(delegate { myExample.Test() }).ExpectMessage("No valid call");
        /// </example>
        public static void WithMessage(this Exception exception, string message)
        {
            Assert.AreEqual(message, exception.Message);
        }

        #endregion
    }
}
