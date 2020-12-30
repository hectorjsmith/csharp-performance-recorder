using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Helper
{
    public static class RecordingResultExtension
    {
        public static string GenerateResultName(this IRecordingResult result, bool includeNamespaceInString)
        {
            return includeNamespaceInString 
                ? $"{result.Namespace}.{result.ClassName}.{result.MethodName}"
                : $"{result.ClassName}.{result.MethodName}";
        }
    }
}