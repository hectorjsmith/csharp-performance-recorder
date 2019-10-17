using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result.Impl
{
    class RecordingResultImpl : IRecordingResult
    {
        private IMethodDefinition _method;

        private string _id;
        public string Id => _id ?? (_id = _method.ToString());

        public string Namespace => _method.Namespace;

        public string ClassName => _method.ClassName;

        public string MethodName => _method.MethodName;

        public double Sum { get; private set; }

        public double Count { get; private set; }

        public double Max { get; private set; }

        public double Min { get; private set; }

        public double Avg => Count > 0 ? Sum / Count : 0;

        public RecordingResultImpl(IMethodDefinition method, long result) : this(method)
        {
            AddResult(result);
        }

        public RecordingResultImpl(IMethodDefinition method)
        { 
            _method = method ?? throw new ArgumentNullException(nameof(method));
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
