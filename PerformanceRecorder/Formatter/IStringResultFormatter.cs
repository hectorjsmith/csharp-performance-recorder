using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    /// <summary>
    /// Implementation of the generic <see cref="IResultFormatter{TOutputType, TRecordingType}"/> interface that converts results to strings.
    /// </summary>
    public interface IStringResultFormatter : IResultFormatter<string, IRecordingResult>
    {
    }
}