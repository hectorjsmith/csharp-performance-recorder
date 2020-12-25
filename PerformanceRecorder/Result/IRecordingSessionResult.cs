using System;
using System.Collections.Generic;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result
{
    public interface IRecordingSessionResult
    {
        int Count { get; }

        IRecordingTree RecordingTree { get; }
        
        ICollection<IRecordingResult> FlatData();
    }
}