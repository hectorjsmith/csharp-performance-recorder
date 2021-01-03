namespace PerformanceRecorder.Result
{
    internal interface IWritableRecordingResult : IRecordingResult
    {
        void AddResult(double result);
    }
}