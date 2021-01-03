namespace PerformanceRecorder.Result
{
    /// <summary>
    /// IRecordingResult instance that also includes the method depth.
    /// </summary>
    public interface IRecordingResultWithDepth : IRecordingResult
    {
        /// <summary>
        /// Depth of the method in the invocation tree.
        /// The depth is defined as the number of recorded methods in the call stack for this method.
        ///
        /// For example, if methodA calls methodB, which calls methodC: methodA has a depth of 0, methodB has a depth of 1, and
        /// methodC has a depth of 2.
        ///
        /// Note that the depth information is lost when the result tree is flattened and combined.
        /// </summary>
        int Depth { get; }
    }
}