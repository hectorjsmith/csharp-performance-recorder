namespace PerformanceRecorder.Result
{
    internal interface IMethodDefinition
    {
        string Namespace { get; }

        string ClassName { get; }

        string MethodName { get; }
    }
}