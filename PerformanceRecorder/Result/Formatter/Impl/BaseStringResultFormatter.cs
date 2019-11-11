using PerformanceRecorder.Recorder.RecordingTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    internal abstract class BaseStringResultFormatter : IResultFormatter<string>
    {
        private const string PlainResultFormat = " count: {1}  sum: {2:0._dec_len_}  avg: {3:0._dec_len_}  max: {4:0._dec_len_}  min: {5:0._dec_len_}";

        private const string PaddedResultFormat = "count: {1,_count_len_}  sum: {2,_num_len_:0._dec_len_}  avg: {3,_num_len_:0._dec_len_}  max: {4,_num_len_:0._dec_len_}  min: {5,_num_len_:0._dec_len_}";

        protected const string DolarSignCharacter = "$";

        protected bool IncludeNamespaceInString { get;  }

        protected int DecimalPlacesInResult { get; }

        protected BaseStringResultFormatter(bool includeNamespaceInString, int decimalPlacesInResult)
        {
            IncludeNamespaceInString = includeNamespaceInString;
            DecimalPlacesInResult = decimalPlacesInResult;
        }

        public abstract string FormatAs(IRecordingTree results, Func<IRecordingResult, bool> filterFunction);

        public string FormatAs(IRecordingTree results)
        {
            return FormatAs(results, r => true);
        }

        protected string GetPlainResultFormat()
        {
            return PlainResultFormat.Replace("_dec_len_", RepeatString("0", DecimalPlacesInResult));
        }

        protected string GetPaddedResultFormat()
        {
            return PaddedResultFormat.Replace("_dec_len_", RepeatString("0", DecimalPlacesInResult));
        }

        protected int FindLengthOfLongestResultName(ICollection<IRecordingResult> results)
        {
            return results.Select(r => GenerateResultName(r).Length).Max();
        }

        protected int FindLengthOfLongestCount(ICollection<IRecordingResult> results)
        {
            double maxSum = results.Select(r => r.Count).Max();
            return string.Format("{0:0}", maxSum).Length;
        }

        protected int FindLengthOfLongestValue(ICollection<IRecordingResult> results)
        {
            double maxSum = results.Select(r => r.Sum).Max();
            string sumFormat = "{0:0._dec_len_}"
                .Replace("_dec_len_", RepeatString("0", DecimalPlacesInResult));

            return string.Format(sumFormat, maxSum).Length;
        }

        protected string GenerateResultName(IRecordingResult result)
        {
            if (IncludeNamespaceInString)
            {
                return string.Format("{0}.{1}.{2}", result.Namespace, result.ClassName, result.MethodName);
            }
            else
            {
                return string.Format("{0}.{1}", result.ClassName, result.MethodName);
            }
        }

        protected string AlignAndRemoveDolarSigns(string input)
        {
            string[] lines = Regex.Split(input, Environment.NewLine);
            int targetIndex = lines.Select(l => l.IndexOf(DolarSignCharacter)).Max();

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex];
                int indexOfMarker = line.IndexOf(DolarSignCharacter);
                line = line.Replace(DolarSignCharacter, RepeatString(" ", targetIndex - indexOfMarker));
                lines[lineIndex] = line;
            }
            return string.Join(Environment.NewLine, lines);
        }

        protected string RepeatString(string input, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(input);
            }
            return sb.ToString();
        }
    }
}