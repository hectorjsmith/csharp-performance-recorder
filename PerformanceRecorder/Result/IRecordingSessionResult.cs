using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result
{
    public interface IRecordingSessionResult
    {
        bool IncludeNamespaceInString { get; set; }

        int Count { get; }

        ICollection<IRecordingResult> RawData();

        string ToRawString();

        string ToFormattedString();
    }
}
