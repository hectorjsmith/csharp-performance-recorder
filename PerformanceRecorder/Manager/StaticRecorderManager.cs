using PerformanceRecorder.Log;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Manager
{
    internal static class StaticRecorderManager
    {
        private static readonly IPerformanceRecorder _inactiveRecorder = new InactivePerformanceRecorderImpl();

        private static IPerformanceRecorder _activeRecorder = new ActivePerformanceRecorderImpl();

        public static bool IsRecordingEnabled { get; set; }

        public static ILogger Logger { get; set; }

        public static IRecordingTree GetResults()
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