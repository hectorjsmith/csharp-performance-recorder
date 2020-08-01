using NUnit.Framework;
using PerformanceRecorder.Attribute;
using PerformanceRecorder.Log;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
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
        [TearDown]
        public void TearDown()
        {
            StaticRecorderManager.ResetRecorder();
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

            ICollection<IRecordingResult> results = StaticRecorderManager.GetRecorder().GetFlatResults();
            Assert.AreEqual(1, results.Count, "Only one result was expected");

            IRecordingResult firstResult = results.First();
            Assert.Greater(firstResult.Sum, 0.0, "Sum of all executions should be greater than 0.0");
            Assert.Greater(firstResult.Avg, 0.0, "Average of all executions should be greater than 0.0");
            Assert.Less(firstResult.Avg, 1.0, "Average of all executions should less than 1.0");
        }

        [Test]
        public void TestGivenActiveRecorderWhenAddingNegativeDurationThenExceptionThrown()
        {
            IPerformanceRecorder recorder = new ActivePerformanceRecorderImpl();
            IMethodDefinition method = new MethodDefinitionImpl("n", "c", "m");
            IRecordingTree node = recorder.RegisterMethd(method);

            Assert.DoesNotThrow(
                () => recorder.RecordMethodDuration(node, 0.0),
                "No exception should be thrown when adding zero duration");

            Assert.Throws<ArgumentException>(
                () => recorder.RecordMethodDuration(node, -1.0),
                "Exception should be thrown when attempting to add negative duration");
        }

        [Test]
        public void TestGivenActiveRecorderWhenCallingNestedFunctionsThenOuterFunctionsAlwaysTakeLongerThanInner()
        {
            HelperFunctionToRunTimedTest(() => HelperFunctionNestedA());
            ICollection<IRecordingResult> results = StaticRecorderManager.GetRecorder().GetFlatResults();
            Assert.AreEqual(2, results.Count, "Tow results were expected");

            IRecordingResult outerFunctionResult = results.Where(r => r.MethodName.Contains("A")).First();
            IRecordingResult innerFunctionResult = results.Where(r => r.MethodName.Contains("B")).First();

            Assert.Greater(outerFunctionResult.Sum, innerFunctionResult.Sum,
                "Outer function should take longer to run that inner functions");
        }

        [Test]
        public void TestGivenActiveRecorderWhenInstrumentedMethodThrowsExceptionThenMethodTimeStillRecorded()
        {
            int sleepBefore = 10;

            Assert.Throws<ArgumentException>(() => HelperFunctionToThrowException(sleepBefore),
                "GIVEN: Helper method does not throw an exception");

            ICollection<IRecordingResult> results = StaticRecorderManager.GetRecorder().GetFlatResults();
            Assert.AreEqual(1, results.Count, "One result was expected, even when exception thrown");

            IRecordingResult firstResult = results.First();
            Assert.AreEqual(sleepBefore, firstResult.Sum, 1,
                "Recorded execution time should include the execution time before the exception was thrown");
        }

        [Test]
        public void TestGivenActiveRecorderWithLoggerWhenRecordingNegativeDurationThenLogMessageRecorded()
        {
            MockLoggerCountsErrors logger = new MockLoggerCountsErrors();
            StaticRecorderManager.Logger = logger;

            ActivePerformanceRecorderImpl recorder = new ActivePerformanceRecorderImpl();
            RecordingTreeImpl methodNode = new RecordingTreeImpl();
            Assert.Throws<ArgumentException>(() => recorder.RecordMethodDuration(methodNode, -1));

            Assert.AreEqual(1, logger.ErrorCount, "One error message should be recorded when adding a negative duration");
        }

        [Test]
        public void TestGivenActiveRecorderWhenLoggerIsNullThenNoNullPointerThrown()
        {
            StaticRecorderManager.Logger = null;

            ActivePerformanceRecorderImpl recorder = new ActivePerformanceRecorderImpl();
            RecordingTreeImpl methodNode = new RecordingTreeImpl();
            Assert.Throws<ArgumentException>(() => recorder.RecordMethodDuration(methodNode, -1),
                "Adding a negative value should trigger an ArgumentException and log the error."
                + " When the logger is null, this should not trigger a NullReferenceException");
        }

        [Test]
        public void Given_ActiveRecorder_When_RecordingProperties_Then_GettersAndSettersAreLabelled()
        {
            StaticRecorderManager.IsRecordingEnabled = true;

            int _ = HelperPropertyToRecord10MsOnGetAndSet;
            HelperPropertyToRecord10MsOnGetAndSet = 10;

            var results = StaticRecorderManager.GetResults().FlattenAndCombine().ToList();
            Assert.AreEqual(1, results.Count(r => r.MethodName == "get_HelperPropertyToRecord10MsOnGetAndSet"),
                "Recording results do not include the expected getter");
            Assert.AreEqual(1, results.Count(r => r.MethodName == "set_HelperPropertyToRecord10MsOnGetAndSet"),
                "Recording results do not include the expected setter");
        }

        private double HelperFunctionToRunTimedTest(Action actionToRun)
        {
            StaticRecorderManager.IsRecordingEnabled = true;
            StaticRecorderManager.ResetRecorder();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                actionToRun?.Invoke();
            }
            catch
            {
            }

            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        [PerformanceLogging]
        private int HelperPropertyToRecord10MsOnGetAndSet
        {
            get { System.Threading.Thread.Sleep(10); return 0; }
            set => System.Threading.Thread.Sleep(10);
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
        private void HelperFunctionNestedA()
        {
            for (int i = 1; i < 10; i++)
            {
                HelperFunctionNestedB();
            }
        }

        [PerformanceLogging]
        private void HelperFunctionNestedB()
        {
            System.Threading.Thread.Sleep(1);
        }

        [PerformanceLogging]
        private void HelperFunctionToThrowException(int sleepBefore)
        {
            System.Threading.Thread.Sleep(sleepBefore);
            throw new ArgumentException("Planned exception during helper function");
        }
    }

    internal class MockLoggerCountsErrors : ILogger
    {
        public int ErrorCount { get; private set; }

        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            ErrorCount++;
        }

        public void Error(string message, Exception ex)
        {
            ErrorCount++;
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }
    }
}