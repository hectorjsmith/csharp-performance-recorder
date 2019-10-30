using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Result;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder
{
    internal interface IPerformanceRecorder
    {
        void RecordMethodDuration(IRecordingTree methodNode, double duration);

        IRecordingTree RegisterMethd(IMethodDefinition methodDefinition);

        IRecordingTree RegisterMethd(IMethodDefinition methodDefinition, IRecordingTree parent);

        IRecordingTree GetResults();

        ICollection<IRecordingResult> GetFlatResults();

        void Reset();
    }
}