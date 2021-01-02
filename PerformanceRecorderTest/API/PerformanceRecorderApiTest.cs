using NUnit.Framework;
using PerformanceRecorder.API;
using PerformanceRecorder.API.Impl;
using PerformanceRecorder.Attribute;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Linq;
using PerformanceRecorder.Formatter;

namespace PerformanceRecorderTest.API
{
    internal class PerformanceRecorderApiTest
    {
        [TearDown]
        public void TearDown()
        {
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.ResetRecorder();
        }

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
            Assert.AreEqual(recorderCount, api.GetResults().FlatRecordingData.Count,
                "Expecing correct number of results reported by the API");

            api.DisablePerformanceRecording();
            Assert.AreEqual(recorderCount, api.GetResults().FlatRecordingData.Count,
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

        [Test]
        public void TestGivenApiInstanceWhenRecordingActionWithNullArgumentsThenExceptionThrown()
        {
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.EnablePerformanceRecording();
            Assert.Throws<ArgumentNullException>(() => api.RecordAction(null, () => { }),
                "Recording an action with null name should throw exception");
            Assert.Throws<ArgumentNullException>(() => api.RecordAction("", () => { }),
                "Recording an action with empty name should throw exception");
            Assert.Throws<ArgumentNullException>(() => api.RecordAction("test", null),
                "Recording an action with null action should throw exception");
        }

        [Test]
        public void TestGivenApiInstanceWhenRecordingActionThenActionAppearsInResults()
        {
            string testMethodName = "testMethod";
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.EnablePerformanceRecording();
            api.RecordAction(testMethodName, () => { });

            bool resultFound = api.GetResults().FlatRecordingData.Any(r => r.MethodName == testMethodName);
            Assert.True(resultFound, "Recorded action should appear in result data");
        }

        [Test]
        public void TestGivenApiInstanceWhenRecordingActionThenActionAppearsInResultsUnderCallingMethod()
        {
            string testMethodName = "testMethod";
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
            api.EnablePerformanceRecording();
            HelperMethodToWrapRecordAction(api, testMethodName);
            
            bool methodFoundInResult = api
                .GetResults()
                .RecordingTree
                .Children()
                .Any(r => r.Value.MethodName == testMethodName);
            
            Assert.False(methodFoundInResult,
                "Recorded action should not have depth 0, it should be nested under another method");
        }

        [PerformanceLogging]
        private void HelperMethodToWrapRecordAction(IPerformanceRecorderApi api, string testMethodName)
        {
            api.RecordAction(testMethodName, () => { });
        }

        private void RecordDummyData(IPerformanceRecorder recorder)
        {
            int sleepTime = 10;
            int testCount = 10;
            for (int i = 0; i < testCount; i++)
            {
                var method = NewMethodDefinition(i);
                IRecordingTree node = recorder.RegisterMethd(method);
                recorder.RecordMethodDuration(node, sleepTime);
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