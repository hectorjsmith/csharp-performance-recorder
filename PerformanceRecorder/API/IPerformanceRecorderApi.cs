using PerformanceRecorder.Result;
using System.Collections.Generic;

namespace PerformanceRecorder.API
{
    public interface IPerformanceRecorderApi
    {
        bool IsRecordingEnabled { get; set; }

        void EnablePerformanceRecording();

        void DisablePerformanceRecording();

        void ResetRecorder();

        IRecordingSessionResult GetResults();
    }
}