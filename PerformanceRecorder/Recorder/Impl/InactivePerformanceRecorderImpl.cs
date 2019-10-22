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

        public void RecordMethodDuration(IMethodDefinition methodDefinition, double duration)
        {
        }

        public void Reset()
        {
        }
    }
}