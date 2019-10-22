using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder
{
    internal interface IPerformanceRecorder
    {
        void RecordMethodDuration(IMethodDefinition methodDefinition, double duration);

        ICollection<IRecordingResult> GetResults();

        void Reset();
    }
}