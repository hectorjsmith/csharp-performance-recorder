﻿using PerformanceRecorder.Result;
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

        public void RecordExecutionTime(string actionName, Action action)
        {
            Stopwatch sw = Stopwatch.StartNew();

            action.Invoke();

            sw.Stop();

            AddResult(actionName, sw.ElapsedMilliseconds);
        }

        public void Reset()
        {
            _recordedTimes.Clear();
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
