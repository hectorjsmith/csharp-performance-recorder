using NUnit.Framework;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorderTest.Result.Formatter
{
    internal class PaddedStringResultFormatterTest
    {
        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPaddedStringThenOutputMatchesExpected()
        {
            ICollection<IRecordingResult> results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string output = sessionResult.ToPaddedString();
            string expectedOutput
                = "nnnn.cccc.mmmm2  count:  3  sum: 1240.00  avg:  413.33  max: 1020.00  min:   20.00" + Environment.NewLine
                + "   nnn.ccc.mmm1  count:  3  sum:  620.00  avg:  206.67  max:  510.00  min:   10.00" + Environment.NewLine
                + "      nn.cc.mm0  count: 13  sum:    0.00  avg:    0.00  max:    0.00  min:    0.00" + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPaddedStringWithoutNamespaceThenOutputMatchesExpected()
        {
            ICollection<IRecordingResult> results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);
            sessionResult.IncludeNamespaceInString = false;

            string output = sessionResult.ToPaddedString();
            string expectedOutput
                = "cccc.mmmm2  count:  3  sum: 1240.00  avg:  413.33  max: 1020.00  min:   20.00" + Environment.NewLine
                + "  ccc.mmm1  count:  3  sum:  620.00  avg:  206.67  max:  510.00  min:   10.00" + Environment.NewLine
                + "    cc.mm0  count: 13  sum:    0.00  avg:    0.00  max:    0.00  min:    0.00" + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenEmptyResultCollectionWhenFormattedAsPaddedStringThenBlankOutputReturned()
        {
            ICollection<IRecordingResult> results = new List<IRecordingResult>();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            string output = sessionResult.ToPaddedString();
            Assert.AreEqual(0, output.Length, "Output string length should be zero");
        }

        private ICollection<IRecordingResult> GenerateMockResults()
        {
            ICollection<IRecordingResult> results = new List<IRecordingResult>();
            for (int i = 0; i < 3; i++)
            {
                IRecordingResult result = new RecordingResultImpl(
                    new MethodDefinitionImpl(RepeatString("n", i + 2), RepeatString("c", i + 2), RepeatString("m", i + 2) + i), i * 100);
                result.AddResult(i * 10);
                result.AddResult(i * 510);
                if (i == 0)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        result.AddResult(0.0);
                    }
                }
                results.Add(result);
            }

            return results;
        }

        private string RepeatString(string input, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(input);
            }
            return sb.ToString();
        }
    }
}