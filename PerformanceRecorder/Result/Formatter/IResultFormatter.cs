using PerformanceRecorder.Recorder.RecordingTree;
using System.Collections.Generic;

namespace PerformanceRecorder.Result.Formatter
{
    internal interface IResultFormatter<TOutputType>
    {
        bool IncludeNamespaceInString { get; set; }

        TOutputType FormatAs(IRecordingTree results);
    }
}