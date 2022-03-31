using PerformanceRecorder.Recorder.RecordingTree;
using System;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter
{
    /// <summary>
    /// Handles converting and formatting the result data into a new format.
    /// </summary>
    public interface IResultFormatter<out TOutputType, out TRecordingType> where TRecordingType : IRecordingResult
    {
        /// <summary>
        /// Convert an <see cref="IRecordingSessionResult"/> instance into another format.
        /// </summary>
        TOutputType FormatAs(IRecordingSessionResult results);

        /// <summary>
        /// Convert an <see cref="IRecordingSessionResult"/> instance into another format.
        /// Before the results are formatted, they are fed through the provided filter function.
        /// Only the results where the filter function returns true will be included in the output.
        /// </summary>
        TOutputType FormatAs(IRecordingSessionResult results, Func<TRecordingType, bool> filterFunction);
    }
}