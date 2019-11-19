namespace PerformanceRecorder.Result
{
    public interface IRecordingResultWithDepth : IRecordingResult
    {
        int Depth { get; }
    }
}