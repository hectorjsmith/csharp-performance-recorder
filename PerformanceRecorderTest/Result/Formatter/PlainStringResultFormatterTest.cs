using NUnit.Framework;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Formatter;
using PerformanceRecorder.Result.Formatter.Impl;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorderTest.Result.Formatter
{
    class PlainStringResultFormatterTest
    {
        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPlainStringThenOutputMatchesExpected()
        {
            ICollection<IRecordingResult> results = GenerateMockResults();
            IResultFormatter<string> formatter = new PlainStringResultFormatterImpl();

            string output = formatter.FormatAs(results);
            string expectedOutput = "t.t.t1  count: 1  sum: 100.00  avg: 100.00  max: 100.00  min: 100.00"
                + Environment.NewLine
                + "t.t.t0  count: 1  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00"
                + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        private ICollection<IRecordingResult> GenerateMockResults()
        {
            ICollection<IRecordingResult> results = new List<IRecordingResult>();
            for (int i = 0; i < 2; i++)
            {
                IRecordingResult result = new RecordingResultImpl(new MethodDefinitionImpl("t", "t", "t" + i), i * 100);
                results.Add(result);
            }

            return results;
        }
    }
}
