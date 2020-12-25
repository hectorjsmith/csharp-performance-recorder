using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    public interface IRecordingTree : ITreeNode<IRecordingResultWithDepth, IRecordingTree>
    {
        IEnumerable<IRecordingResult> FlattenAndCombine();

        IRecordingTree Filter(Func<IRecordingResultWithDepth, bool> filterFunction);
    }
}