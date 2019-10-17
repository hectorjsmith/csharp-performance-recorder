using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result.Impl
{
    class RecordingResultImpl : IRecordingResult
    {
        private string v;

        public string Id { get; private set; }

        public double Sum { get; private set; }

        public double Count { get; private set; }

        public double Max { get; private set; }

        public double Min { get; private set; }

        public double Avg => Sum / Count;

        public RecordingResultImpl(string id, long result)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            AddResult(result);
        }

        public RecordingResultImpl(string id) : this(id, 0)
        { 
        }

        public void AddResult(long result)
        {
            if (Count == 0)
            {
                Min = result;
                Max = result;
            }
            else
            {
                if (Max < result)
                    Max = result;
                if (Min > result)
                    Min = result;
            }
            Count++;
            Sum += result;
        }
    }
}
