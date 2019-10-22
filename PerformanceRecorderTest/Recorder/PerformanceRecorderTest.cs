using NUnit.Framework;
using PerformanceRecorder.Attribute;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PerformanceRecorderTest.Recorder
{
    internal class PerformanceRecorderTest
    {
        [Test]
        public void TestGivenActiveRecorderWhenActionExecutionTimeRecordedThenActionIsExecuted()
        {
            IPerformanceRecorder recorder = new ActivePerformanceRecorderImpl();
            HelperFunctionToEnsureActionsExectutedInRecorders(recorder);
        }

        [Test]
        public void TestGivenInactiveRecorderWhenActionExecutionTimeRecordedThenActionIsExecuted()
        {
            IPerformanceRecorder recorder = new InactivePerformanceRecorderImpl();
            HelperFunctionToEnsureActionsExectutedInRecorders(recorder);
        }

        [Test]
        public void TestGivenActiveRecorderWhenShortMethodRecordedThenNoPrecisionLost()
        {
            int runCount = 5_000;

            StaticRecorderManager.IsRecordingEnabled = true;
            for (int i = 0; i < runCount; i++)
            {
                HelperFunctionToRecordAverageTimeBetween0And1Ms(i);
            }

            ICollection<IRecordingResult> results = StaticRecorderManager.GetRecorder().GetResults();
            Assert.AreEqual(1, results.Count, "Only one result was expected");

            IRecordingResult firstResult = results.First();
            Assert.Greater(firstResult.Sum, 0.0, "Sum of all executions should be greater than 0.0");
            Assert.Greater(firstResult.Avg, 0.0, "Average of all executions should be greater than 0.0");
            Assert.Less(firstResult.Avg, 1.0, "Average of all executions should less than 1.0");
        }

        [Test]
        public void TestGivenActiveRecorderWhenManyShortMethodsRecordedThenTotalActualTimeAndTotalRecordedTimeWithin5PercentDelta()
        {
            int runCount = 5_000;
            double actualExecutionTime = HelperFunctionToRunTimedTest(() =>
            {
                for (int i = 0; i < runCount; i++)
                {
                    HelperFunctionToRecordAverageTimeBetween0And1Ms(i);
                }
            });

            ICollection<IRecordingResult> results = StaticRecorderManager.GetRecorder().GetResults();
            Assert.AreEqual(1, results.Count, "Only one result was expected");

            IRecordingResult firstResult = results.First();
            double percentOfActual = actualExecutionTime * 0.05;
            Assert.AreEqual(actualExecutionTime, firstResult.Sum, percentOfActual,
                "Recorded execution time should be within 5% of actual execution time");
        }

        [Test]
        public void TestGivenActiveRecorderWhenSingleLongMethodRecordedThenTotalActualTimeAndTotalRecordedTimeWithin1PercentDelta()
        {
            double actualExecutionTime = HelperFunctionToRunTimedTest(() => HelperFunctionToRecordTotalTimeOf1Second());

            ICollection<IRecordingResult> results = StaticRecorderManager.GetRecorder().GetResults();
            Assert.AreEqual(1, results.Count, "Only one result was expected");

            IRecordingResult firstResult = results.First();
            double tenPercentOfActual = actualExecutionTime * 0.01;
            Assert.AreEqual(actualExecutionTime, firstResult.Sum, tenPercentOfActual,
                "Recorded execution time should be within 1% of actual execution time");
        }

        private void HelperFunctionToEnsureActionsExectutedInRecorders(IPerformanceRecorder recorder)
        {
            int initialValue = 10;
            int value = initialValue;
            recorder.RecordExecutionTime(new MethodDefinitionImpl("t", "t", "t"), () => value *= value);

            Assert.AreNotEqual(initialValue, value, "Value should have been modified in the performance recorder");
        }

        private double HelperFunctionToRunTimedTest(Action actionToRun)
        {
            StaticRecorderManager.IsRecordingEnabled = true;
            StaticRecorderManager.ResetRecorder();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            actionToRun?.Invoke();

            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        [PerformanceLogging]
        private void HelperFunctionToRecordAverageTimeBetween0And1Ms(int i)
        {
            if (i % 10 == 0)
            {
                // One in ten times, wait 1ms
                // This is to ensure this method takes on average, between 0 and 1ms
                System.Threading.Thread.Sleep(1);
            }
        }

        [PerformanceLogging]
        private void HelperFunctionToRecordTotalTimeOf1Second()
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}