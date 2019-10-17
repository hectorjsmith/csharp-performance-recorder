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

        public double Avg => Count > 0 ? Sum / Count : 0;

        public RecordingResultImpl(string id, long result) : this(id)
        {
            AddResult(result);
        }

        public RecordingResultImpl(string id)
        { 
            Id = id ?? throw new ArgumentNullException(nameof(id));
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
