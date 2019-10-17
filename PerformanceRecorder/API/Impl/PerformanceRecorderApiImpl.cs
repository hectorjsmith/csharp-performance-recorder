using System;
using System.Collections.Generic;
using System.Text;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.API.Impl
{
    public class PerformanceRecorderApiImpl : IPerformanceRecorderApi
    {
        public void DisablePerformanceRecording()
        {
            StaticRecorderManager.IsRecordingEnabled = false;
        }

        public void EnablePerformanceRecording()
        {
            StaticRecorderManager.IsRecordingEnabled = true;
        }

        public ICollection<IRecordingResult> GetResults()
        {
            return StaticRecorderManager.GetResults();
        }

        public void ResetRecorder()
        {
            StaticRecorderManager.ResetRecorder();
        }
    }
}
