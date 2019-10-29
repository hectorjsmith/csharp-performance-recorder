using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    class NestedStringResultFormatterImpl : BaseStringResultFormatter
    {
        private string RawFormatString = "{0} $ " + PaddedResultFormat;

        public override string FormatAs(IRecordingTree results)
        {
            List<IRecordingResult> flatResults = results.Flatten().ToList();
            int countLenght = FindLengthOfLongestCount(flatResults);
            int fieldLength = FindLengthOfLongestValue(flatResults);

            return AlignAndRemoveDolarSigns(PrintTree(results, "", true, countLenght, fieldLength));
        }

        // Inspired by: https://stackoverflow.com/a/8567550
        string PrintTree(IRecordingTree tree, string indent, bool last, int maxCountLength, int maxFieldLength)
        {
            string result = (indent + "+- " + FormatStringForRecording(tree.Value, maxCountLength, maxFieldLength)) + Environment.NewLine;
            indent += last ? "   " : "|  ";

            int count = tree.ChildCount;
            int index = 0;
            foreach (IRecordingTree child in tree.Children())
            {
                bool isLastChild = (index == count - 1);
                result += PrintTree(child, indent, isLastChild, maxCountLength, maxFieldLength);
                index++;
            }
            return result;
        }

        private string FormatStringForRecording(IRecordingResult result, int maxCountLength, int maxFieldLength)
        {
            if (result == null)
            {
                return "";
            }
            string formatString = RawFormatString
                .Replace("_count_len_", "" + maxCountLength)
                .Replace("_num_len_", "" + maxFieldLength);
            return string.Format(formatString,
                GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min);
        }
    }
}
