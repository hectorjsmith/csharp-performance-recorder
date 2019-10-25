using PerformanceRecorder.Result;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder
{
    internal interface IPerformanceRecorder
    {
        void RecordMethodDuration(IMethodDefinition methodDefinition, double duration);

        void RegisterMethd(IMethodDefinition methodDefinition);

        void RegisterMethd(IMethodDefinition methodDefinition, IMethodDefinition parent);

        ICollection<IRecordingResult> GetResults();

        void Reset();
    }
}