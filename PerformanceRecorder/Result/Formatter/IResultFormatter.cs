using PerformanceRecorder.Recorder.RecordingTree;
using System;

namespace PerformanceRecorder.Result.Formatter
{
    internal interface IResultFormatter<TOutputType, TRecordingType> where TRecordingType : IRecordingResult
    {
        TOutputType FormatAs(IRecordingTree results);

        TOutputType FormatAs(IRecordingTree results, Func<TRecordingType, bool> filterFunction);
    }
}