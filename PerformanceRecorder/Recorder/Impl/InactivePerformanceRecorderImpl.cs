using PerformanceRecorder.Result;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.Impl
{
    internal class InactivePerformanceRecorderImpl : IPerformanceRecorder
    {
        public ICollection<IRecordingResult> GetResults()
        {
            return new List<IRecordingResult>();
        }

        public void MethodStart(IMethodDefinition methodDefinition, IMethodDefinition parent)
        {
        }

        public void RecordMethodDuration(IMethodDefinition methodDefinition, double duration)
        {
        }

        public void RecordMethodDuration(IMethodDefinition methodDefinition, IMethodDefinition parent, double duration)
        {
        }

        public void Reset()
        {
        }
    }
}