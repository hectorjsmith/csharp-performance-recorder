using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    /// <summary>
    /// Represents a tree structure of recorded data.
    /// The tree structure comes from the call hierarchy of each method included in the results.
    /// </summary>
    public interface IRecordingTree : ITreeNode<IRecordingResultWithDepth, IRecordingTree>
    {
        /// <summary>
        /// Flatten the result tree and combine all results for the same method (as defined by namespace, class, and
        /// method name). This means that if the same method is called from different places in the code, this method
        /// will combine those two results into a single result. The plain Flatten() method will include both results
        /// separately.
        /// During the combine operation the call durations and call counts are combined and the min/max values are
        /// adjusted accordingly. 
        /// </summary>
        IEnumerable<IRecordingResult> FlattenAndCombine();

        /// <summary>
        /// Return a copy of the tree after applying the given filter.
        /// The new tree will only include nodes (i.e. results) where the filter function evaluates to true.
        /// </summary>
        IRecordingTree Filter(Func<IRecordingResultWithDepth, bool> filterFunction);
    }
}