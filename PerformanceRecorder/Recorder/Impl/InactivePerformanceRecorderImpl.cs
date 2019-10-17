using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.Impl
{
    internal class InactivePerformanceRecorderImpl : IPerformanceRecorder
    {
        public ICollection<IRecordingResult> GetResults()
        {
            return new List<IRecordingResult>();
        }

        public void RecordExecutionTime(IMethodDefinition methodDefinition, Action action)
        {
            action.Invoke();
        }

        public void Reset()
        {
        }
    }
}