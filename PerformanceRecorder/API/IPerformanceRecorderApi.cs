using PerformanceRecorder.Log;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.API
{
    public interface IPerformanceRecorderApi
    {
        void SetLogger(ILogger logger);

        bool IsRecordingEnabled { get; set; }

        void EnablePerformanceRecording();

        void DisablePerformanceRecording();

        void ResetRecorder();

        IRecordingSessionResult GetResults();
    }
}