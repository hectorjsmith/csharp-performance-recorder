using System.Collections.Generic;

namespace PerformanceRecorder.Result
{
    public interface IRecordingSessionResult
    {
        bool IncludeNamespaceInString { get; set; }

        int Count { get; }

        ICollection<IRecordingResult> RawData();

        string ToRawString();

        string ToPaddedString();
    }
}