using PerformanceRecorder.Result;
using System.Collections.ObjectModel;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal interface IRecordingTree : ITreeNode<IRecordingResult, IRecordingTree>
    {
    }
}