using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    abstract class BaseStringResultFormatter : IResultFormatter<string>
    {
        protected const string PaddedResultFormat = "count: {1,_count_len_}  sum: {2,_num_len_:0.00}  avg: {3,_num_len_:0.00}  max: {4,_num_len_:0.00}  min: {5,_num_len_:0.00}";

        protected const string DolarSignCharacter = "$";

        public bool IncludeNamespaceInString { get; set; }

        public abstract string FormatAs(IRecordingTree results);

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
            return string.Format("{0:0.00}", maxSum).Length;
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
            string[] lines = input.Split(Environment.NewLine);
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
