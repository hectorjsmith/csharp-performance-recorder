using System.Text.RegularExpressions;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Helper
{
    /// <summary>
    /// General purpose extensions for <see cref="IRecordingResult"/>.
    /// </summary>
    public static class RecordingResultExtension
    {
        private static readonly Regex NamespaceRemoverRegex = new Regex("\\w+\\.(\\w+)");

        /// <summary>
        /// Calculate the name of this result.
        /// </summary>
        public static string GenerateResultName(this IRecordingResult result, bool includeNamespaceInString)
        {
            return includeNamespaceInString 
                ? $"{result.Namespace}.{result.ClassName}.{result.MethodName}"
                : $"{RemoveNamespaceFromString(result.ClassName)}.{RemoveNamespaceFromString(result.MethodName)}";
        }

        private static string RemoveNamespaceFromString(string input)
        {
            return NamespaceRemoverRegex.Replace(input, "$1");
        }
    }
}