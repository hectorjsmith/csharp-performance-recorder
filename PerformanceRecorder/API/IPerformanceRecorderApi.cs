using PerformanceRecorder.Log;
using PerformanceRecorder.Result;
using System;

namespace PerformanceRecorder.API
{
    public interface IPerformanceRecorderApi
    {
        bool IsRecordingEnabled { get; set; }

        void SetLogger(ILogger logger);

        void EnablePerformanceRecording();

        void DisablePerformanceRecording();

        void ResetRecorder();

        IRecordingSessionResult GetResults();

        void RecordAction(string actionName, Action actionToRecord);

        IFormatterFactoryApi NewFormatterFactoryApi();
    }
}