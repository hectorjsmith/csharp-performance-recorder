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

        public void RegisterMethd(IMethodDefinition methodDefinition)
        {
            throw new System.NotImplementedException();
        }

        public void RegisterMethd(IMethodDefinition methodDefinition, IMethodDefinition parent)
        {
        }

        public void RecordMethodDuration(IMethodDefinition methodDefinition, double duration)
        {
        }

        public void Reset()
        {
        }

    }
}