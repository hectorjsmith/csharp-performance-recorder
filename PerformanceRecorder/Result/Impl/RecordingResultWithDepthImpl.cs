namespace PerformanceRecorder.Result.Impl
{
    internal class RecordingResultWithDepthImpl : RecordingResultImpl, IRecordingResultWithDepth
    {
        public int Depth { get; }

        public RecordingResultWithDepthImpl(IMethodDefinition method, int depth) : base(method)
        {
            Depth = depth;
        }
    }
}