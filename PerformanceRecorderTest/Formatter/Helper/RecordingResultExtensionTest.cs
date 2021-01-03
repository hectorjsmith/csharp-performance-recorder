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
            yield return new TestCaseData("n.a", "cls<a.b>", "mtd<a.b>", true, "n.a.cls<a.b>.mtd<a.b>");
            yield return new TestCaseData("n.a", "cls<a.b>", "mtd<a.b>", false, "cls<b>.mtd<b>");
            yield return new TestCaseData("n.a", "cls<a.b, a.c>", "mtd<a.b<a.c>>", false, "cls<b, c>.mtd<b<c>>");
            yield return new TestCaseData("n.a", "cls<a.b, a.c>", ".ctor", true, "n.a.cls<a.b, a.c>..ctor");
            yield return new TestCaseData("n.a", "cls<a.b, a.c>", ".ctor", false, "cls<b, c>..ctor");
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