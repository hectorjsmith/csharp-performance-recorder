﻿using NUnit.Framework;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Result.Impl;

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
    }
}