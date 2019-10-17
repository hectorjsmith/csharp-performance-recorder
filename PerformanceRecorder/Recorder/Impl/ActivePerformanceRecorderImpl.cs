using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PerformanceRecorder.Recorder.Impl
{
    class ActivePerformanceRecorderImpl : IPerformanceRecorder
    {
        private readonly IDictionary<string, IRecordingResult> _recordedTimes = new Dictionary<string, IRecordingResult>();

        public ICollection<IRecordingResult> GetResults()
        {
            return _recordedTimes.Values;
        }

        public void RecordExecutionTime(IMethodDefinition methodDefinition, Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();

            action.Invoke();

            sw.Stop();

            AddResult(methodDefinition, sw.ElapsedMilliseconds);
        }

        public void Reset()
        {
            _recordedTimes.Clear();
        }

        private void AddResult(IMethodDefinition methodDefinition, long duration)
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
