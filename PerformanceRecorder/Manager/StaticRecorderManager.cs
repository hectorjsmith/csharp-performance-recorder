using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Result;
using System.Collections.Generic;

namespace PerformanceRecorder.Manager
{
    internal static class StaticRecorderManager
    {
        public static bool IsRecordingEnabled { get; set; }

        private static readonly IPerformanceRecorder _inactiveRecorder = new InactivePerformanceRecorderImpl();

        private static IPerformanceRecorder _activeRecorder = new ActivePerformanceRecorderImpl();

        public static ICollection<IRecordingResult> GetResults()
        {
            return _activeRecorder.GetResults();
        }

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

        public static void ResetRecorder()
        {
            _activeRecorder.Reset();
            _inactiveRecorder.Reset();
        }
    }
}