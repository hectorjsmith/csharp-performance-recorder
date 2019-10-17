using NUnit.Framework;
using PerformanceRecorder.API;
using PerformanceRecorder.API.Impl;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorderTest.API
{
    class PerformanceRecorderApiTest
    {
        [Test]
        public void TestGivenApiObjectWhenRecordingEnabledThenCorrectRecorderTypeUsed()
        {
            Assert.IsFalse(StaticRecorderManager.IsRecordingEnabled,
                "GIVEN: Performance recording should be off by default");
            Assert.IsTrue(StaticRecorderManager.GetRecorder() is InactivePerformanceRecorderImpl,
                "GIVEN: Default performance recorder should be inactive");

            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.EnablePerformanceRecording();
            Assert.IsTrue(StaticRecorderManager.IsRecordingEnabled,
                "Performance recording should be on");
            Assert.IsTrue(StaticRecorderManager.GetRecorder() is ActivePerformanceRecorderImpl,
                "Performance recorder should be active");
        }

        [Test]
        public void TestGivenApiObjectWhenRecordingDisabledThenCorrectRecorderTypeUsed()
        {
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.EnablePerformanceRecording();

            Assert.IsTrue(StaticRecorderManager.IsRecordingEnabled,
                "GIVEN: Performance recording should be on");
            Assert.IsTrue(StaticRecorderManager.GetRecorder() is ActivePerformanceRecorderImpl,
                "GIVEN: Performance recorder should be active");

            api.DisablePerformanceRecording();
            Assert.IsFalse(StaticRecorderManager.IsRecordingEnabled,
                "Performance recording should be off");
            Assert.IsTrue(StaticRecorderManager.GetRecorder() is InactivePerformanceRecorderImpl,
                "Performance recorder should be inactive");
        }
    }
}
