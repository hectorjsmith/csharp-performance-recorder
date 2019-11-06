namespace PerformanceRecorder.Result
{
    public interface IRecordingResult
    {
        int Depth { get; }

        string Id { get; }

        string Namespace { get; }

        string ClassName { get; }

        string MethodName { get; }

        double Sum { get; }

        double Count { get; }

        double Max { get; }

        double Min { get; }

        double Avg { get; }

        void AddResult(double result);
    }
}