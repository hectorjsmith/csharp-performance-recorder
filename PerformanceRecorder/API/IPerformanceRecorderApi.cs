using PerformanceRecorder.Result;
using System.Collections.Generic;

namespace PerformanceRecorder.API
{
    public interface IPerformanceRecorderApi
    {
        void EnablePerformanceRecording();

        void DisablePerformanceRecording();

        void ResetRecorder();

        ICollection<IRecordingResult> GetResults();
    }
}