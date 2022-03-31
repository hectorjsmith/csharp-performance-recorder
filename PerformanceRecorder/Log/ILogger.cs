using System;

namespace PerformanceRecorder.Log
{
    /// <summary>
    /// General logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Log an error-level message.
        /// </summary>
        void Error(string message);

        /// <summary>
        /// Log an error-level message with an exception.
        /// </summary>
        void Error(string message, Exception ex);
    }
}