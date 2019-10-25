using NUnit.Framework;
using PerformanceRecorder.API;
using PerformanceRecorder.API.Impl;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;

namespace PerformanceRecorderTest.API
{
    internal class PerformanceRecorderApiTest
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

        [Test]
        public void TestGivenActiveRecorderWhenDummyResultsAddedThenResultsReturnedThroughApi()
        {
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.EnablePerformanceRecording();

            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            Assert.IsTrue(recorder is ActivePerformanceRecorderImpl,
                "GIVEN: Performance recorder should be active");

            RecordDummyData(recorder);

            int recorderCount = recorder.GetFlatResults().Count;
            Assert.AreEqual(recorderCount, api.GetResults().Count,
                "Expecing correct number of results reported by the API");

            api.DisablePerformanceRecording();
            Assert.AreEqual(recorderCount, api.GetResults().Count,
                "Expecing correct number of results reported by the API, even when recorder disabled");
        }

        [Test]
        public void TestGivenActiveRecorderWithDummyDataWhenRecordingResetThenAllResultsRemoved()
        {
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.EnablePerformanceRecording();

            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            Assert.IsTrue(recorder is ActivePerformanceRecorderImpl,
                "GIVEN: Performance recorder should be active");

            RecordDummyData(recorder);

            api.ResetRecorder();
            Assert.AreEqual(0, recorder.GetFlatResults().Count,
                "All results should be cleared from the recorder");

            RecordDummyData(recorder);
            api.DisablePerformanceRecording();

            api.ResetRecorder();
            Assert.AreEqual(0, recorder.GetFlatResults().Count,
                "All results should be cleared from the recorder, even when recording disabled");
        }

        private void RecordDummyData(IPerformanceRecorder recorder)
        {
            int sleepTime = 10;
            int testCount = 10;
            for (int i = 0; i < testCount; i++)
            {
                var method = NewMethodDefinition(i);
                recorder.RegisterMethd(method);
                recorder.RecordMethodDuration(method, sleepTime);
            }

            Assert.AreEqual(testCount, recorder.GetFlatResults().Count,
                "GIVEN: Expecing correct number of results added to recorder");
        }

        private IMethodDefinition NewMethodDefinition(int index)
        {
            return new MethodDefinitionImpl("test", "test", "test." + index);
        }
    }
}