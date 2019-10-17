using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder
{
    internal interface IPerformanceRecorder
    {
        void RecordExecutionTime(IMethodDefinition methodDefinition, Action action);

        ICollection<IRecordingResult> GetResults();

        void Reset();
    }
}