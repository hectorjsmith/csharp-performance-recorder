using PerformanceRecorder.Result;

namespace PerformanceRecorder.Recorder.RecordingTree
{
    internal interface IRecordingTree : ITreeNode<IRecordingResult, IRecordingTree>
    {
    }
}