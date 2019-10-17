using NUnit.Framework;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorderTest.Result
{
    internal class RecordingResultTest
    {
        [Test]
        public void TestGivenTwoResultsWhenAddedToResultObjectThenCountEqualsTwo()
        {
            IRecordingResult result = new RecordingResultImpl(new MethodDefinitionImpl("t", "t", "t"), 110);
            Assert.AreEqual(1, result.Count, "Result count should be set to 1 on new object");

            result.AddResult(186);
            Assert.AreEqual(2, result.Count, "Result count should be two after adding a new result");
        }

        [Test]
        [TestCaseSource("NumberProviderForResultObjectTesting")]
        public void TestGivenResultsWhenAddedToResultObjectThenSumMatchesResultSum(int[] values)
        {
            HelperFunctionForRecordingResultTesting(values, values.Sum(), r => r.Sum);
        }

        [Test]
        [TestCaseSource("NumberProviderForResultObjectTesting")]
        public void TestGivenResultsWhenAddedToResultObjectThenCountMatchesResultCount(int[] values)
        {
            HelperFunctionForRecordingResultTesting(values, values.Length, r => r.Count);
        }

        [Test]
        [TestCaseSource("NumberProviderForResultObjectTesting")]
        public void TestGivenResultsWhenAddedToResultObjectThenMaxMatchesResultMax(int[] values)
        {
            double max = values.Any() ? values.Max() : 0.0;
            HelperFunctionForRecordingResultTesting(values, max, r => r.Max);
        }

        [Test]
        [TestCaseSource("NumberProviderForResultObjectTesting")]
        public void TestGivenResultsWhenAddedToResultObjectThenMinMatchesResultMin(int[] values)
        {
            double min = values.Any() ? values.Min() : 0.0;
            HelperFunctionForRecordingResultTesting(values, min, r => r.Min);
        }

        [Test]
        [TestCaseSource("NumberProviderForResultObjectTesting")]
        public void TestGivenResultsWhenAddedToResultObjectThenAvgMatchesResultAvg(int[] values)
        {
            double avg = values.Any() ? values.Average() : 0.0;
            HelperFunctionForRecordingResultTesting(values, avg, r => r.Avg);
        }

        private static IEnumerable<int[]> NumberProviderForResultObjectTesting()
        {
            return new List<int[]>()
            {
                new int[] { 1, 1 },
                new int[] { 1, 1, 1 },
                new int[] { 1 },
                new int[] { 0 },
                new int[] { },
                new int[] { 10, 20 },
                new int[] { 20, 10},
            };
        }

        private void HelperFunctionForRecordingResultTesting(int[] values, double expected, Func<IRecordingResult, double> resultProvider)
        {
            IRecordingResult result = new RecordingResultImpl(new MethodDefinitionImpl("t", "t", "t"));
            foreach (int value in values)
            {
                result.AddResult(value);
            }
            Assert.AreEqual(expected, resultProvider(result));
        }
    }
}