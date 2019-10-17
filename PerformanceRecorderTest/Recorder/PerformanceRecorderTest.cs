using NUnit.Framework;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorderTest.Recorder
{
    class PerformanceRecorderTest
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
            recorder.RecordExecutionTime("test", () => value *= value);

            Assert.AreNotEqual(initialValue, value, "Value should have been modified in the performance recorder");
        }
    }
}
