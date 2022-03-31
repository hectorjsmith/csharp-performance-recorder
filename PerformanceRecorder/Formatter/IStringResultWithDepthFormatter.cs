using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    /// <summary>
    /// Implementation of the generic <see cref="IResultWithDepthFormatter{TOutputType, TRecordingType}"/> interface that converts results to strings.
    /// </summary>
    public interface IStringResultWithDepthFormatter : IResultWithDepthFormatter<string, IRecordingResultWithDepth>
    {
    }
}
