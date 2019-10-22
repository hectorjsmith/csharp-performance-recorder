using NUnit.Framework;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;

namespace PerformanceRecorderTest.Result.Formatter
{
    internal class PlainStringResultFormatterTest
    {
        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringThenOutputMatchesExpected()
        {
            ICollection<IRecordingResult> results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string output = sessionResult.ToRawString();
            string expectedOutput = "n.c.m1  count: 2  sum: 110.00  avg: 55.00  max: 100.00  min: 10.00"
                + Environment.NewLine
                + "n.c.m0  count: 2  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00"
                + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringWithoutNamespaceThenOutputMatchesExpected()
        {
            ICollection<IRecordingResult> results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);
            sessionResult.IncludeNamespaceInString = false;

            string output = sessionResult.ToRawString();
            string expectedOutput
                = "c.m1  count: 2  sum: 110.00  avg: 55.00  max: 100.00  min: 10.00" + Environment.NewLine
                + "c.m0  count: 2  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00" + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenEmptyResultCollectionWhenFormattedAsPlainStringThenBlankOutputReturned()
        {
            ICollection<IRecordingResult> results = new List<IRecordingResult>();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string output = sessionResult.ToRawString();
            Assert.AreEqual(0, output.Length, "Output string length should be zero");
        }

        private ICollection<IRecordingResult> GenerateMockResults()
        {
            ICollection<IRecordingResult> results = new List<IRecordingResult>();
            for (int i = 0; i < 2; i++)
            {
                IRecordingResult result = new RecordingResultImpl(new MethodDefinitionImpl("n", "c", "m" + i), i * 100);
                result.AddResult(i * 10);
                results.Add(result);
            }

            return results;
        }
    }
}