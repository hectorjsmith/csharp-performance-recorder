using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder
{
    internal interface IPerformanceRecorder
    {
        void RecordMethodDuration(IMethodDefinition methodDefinition, double duration);

        void RecordMethodDuration(IMethodDefinition methodDefinition, IMethodDefinition parent, double duration);

        void MethodStart(IMethodDefinition methodDefinition, IMethodDefinition parent);

        ICollection<IRecordingResult> GetResults();

        void Reset();
    }
}