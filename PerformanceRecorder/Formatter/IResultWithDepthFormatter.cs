using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    /// <summary>
    /// More specific interface of <see cref="IResultFormatter{TOutputType, TRecordingType}"/> that handles recoring depth.
    /// </summary>
    public interface IResultWithDepthFormatter<out TOutputType, out TRecordingType>
        : IResultFormatter<TOutputType, TRecordingType>
        where TRecordingType : IRecordingResultWithDepth
    {
    }
}
