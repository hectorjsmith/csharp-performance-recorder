using PerformanceRecorder.Log;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;

namespace PerformanceRecorder.API.Impl
{
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
    }
}