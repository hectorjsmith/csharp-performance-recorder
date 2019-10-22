using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.Impl
{
    internal class ActivePerformanceRecorderImpl : IPerformanceRecorder
    {
        private readonly IDictionary<string, IRecordingResult> _recordedTimes = new Dictionary<string, IRecordingResult>();

        public ICollection<IRecordingResult> GetResults()
        {
            return _recordedTimes.Values;
        }

        public void RecordMethodDuration(IMethodDefinition methodDefinition, double duration)
        {
            if (duration < 0.0)
            {
                throw new ArgumentException(string.Format("Duration cannot be negative. Trying to add {0} duration", duration));
            }
            AddResult(methodDefinition, duration);
        }

        public void Reset()
        {
            _recordedTimes.Clear();
        }

        private void AddResult(IMethodDefinition methodDefinition, double duration)
        {
            string methodId = methodDefinition.ToString();
            if (_recordedTimes.ContainsKey(methodId))
            {
                IRecordingResult result = _recordedTimes[methodId];
                result.AddResult(duration);
            }
            else
            {
                _recordedTimes[methodId] = new RecordingResultImpl(methodDefinition, duration);
            }
        }
    }
}