using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Log.Impl
{
    class InactiveLoggerImpl : ILogger
    {
        public void Debug(string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Warn(string message)
        {
        }

        public void Error(string message)
        {
        }

        public void Error(string message, Exception ex)
        {
        }
    }
}
