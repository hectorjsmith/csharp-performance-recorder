using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Result
{
    public interface IRecordingSessionResult
    {
        bool IncludeNamespaceInString { get; set; }

        int DecimalPlacesInResults { get; set; }

        int Count { get; }

        ICollection<IRecordingResult> FlatData();

        string ToRawString();

        string ToRawString(Func<IRecordingResult, bool> filterFunction);

        string ToPaddedString();

        string ToPaddedString(Func<IRecordingResult, bool> filterFunction);

        string ToNestedString();

        string ToNestedString(Func<IRecordingResultWithDepth, bool> filterFunction);
    }
}