using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PerformanceRecorder.Recorder.Impl
{
    class PerformanceRecorderImpl : IPerformanceRecorder
    {
        private readonly IDictionary<string, IRecordingResult> _recordedTimes = new Dictionary<string, IRecordingResult>();

        public void RecordExecutionTime(string actionName, Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();

            action.Invoke();

            sw.Stop();

            AddResult(actionName, sw.ElapsedMilliseconds);
        }

        private void AddResult(string actionName, long duration)
        {
            if (_recordedTimes.ContainsKey(actionName))
            {
                IRecordingResult result = _recordedTimes[actionName];
                result.AddResult(duration);
            }
            else
            {
                _recordedTimes[actionName] = new RecordingResultImpl(actionName, duration);
            }
        }
    }
}
