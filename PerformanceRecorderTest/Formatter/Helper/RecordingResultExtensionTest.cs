using System;
using System.Collections.Generic;
using NUnit.Framework;
using PerformanceRecorder.Formatter.Helper;
using PerformanceRecorder.Result.Impl;

namespace PerformanceRecorderTest.Formatter.Helper
{
    public class RecordingResultExtensionTest
    {
        private static IEnumerable<TestCaseData> RecordingResultNameWithAndWithoutNamespaceProvider()
        {
            yield return new TestCaseData("n.a", "cls", "mtd", true, "n.a.cls.mtd");
            yield return new TestCaseData("n.a", "cls", "mtd", false, "cls.mtd");
        }

        [Test]
        [TestCaseSource("RecordingResultNameWithAndWithoutNamespaceProvider")]
        public void GIVEN_RecordingResult_WHEN_NameGeneratedWithNamespaceOption_THEN_NameMatchesExpected(
            string classNamespace, string className, string methodName, bool includeNamespace, string expected)
        {
            // Assemble
            var methodDefinition = new MethodDefinitionImpl(classNamespace, className, methodName);
            var result = new RecordingResultWithDepthImpl(methodDefinition, 0);

            // Act
            string name = result.GenerateResultName(includeNamespace);
            Console.WriteLine(name);

            // Assert
            Assert.AreEqual(expected, name, "Generated result name should match expected");
        }
    }
}