using PerformanceRecorder.Recorder.RecordingTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    internal class PaddedStringResultFormatterImpl : BaseStringResultFormatter, IResultFormatter<string>
    {
        private const string RawFormatString = "{0,_key_len_}  " + PaddedResultFormat;

        public override string FormatAs(IRecordingTree treeResults)
        {
            ICollection<IRecordingResult> results = treeResults.Flatten().ToList();
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
                sb.Append(string.Format(formatString,
                    GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}