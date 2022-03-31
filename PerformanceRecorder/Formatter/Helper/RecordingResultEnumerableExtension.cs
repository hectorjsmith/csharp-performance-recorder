using System.Collections.Generic;
using System.Linq;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Helper
{
    /// <summary>
    /// General purpose extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class RecordingResultEnumerableExtension
    {
        /// <summary>
        /// Given an <see cref="IEnumerable{T}"/> or recording results, find the number of characters required to display the longest value of <see cref="IRecordingResult.Sum"/>.
        /// </summary>
        public static int FindLengthOfLongestSum(this IEnumerable<IRecordingResult> results, int numberOfDecimalPlaces)
        {
            double maxSum = results
                .Select(r => r.Sum)
                .DefaultIfEmpty(0)
                .Max();
            string strFormat = numberOfDecimalPlaces <= 0 ? "{0:0}" : "{0:0." + "0".Repeat(numberOfDecimalPlaces) + "}";
            return string.Format(strFormat, maxSum).Length;
        }
        
        /// <summary>
        /// Given an <see cref="IEnumerable{T}"/> or recording results, find the number of characters required to display the longest value of <see cref="IRecordingResult.Count"/>.
        /// </summary>
        public static int FindLengthOfLongestCount(this IEnumerable<IRecordingResult> results)
        {
            double maxCount = results
                .Select(r => r.Count)
                .DefaultIfEmpty(0)
                .Max();
            return $"{maxCount:0}".Length;
        }
        
        /// <summary>
        /// Given an <see cref="IEnumerable{T}"/> or recording results, find the number of characters required to display the longest result name.
        /// </summary>
        public static int FindLengthOfLongestResultName(this IEnumerable<IRecordingResult> results, bool includeNamespaceInString)
        {
            return results
                .Select(r => r.GenerateResultName(includeNamespaceInString).Length)
                .Max();
        }
    }
}