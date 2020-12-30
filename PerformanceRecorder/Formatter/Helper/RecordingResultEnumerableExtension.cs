using System.Collections.Generic;
using System.Linq;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Helper
{
    public static class RecordingResultEnumerableExtension
    {
        public static int FindLengthOfLongestSum(this IEnumerable<IRecordingResult> results, int numberOfDecimalPlaces)
        {
            double maxSum = results
                .Select(r => r.Sum)
                .DefaultIfEmpty(0)
                .Max();
            string strFormat = numberOfDecimalPlaces <= 0 ? "{0:0}" : "{0:0." + "0".Repeat(numberOfDecimalPlaces) + "}";
            return string.Format(strFormat, maxSum).Length;
        }
        
        public static int FindLengthOfLongestCount(this IEnumerable<IRecordingResult> results)
        {
            double maxCount = results
                .Select(r => r.Count)
                .DefaultIfEmpty(0)
                .Max();
            return $"{maxCount:0}".Length;
        }
        
        public static int FindLengthOfLongestResultName(this IEnumerable<IRecordingResult> results, bool includeNamespaceInString)
        {
            return results
                .Select(r => r.GenerateResultName(includeNamespaceInString).Length)
                .Max();
        }
    }
}