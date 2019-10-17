using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Recorder
{
    public interface IPerformanceRecorder
    {
        void RecordExecutionTime(string actionName, Action action);

        ICollection<IRecordingResult> GetResults();

        void Reset();
    }
}
