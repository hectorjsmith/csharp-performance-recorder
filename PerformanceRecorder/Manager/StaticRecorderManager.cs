using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Manager
{
    static class StaticRecorderManager
    {
        public static bool IsRecordingEnabled { get; set; }

        private static readonly IPerformanceRecorder _activeRecorder = new ActivePerformanceRecorderImpl();

        private static readonly IPerformanceRecorder _inactiveRecorder = new InactivePerformanceRecorderImpl();

        public static IPerformanceRecorder GetRecorder()
        {
            if (IsRecordingEnabled)
            {
                return _activeRecorder;
            }
            else
            {
                return _inactiveRecorder;
            }
        }
    }
}
