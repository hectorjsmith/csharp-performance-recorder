using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Recorder.Impl
{
    class InactivePerformanceRecorderImpl : IPerformanceRecorder
    {
        public void RecordExecutionTime(string actionName, Action action)
        {
            action.Invoke();
        }
    }
}
