using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PerformanceRecorder.Recorder.Impl
{
    class PerformanceRecorderImpl : IPerformanceRecorder
    {
        private Dictionary<string, long> _recordedTimes;

        public void RecordExecutionTime(string actionName, Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();

            action.Invoke();

            sw.Stop();
            long duration = sw.ElapsedMilliseconds;

            if (_recordedTimes.ContainsKey(actionName))
            {
                _recordedTimes[actionName] += duration;
            }
            else
            {
                _recordedTimes[actionName] = duration;
            }
        }
    }
}
