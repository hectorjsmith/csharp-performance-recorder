using PerformanceRecorder.Recorder.RecordingTree;
using System;
using System.Collections.Generic;
using System.Linq;
using PerformanceRecorder.Formatter.Helper;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Impl
{
    internal class NestedStringResultFormatterImpl : BaseStringResultFormatter<IRecordingResultWithDepth>, IStringResultWithDepthFormatter
    {
        public NestedStringResultFormatterImpl(bool includeNamespaceInString, int decimalPlacesInResult)
            : base(includeNamespaceInString, decimalPlacesInResult)
        {
        }

        public override string FormatAs(IRecordingSessionResult results, Func<IRecordingResultWithDepth, bool> filterFunction)
        {
            IList<IRecordingResult> flatResults = results.FlatData().ToList();
            if (!flatResults.Any())
            {
                return "";
            }

            int countLength = flatResults.FindLengthOfLongestCount();
            int sumLength = flatResults.FindLengthOfLongestSum(DecimalPlacesInResult);

            IRecordingTree filteredTree = results.RecordingTree.Filter(filterFunction);
            return AlignAndRemoveDolarSigns(PrintTree(filteredTree, "", true, countLength, sumLength));
        }

        // Inspired by: https://stackoverflow.com/a/8567550
        private string PrintTree(IRecordingTree tree, string indent, bool last, int maxCountLength, int maxFieldLength)
        {
            string result = (indent + "+- " + FormatStringForRecording(tree.Value, maxCountLength, maxFieldLength)) + Environment.NewLine;
            indent += last ? "   " : "|  ";

            int count = tree.ChildCount;
            int index = 0;
            foreach (IRecordingTree child in tree.Children().OrderByDescending(c => c.Value?.Sum))
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

            string rawString = "{0} $ " + GetPaddedResultFormat();
            string formatString = rawString
                .Replace(CountLengthPlaceholder, "" + maxCountLength)
                .Replace(NumberLengthPlaceholder, "" + maxFieldLength);
            return string.Format(formatString,
                GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min);
        }
    }
}