namespace PerformanceRecorder.Result.Impl
{
    internal class RecordingResultWithDepthImpl : RecordingResultImpl, IRecordingResultWithDepth
    {
        public RecordingResultWithDepthImpl(IMethodDefinition method, int depth) : base(method)
        {
            Depth = depth;
        }

        public int Depth { get; }
    }
}