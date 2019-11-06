using PerformanceRecorder.Log;
using PerformanceRecorder.Log.Impl;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Manager
{
    internal static class StaticRecorderManager
    {
        private static readonly IPerformanceRecorder _inactiveRecorder = new InactivePerformanceRecorderImpl();

        private static readonly IPerformanceRecorder _activeRecorder = new ActivePerformanceRecorderImpl();

        public static bool IsRecordingEnabled { get; set; }

        private static readonly ILogger _inactiveLogger = new InactiveLoggerImpl();
        private static ILogger _injectedLogger;
        public static ILogger Logger
        {
            get { return _injectedLogger ?? _inactiveLogger; }
            set { _injectedLogger = value; }
        }

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