using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    class FormattedStringResultFormatterImpl : IResultFormatter<string>
    {
        private const string RawFormatString = "{0,_key_len_}  count: {1,_count_len_}  sum: {2,_num_len_:0.00}  avg: {3,_num_len_:0.00}  max: {4,_num_len_:0.00}  min: {5,_num_len_:0.00}";

        public string FormatAs(ICollection<IRecordingResult> results)
        {
            if (results.Count == 0)
            {
                return "";
            }

            int keyLength = FindLengthOfLongestResultName(results);
            int countLength = FindLengthOfLongestCount(results);
            int numLength = FindLengthOfLongestValue(results);

            string formatString = RawFormatString
                .Replace("_key_len_", "" + keyLength)
                .Replace("_count_len_", "" + countLength)
                .Replace("_num_len_", "" + numLength);

            StringBuilder sb = new StringBuilder();
            foreach (IRecordingResult result in results.OrderByDescending(r => r.Sum))
            {
                string resultName = string.Format("{0}.{1}.{2}", result.Namespace, result.ClassName, result.MethodName);

                sb.Append(string.Format(formatString,
                    resultName, result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        private int FindLengthOfLongestResultName(ICollection<IRecordingResult> results)
        {
            return results.Select(r => string.Format("{0}.{1}.{2}", r.Namespace, r.ClassName, r.MethodName).Length).Max();
        }

        private int FindLengthOfLongestCount(ICollection<IRecordingResult> results)
        {
            double maxSum = results.Select(r => r.Count).Max();
            return string.Format("{0:0}", maxSum).Length;
        }

        private int FindLengthOfLongestValue(ICollection<IRecordingResult> results)
        {
            double maxSum = results.Select(r => r.Sum).Max();
            return string.Format("{0:0.00}", maxSum).Length;
        }

    }
}
