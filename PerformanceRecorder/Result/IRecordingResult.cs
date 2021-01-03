namespace PerformanceRecorder.Result
{
    /// <summary>
    /// Recording result for a given method.
    /// An instance of this interface includes the combined recording results for all the executions of a given method.
    /// </summary>
    public interface IRecordingResult
    {
        /// <summary>
        /// Method ID. Defined as a concatenation of namespace, class name, and method name.
        /// Two IRecordingResult objects with the same ID are considered to be for the same method.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Namespace of the class that contains the method.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Name of the class that contains the method.
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Name of the method.
        /// </summary>
        string MethodName { get; }

        /// <summary>
        /// Combined execution time of all method invocations.
        /// Time in milliseconds.
        /// </summary>
        double Sum { get; }

        /// <summary>
        /// Total number of times the method was executed.
        /// </summary>
        double Count { get; }

        /// <summary>
        /// Longest recorded method execution duration.
        /// Time in milliseconds.
        /// </summary>
        double Max { get; }

        /// <summary>
        /// Shortest recorded method execution duration.
        /// Time in milliseconds.
        /// </summary>
        double Min { get; }

        /// <summary>
        /// Average method execution duration.
        /// This is defined as the Sum divided by the Count.
        /// Time in milliseconds.
        /// </summary>
        double Avg { get; }
    }
}