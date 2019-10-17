using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result
{
    public interface IRecordingResult
    {
        string Id { get; }

        double Sum { get; }

        double Count { get; }

        double Max { get; }

        double Min { get; }

        double Avg { get; }

        void AddResult(long result);
    }
}
