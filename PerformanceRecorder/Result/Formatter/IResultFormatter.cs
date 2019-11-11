using PerformanceRecorder.Recorder.RecordingTree;
using System;

namespace PerformanceRecorder.Result.Formatter
{
    internal interface IResultFormatter<TOutputType>
    {
        TOutputType FormatAs(IRecordingTree results);

        TOutputType FormatAs(IRecordingTree results, Func<IRecordingResult, bool> filterFunction);
    }
}