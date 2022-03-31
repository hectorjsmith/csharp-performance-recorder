using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    /// <summary>
    /// Implementation of the generic <see cref="IResultFormatter{T}{V}"/> interface that converts results to strings.
    /// </summary>
    public interface IStringResultFormatter : IResultFormatter<string, IRecordingResult>
    {
    }
}