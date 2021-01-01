using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    public interface IResultWithDepthFormatter<out TOutputType, out TRecordingType>
        : IResultFormatter<TOutputType, TRecordingType>
        where TRecordingType : IRecordingResultWithDepth
    {
        
    }
}