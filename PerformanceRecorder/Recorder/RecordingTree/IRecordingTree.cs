using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal interface IRecordingTree : ITreeNode<IRecordingResult, IRecordingTree>
    {
        IEnumerable<IRecordingResult> FlattenAndCombine();

        IRecordingTree Filter(Func<IRecordingResult, bool> filterFunction);
    }
}