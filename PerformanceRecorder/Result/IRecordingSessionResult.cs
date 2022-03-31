using System;
using System.Collections.Generic;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result
{
    /// <summary>
    /// Represents the recording results from a particular "session" or period of time.
    /// </summary>
    public interface IRecordingSessionResult
    {
        /// <summary>
        /// Return the recording tree that backs this session result.
        /// </summary>
        IRecordingTree RecordingTree { get; }

        /// <summary>
        /// Flatten the recording tree to a flat collection and return it.
        /// Note that this operation merges results for the same method.
        /// </summary>
        ICollection<IRecordingResult> FlatRecordingData { get; }
    }
}