using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorder.Result.Impl
{
    internal class RecordingResultImpl : IRecordingResult
    {
        private IMethodDefinition _method;

        private string _id;

        public RecordingResultImpl(IEnumerable<IRecordingResult> recordings)
        {
            if (!recordings.Any())
            {
                throw new ArgumentException("No recordings found in IEnumerable");
            }

            foreach (IRecordingResult result in recordings)
            {
                if (_method == null)
                {
                    _method = new MethodDefinitionImpl(result.Namespace, result.ClassName, result.MethodName);
                }

                MaybeSetMax(result.Max);
                MaybeSetMin(result.Min);
                Sum += result.Sum;
                Count += result.Count;
            }
        }

        public RecordingResultImpl(IMethodDefinition method)
        {
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public string Id => _id ?? (_id = _method.ToString());

        public string Namespace => _method.Namespace;

        public string ClassName => _method.ClassName;

        public string MethodName => _method.MethodName;

        public double Sum { get; private set; }

        public double Count { get; private set; }

        public double Max { get; private set; }

        public double Min { get; private set; }

        public double Avg => Count > 0 ? Sum / Count : 0;

        public void AddResult(double result)
        {
            MaybeSetMax(result);
            MaybeSetMin(result);

            Count++;
            Sum += result;
        }

        private void MaybeSetMax(double result)
        {
            if (Count == 0 || Max < result)
            {
                Max = result;
            }
        }

        private void MaybeSetMin(double result)
        {
            if (Count == 0 || Min > result)
            {
                Min = result;
            }
        }
    }
}