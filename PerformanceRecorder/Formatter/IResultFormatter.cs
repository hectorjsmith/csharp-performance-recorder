using PerformanceRecorder.Recorder.RecordingTree;
using System;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    public interface IResultFormatter<out TOutputType, out TRecordingType> where TRecordingType : IRecordingResult
    {
        TOutputType FormatAs(IRecordingSessionResult results);

        TOutputType FormatAs(IRecordingSessionResult results, Func<TRecordingType, bool> filterFunction);
    }
}