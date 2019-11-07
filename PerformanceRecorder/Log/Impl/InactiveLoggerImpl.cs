using System;

namespace PerformanceRecorder.Log.Impl
{
    internal class InactiveLoggerImpl : ILogger
    {
        public void Error(string message)
        {
        }

        public void Error(string message, Exception ex)
        {
        }
    }
}