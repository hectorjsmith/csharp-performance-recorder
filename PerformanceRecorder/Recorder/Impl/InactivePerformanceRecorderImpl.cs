using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.Impl
{
    internal class InactivePerformanceRecorderImpl : IPerformanceRecorder
    {
        public IRecordingTree GetResults()
        {
            return new RecordingTreeImpl();
        }

        public ICollection<IRecordingResult> GetFlatResults()
        {
            return new List<IRecordingResult>();
        }

        public void RegisterMethd(IMethodDefinition methodDefinition)
        {
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