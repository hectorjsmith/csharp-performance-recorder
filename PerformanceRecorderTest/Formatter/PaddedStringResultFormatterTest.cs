using System;
using System.Text;
using NUnit.Framework;
using PerformanceRecorder.API;
using PerformanceRecorder.Formatter;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;

namespace PerformanceRecorderTest.Formatter
{
    internal class PaddedStringResultFormatterTest
    {
        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPaddedStringThenOutputMatchesExpected()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPaddedStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);

            string expectedOutput
                = "nnnn.cccc.mmmm2  count:  3  sum: 1240.000  avg:  413.333  max: 1020.000  min:   20.000" + Environment.NewLine
                + "   nnn.ccc.mmm1  count:  3  sum:  620.000  avg:  206.667  max:  510.000  min:   10.000" + Environment.NewLine
                + "      nn.cc.mm0  count: 13  sum:    0.000  avg:    0.000  max:    0.000  min:    0.000" + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPaddedStringWithoutNamespaceThenOutputMatchesExpected()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IFormatterFactoryApi formatterFactory = new FormatterFactoryApiImpl();
            formatterFactory.IncludeNamespaceInString = false;
            
            IStringResultFormatter formatter = formatterFactory.NewPaddedStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);
            
            string expectedOutput
                = "cccc.mmmm2  count:  3  sum: 1240.000  avg:  413.333  max: 1020.000  min:   20.000" + Environment.NewLine
                + "  ccc.mmm1  count:  3  sum:  620.000  avg:  206.667  max:  510.000  min:   10.000" + Environment.NewLine
                + "    cc.mm0  count: 13  sum:    0.000  avg:    0.000  max:    0.000  min:    0.000" + Environment.NewLine;

            Assert.Greater(output.Length, 0, "Output string length should be greater than 0");
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenEmptyResultCollectionWhenFormattedAsPaddedStringThenBlankOutputReturned()
        {
            IRecordingTree results = new RecordingTreeImpl();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPaddedStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);

            Assert.AreEqual(0, output.Length, "Output string length should be zero");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPaddedStringWithFilterThenFilterIsRespected()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPaddedStringResultFormatter();
            string rawOutput = formatter.FormatAs(sessionResult);
            string filteredOutput = formatter.FormatAs(sessionResult, r => r.Sum > 0);
            
            string expectedOutput
                = "nnnn.cccc.mmmm2  count:  3  sum: 1240.000  avg:  413.333  max: 1020.000  min:   20.000" + Environment.NewLine
                + "   nnn.ccc.mmm1  count:  3  sum:  620.000  avg:  206.667  max:  510.000  min:   10.000" + Environment.NewLine;

            Assert.AreEqual(expectedOutput, filteredOutput, "Formatted output did not match expected format");
            Assert.AreNotEqual(rawOutput, filteredOutput, "Filtered output should not match raw output");
        }

        [Test]
        public void TestGivenResultCollectionWhenFormattedAsPaddedStringWithZeroDecimalPlacesThenResultFormattedCorrectly()
        {
            IRecordingTree results = GenerateMockResults();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            IFormatterFactoryApi formatterFactory = new FormatterFactoryApiImpl();
            formatterFactory.DecimalPlacesInResults = 0;
            
            IStringResultFormatter formatter = formatterFactory.NewPaddedStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);

            string expectedOutput
                = "nnnn.cccc.mmmm2  count:  3  sum: 1240  avg:  413  max: 1020  min:   20" + Environment.NewLine
                + "   nnn.ccc.mmm1  count:  3  sum:  620  avg:  207  max:  510  min:   10" + Environment.NewLine
                + "      nn.cc.mm0  count: 13  sum:    0  avg:    0  max:    0  min:    0" + Environment.NewLine;

            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        [Test]
        public void TestGivenBlankResultCollectionWhenFormattedAsPaddedStringThenEmptyStringReturned()
        {
            // Assemble
            string expectedOutput = "";
            IRecordingTree results = new RecordingTreeImpl();
            IRecordingSessionResult sessionResult = new RecordingSessionResultImpl(results);

            // Act
            IStringResultFormatter formatter = new FormatterFactoryApiImpl().NewPaddedStringResultFormatter();
            string output = formatter.FormatAs(sessionResult);

            // Assert
            Assert.AreEqual(expectedOutput, output, "Formatted output did not match expected format");
        }

        private IRecordingTree GenerateMockResults()
        {
            IRecordingTree results = new RecordingTreeImpl();
            for (int i = 0; i < 3; i++)
            {
                var result = new RecordingResultWithDepthImpl(
                    new MethodDefinitionImpl(RepeatString("n", i + 2), RepeatString("c", i + 2), RepeatString("m", i + 2) + i), 0);
                result.AddResult(i * 10);
                result.AddResult(i * 100);
                result.AddResult(i * 510);
                if (i == 0)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        result.AddResult(0.0);
                    }
                }
                results.AddChild(result);
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