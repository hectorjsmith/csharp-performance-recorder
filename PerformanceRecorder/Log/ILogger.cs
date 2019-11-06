using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Log
{
    public interface ILogger
    {
        void Error(string message);

        void Error(string message, Exception ex);
    }
}
