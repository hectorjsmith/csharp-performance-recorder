using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result
{
    public interface IRecordingSessionResult
    {
        bool IncludeNamespaceInString { get; set; }

        ICollection<IRecordingResult> RawData();

        string ToRawString();

        string ToFormattedString();
    }
}
