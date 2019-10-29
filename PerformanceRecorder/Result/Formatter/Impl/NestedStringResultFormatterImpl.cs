using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    class NestedStringResultFormatterImpl : BaseStringResultFormatter
    {
        public override string FormatAs(IRecordingTree results)
        {
            return AlignAndRemoveDolarSigns(PrintTree(results, "", true));
        }

        // Inspired by: https://stackoverflow.com/a/8567550
        string PrintTree(IRecordingTree tree, string indent, bool last)
        {
            string result = (indent + "+- " + FormatStringForRecording(tree.Value)) + Environment.NewLine;
            indent += last ? "   " : "|  ";

            int count = tree.ChildCount;
            int index = 0;
            foreach (IRecordingTree child in tree.Children())
            {
                bool isLastChild = (index == count - 1);
                result += PrintTree(child, indent, isLastChild);
                index++;
            }
            return result;
        }

        private string FormatStringForRecording(IRecordingResult result)
        {
            if (result == null)
            {
                return "";
            }
            return string.Format("{0} $ count: {1,1}  sum: {2:0.00}  avg: {3:0.00}  max: {4:0.00}  min: {5:0.00}",
                GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min);
        }
    }
}
