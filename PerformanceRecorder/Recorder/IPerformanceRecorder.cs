using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Recorder
{
    interface IPerformanceRecorder
    {
        void RecordExecutionTime(IMethodDefinition methodDefinition, Action action);

        ICollection<IRecordingResult> GetResults();

        void Reset();
    }
}
