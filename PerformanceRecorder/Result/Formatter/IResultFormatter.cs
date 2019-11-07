using PerformanceRecorder.Recorder.RecordingTree;
using System;

namespace PerformanceRecorder.Result.Formatter
{
    internal interface IResultFormatter<TOutputType>
    {
        bool IncludeNamespaceInString { get; set; }

        TOutputType FormatAs(IRecordingTree results);

        TOutputType FormatAs(IRecordingTree results, Func<IRecordingResult, bool> filterFunction);
    }
}