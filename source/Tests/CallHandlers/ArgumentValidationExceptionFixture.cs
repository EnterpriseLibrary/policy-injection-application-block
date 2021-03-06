﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EnterpriseLibrary.Validation.PolicyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EnterpriseLibrary.Validation.Tests.PolicyInjection
{
    [TestClass]
    public class ArgumentValidationExceptionFixture
    {
        [TestMethod]
        public void CanGetTextualRepresentationForExceptionWithEmptyValidationResults()
        {
            var exception = new ArgumentValidationException(new ValidationResults(), "param");
            var toString = exception.ToString();

            Assert.IsNotNull(toString);
        }

        [TestMethod]
        public void CanGetTextualRepresentationForExceptionWithKeylessValidationResults()
        {
            var results = new ValidationResults();
            results.AddResult(new ValidationResult("message1", null, null, null, null));
            var exception = new ArgumentValidationException(results, "param");
            var toString = exception.ToString();

            Assert.IsNotNull(toString);
            Assert.IsTrue(toString.Contains("message1"));
        }

        [TestMethod]
        public void CanGetTextualRepresentationForExceptionWithValidationResultsWithKey()
        {
            var results = new ValidationResults();
            results.AddResult(new ValidationResult("message1", null, "the key", null, null));
            var exception = new ArgumentValidationException(results, "param");
            var toString = exception.ToString();

            Assert.IsNotNull(toString);
            Assert.IsTrue(toString.Contains("message1"));
            Assert.IsTrue(toString.Contains("the key"));
        }

        [TestMethod]
        public void CanGetTextualRepresentationForExceptionWithMultipleValidationResults()
        {
            var results = new ValidationResults();
            results.AddResult(new ValidationResult("message1", null, null, null, null));
            results.AddResult(new ValidationResult("message2", null, "the key", null, null));
            results.AddResult(new ValidationResult("message3", null, null, null, null));
            var exception = new ArgumentValidationException(results, "param");
            var toString = exception.ToString();

            Assert.IsNotNull(toString);
            Assert.IsTrue(toString.Contains("message1"));
            Assert.IsTrue(toString.Contains("message2"));
            Assert.IsTrue(toString.Contains("message3"));
        }

        [TestMethod]
        public void CanDeserializeSerializedException()
        {
            var results = new ValidationResults();
            results.AddResult(new ValidationResult("message1", null, null, null, null));
            results.AddResult(new ValidationResult("message2", null, "the key", null, null));
            results.AddResult(new ValidationResult("message3", null, null, null, null));
            var exception = new ArgumentValidationException(results, "param");

            var formatter = new BinaryFormatter();
            ArgumentValidationException deserializedException;
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, exception);
                stream.Seek(0L, SeekOrigin.Begin);
                deserializedException = (ArgumentValidationException)formatter.Deserialize(stream);
            }

            var toString = deserializedException.ToString();
            Assert.IsNotNull(toString);
            Assert.IsTrue(toString.Contains("message1"));
            Assert.IsTrue(toString.Contains("message2"));
            Assert.IsTrue(toString.Contains("message3"));
        }
    }
}
