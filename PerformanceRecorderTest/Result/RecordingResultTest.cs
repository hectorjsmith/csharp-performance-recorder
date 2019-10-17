using NUnit.Framework;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceRecorderTest.Result
{
    class RecordingResultTest
    {
        [Test]
        public void TestGivenTwoResultsWhenAddedToResultObjectThenCountEqualsTwo()
        {
            IRecordingResult result = new RecordingResultImpl("test", 110);
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

        private void HelperFunctionForRecordingResultTesting(int[] values, double expected, Func<IRecordingResult, double> resultProvider)
        {
            IRecordingResult result = new RecordingResultImpl("test");
            foreach (int value in values)
            {
                result.AddResult(value);
            }
            Assert.AreEqual(expected, resultProvider(result));
        }

        public static IEnumerable<int[]> NumberProviderForResultObjectTesting()
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
    }
}
