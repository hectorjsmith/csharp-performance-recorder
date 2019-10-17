using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Recorder
{
    public interface IPerformanceRecorderFactory
    {
        IPerformanceRecorder New();
    }
}
