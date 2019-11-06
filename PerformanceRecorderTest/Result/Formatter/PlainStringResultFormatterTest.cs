using NUnit.Framework;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;

namespace PerformanceRecorderTest.Result.Formatter
{
    internal class PlainStringResultFormatterTest
    {
        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringThenOutputMatchesExpected()
        {
            IRecordingTree results = GenerateMockResults();
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
            IRecordingTree results = GenerateMockResults();
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
            IRecordingTree results = new RecordingTreeImpl();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string output = sessionResult.ToRawString();
            Assert.AreEqual(0, output.Length, "Output string length should be zero");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringWithFilterThenFilterIsRespected()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string rawOutput = sessionResult.ToRawString();
            string filteredOutput = sessionResult.ToRawString(r => r.Sum > 0);
            string expectedOutput
                = "n.c.m1  count: 2  sum: 110.00  avg: 55.00  max: 100.00  min: 10.00" + Environment.NewLine;

            Assert.AreEqual(expectedOutput, filteredOutput, "Formatted output did not match expected format");
            Assert.AreNotEqual(rawOutput, filteredOutput, "Filtered output should not match raw output");
        }

        private IRecordingTree GenerateMockResults()
        {
            IRecordingTree results = new RecordingTreeImpl();
            for (int i = 0; i < 2; i++)
            {
                IRecordingResult result = new RecordingResultImpl(new MethodDefinitionImpl("n", "c", "m" + i));
                result.AddResult(i * 10);
                result.AddResult(i * 100);
                results.AddChild(result);
            }

            return results;
        }
    }
}