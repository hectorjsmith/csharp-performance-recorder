using PerformanceRecorder.Recorder.RecordingTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    internal class PlainStringResultFormatterImpl : BaseStringResultFormatter<IRecordingResult>
    {
        public PlainStringResultFormatterImpl(bool includeNamespaceInString, int decimalPlacesInResult)
            : base(includeNamespaceInString, decimalPlacesInResult)
        {
        }

        public override string FormatAs(IRecordingTree treeResults, Func<IRecordingResult, bool> filterFunction)
        {
            string formatString = "{0} " + GetPlainResultFormat();
            StringBuilder sb = new StringBuilder();

            ICollection<IRecordingResult> results = treeResults.FlattenAndCombine().ToList();
            foreach (IRecordingResult result in results.Where(filterFunction).OrderByDescending(r => r.Sum))
            {
                sb.Append(string.Format(formatString,
                    GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}