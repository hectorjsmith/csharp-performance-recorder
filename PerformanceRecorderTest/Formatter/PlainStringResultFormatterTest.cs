using System;
using NUnit.Framework;
using PerformanceRecorder.API;
using PerformanceRecorder.Formatter;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;

namespace PerformanceRecorderTest.Formatter
{
    internal class PlainStringResultFormatterTest
    {
        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringThenOutputMatchesExpected()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPlainStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);
            
            string expectedOutput = "n.c.m1  count: 2  sum: 110.000  avg: 55.000  max: 100.000  min: 10.000"
                + Environment.NewLine
                + "n.c.m0  count: 2  sum: 0.000  avg: 0.000  max: 0.000  min: 0.000"
                + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringWithoutNamespaceThenOutputMatchesExpected()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);
            
            IFormatterFactoryApi formatterFactory = new FormatterFactoryApiImpl();
            formatterFactory.IncludeNamespaceInString = false;
            
            IStringResultFormatter formatter = formatterFactory.NewPlainStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);
            
            string expectedOutput
                = "c.m1  count: 2  sum: 110.000  avg: 55.000  max: 100.000  min: 10.000" + Environment.NewLine
                + "c.m0  count: 2  sum: 0.000  avg: 0.000  max: 0.000  min: 0.000" + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenEmptyResultCollectionWhenFormattedAsPlainStringThenBlankOutputReturned()
        {
            IRecordingTree results = new RecordingTreeImpl();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);
            
            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPlainStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);

            Assert.AreEqual(0, output.Length, "Output string length should be zero");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringWithFilterThenFilterIsRespected()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPlainStringResultFormatter();
            string rawOutput = formatter.FormatAs(sessionResult);
            string filteredOutput = formatter.FormatAs(sessionResult, r => r.Sum > 0);
            
            string expectedOutput
                = "n.c.m1  count: 2  sum: 110.000  avg: 55.000  max: 100.000  min: 10.000" + Environment.NewLine;

            Assert.AreEqual(expectedOutput, filteredOutput, "Formatted output did not match expected format");
            Assert.AreNotEqual(rawOutput, filteredOutput, "Filtered output should not match raw output");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringWithZeroDecimalPlacesThenResultFormattedCorrectly()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IFormatterFactoryApi formatterFactory = new FormatterFactoryApiImpl();
            formatterFactory.DecimalPlacesInResults = 0;
            
            IStringResultFormatter formatter = formatterFactory.NewPlainStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);

            string expectedOutput
                = "n.c.m1  count: 2  sum: 110  avg: 55  max: 100  min: 10" + Environment.NewLine
                + "n.c.m0  count: 2  sum: 0  avg: 0  max: 0  min: 0" + Environment.NewLine;

            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenBlankResultCollectionWhenFormattedAsPlainStringThenEmptyStringReturned()
        {
            // Assemble
            string expectedOutput = "";
            IRecordingTree results = new RecordingTreeImpl();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            // Act
            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPlainStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);

            // Assert
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        private IRecordingTree GenerateMockResults()
        {
            IRecordingTree results = new RecordingTreeImpl();
            for (int i = 0; i < 2; i++)
            {
                IRecordingResultWithDepth result = new RecordingResultWithDepthImpl(new MethodDefinitionImpl("n", "c", "m" + i), 0);
                result.AddResult(i * 10);
                result.AddResult(i * 100);
                results.AddChild(result);
            }

            return results;
        }
    }
}