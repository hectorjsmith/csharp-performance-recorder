using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    /// <summary>
    /// More specific interface of <see cref="IResultFormatter{T}{V}"/> that handles recoring depth.
    /// </summary>
    public interface IResultWithDepthFormatter<out TOutputType, out TRecordingType>
        : IResultFormatter<TOutputType, TRecordingType>
        where TRecordingType : IRecordingResultWithDepth
    {
    }
}
