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

        public void RecordMethodDuration(IRecordingTree methodNode, double duration)
        {
        }

        public IRecordingTree RegisterMethd(IMethodDefinition methodDefinition)
        {
            return new RecordingTreeImpl();
        }

        public IRecordingTree RegisterMethd(IMethodDefinition methodDefinition, IRecordingTree parent)
        {
            return new RecordingTreeImpl();
        }

        public void Reset()
        {
        }
    }
}