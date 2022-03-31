using PerformanceRecorder.Log;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder.Worker;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;

namespace PerformanceRecorder.API.Impl
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    // No sense documenting this class as it will be exactly the same as the interface.
    // Ideally this class should be internal and hidden behind a builder function.
    public class PerformanceRecorderApiImpl : IPerformanceRecorderApi
    {
        public bool IsRecordingEnabled
        {
            get => StaticRecorderManager.IsRecordingEnabled;
            set => StaticRecorderManager.IsRecordingEnabled = value;
        }

        public void SetLogger(ILogger logger)
        {
            StaticRecorderManager.Logger = logger;
        }

        public void DisablePerformanceRecording()
        {
            IsRecordingEnabled = false;
        }

        public void EnablePerformanceRecording()
        {
            IsRecordingEnabled = true;
        }

        public IRecordingSessionResult GetResults()
        {
            return new RecordingSessionResultImpl(StaticRecorderManager.GetResults());
        }

        public void ResetRecorder()
        {
            StaticRecorderManager.ResetRecorder();
        }

        public void RecordAction(string actionName, Action actionToRecord)
        {
            if (string.IsNullOrWhiteSpace(actionName))
            {
                throw new ArgumentNullException("Action name must not be blank");
            }
            if (actionToRecord == null)
            {
                throw new ArgumentNullException("Action to run must not be null");
            }

            StaticRecordingWorker.RegisterMethodBeforeItRuns(actionName, actionToRecord);
            try
            {
                actionToRecord();
            }
            finally
            {
                StaticRecordingWorker.RecordMethodDurationAfterItRuns();
            }
        }

        public IFormatterFactoryApi NewFormatterFactoryApi()
        {
            return new FormatterFactoryApiImpl();
        }
    }
    #pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}