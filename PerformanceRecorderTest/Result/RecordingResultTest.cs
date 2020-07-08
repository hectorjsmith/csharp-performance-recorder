﻿using NUnit.Framework;
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
            IRecordingResult result = new RecordingResultImpl(new MethodDefinitionImpl("t", "t", "t"));
            result.AddResult(110);
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

        [Test]
        public void TestGivenResultListWhenNewResultCreatedFromListThenMethodIsSetCorrectly()
        {
            MethodDefinitionImpl method = new MethodDefinitionImpl("n", "c", "m");
            IEnumerable<IRecordingResult> recordingResults = HelperMethodToGenerateDuplicateRecordingList(method);

            RecordingResultImpl combinedRecording = new RecordingResultImpl(recordingResults);

            Assert.AreEqual(method.ToString(), combinedRecording.Id,
                "Combined recording ID should match the method data");
        }

        [Test]
        public void TestGivenResultListWhenNewResultCreatedFromListThenDataIsMergedCorrectly()
        {
            MethodDefinitionImpl method = new MethodDefinitionImpl("n", "c", "m");
            IEnumerable<IRecordingResult> recordingResults = HelperMethodToGenerateDuplicateRecordingList(method);

            RecordingResultImpl combinedRecording = new RecordingResultImpl(recordingResults);

            Assert.AreEqual(50.0, combinedRecording.Max, "Max value on combined result is not correct");
            Assert.AreEqual(5.0, combinedRecording.Min, "Min value on combined result is not correct");
            Assert.AreEqual(4, combinedRecording.Count, "Count value on combined result is not correct");
            Assert.AreEqual(21.25, combinedRecording.Avg, "Avg value on combined result is not correct");
            Assert.AreEqual(85.0, combinedRecording.Sum, "Sum value on combined result is not correct");
        }

        private static IEnumerable<int[]> NumberProviderForResultObjectTesting()
        {
            return new List<int[]>()
            {
                new [] { 1, 1 },
                new [] { 1, 1, 1 },
                new [] { 1 },
                new [] { 0 },
                new int[] { },
                new [] { 10, 20 },
                new [] { 20, 10},
            };
        }

        private IEnumerable<IRecordingResult> HelperMethodToGenerateDuplicateRecordingList(IMethodDefinition method)
        {
            RecordingResultImpl result1 = new RecordingResultImpl(method);
            RecordingResultImpl result2 = new RecordingResultImpl(method);

            result1.AddResult(10.0);
            result1.AddResult(5.0);
            result1.AddResult(20.0);

            result2.AddResult(50.0);

            return new List<IRecordingResult> { result1, result2 };
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