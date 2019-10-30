using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result.Formatter
{
    internal interface IResultFormatter<TOutputType>
    {
        bool IncludeNamespaceInString { get; set; }

        TOutputType FormatAs(IRecordingTree results);
    }
}