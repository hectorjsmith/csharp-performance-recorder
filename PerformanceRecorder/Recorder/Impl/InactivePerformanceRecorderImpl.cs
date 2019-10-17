using System;
using System.Collections.Generic;
using System.Text;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Recorder.Impl
{
    class InactivePerformanceRecorderImpl : IPerformanceRecorder
    {
        public ICollection<IRecordingResult> GetResults()
        {
            return new List<IRecordingResult>();
        }

        public void RecordExecutionTime(string actionName, Action action)
        {
            action.Invoke();
        }

        public void Reset()
        {
        }
    }
}
