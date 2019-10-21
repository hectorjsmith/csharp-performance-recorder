﻿using NUnit.Framework;
using PerformanceRecorder.Attribute;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorderTest.Recorder
{
    internal class PerformanceRecorderTest
    {
        [Test]
        public void TestGivenActivePerformanceRecorderWhenActionExecutionTimeRecordedThenActionIsExecuted()
        {
            IPerformanceRecorder recorder = new ActivePerformanceRecorderImpl();
            HelperFunctionToEnsureActionsExectutedInRecorders(recorder);
        }

        [Test]
        public void TestGivenInactivePerformanceRecorderWhenActionExecutionTimeRecordedThenActionIsExecuted()
        {
            IPerformanceRecorder recorder = new InactivePerformanceRecorderImpl();
            HelperFunctionToEnsureActionsExectutedInRecorders(recorder);
        }

        private void HelperFunctionToEnsureActionsExectutedInRecorders(IPerformanceRecorder recorder)
        {
            int initialValue = 10;
            int value = initialValue;
            recorder.RecordExecutionTime(new MethodDefinitionImpl("t", "t", "t"), () => value *= value);

            Assert.AreNotEqual(initialValue, value, "Value should have been modified in the performance recorder");
        }

        [Test]
        public void TestGivenActivePerformanceRecorderWhenShortMethodRecordedThenNoPrecisionLost()
        {
            int runCount = 10_000;

            StaticRecorderManager.IsRecordingEnabled = true;
            for (int i = 0; i < runCount; i++)
            {
                HelperFunctionToRecord(i);
            }

            ICollection<IRecordingResult> results = StaticRecorderManager.GetRecorder().GetResults();
            Assert.AreEqual(1, results.Count, "Only one result was expected");

            IRecordingResult firstResult = results.First();
            Assert.AreNotEqual(0.0, firstResult.Sum, "Sum of all executions should not be 0.0");
            Assert.AreNotEqual(0.0, firstResult.Avg, "Average of all executions should not be 0.0");
        }

        [PerformanceLogging]
        private void HelperFunctionToRecord(int i)
        {
            if (i % 10 == 0)
            {
                // One in ten times, wait 1ms
                // This is to ensure this method takes on average, between 0 and 1ms
                System.Threading.Thread.Sleep(1);
            }
        }
    }
}