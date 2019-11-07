using PerformanceRecorder.Recorder.RecordingTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    internal class PlainStringResultFormatterImpl : BaseStringResultFormatter, IResultFormatter<string>
    {
        public override string FormatAs(IRecordingTree treeResults, Func<IRecordingResult, bool> filterFunction)
        {
            ICollection<IRecordingResult> results = treeResults.FlattenAndCombine().ToList();

            StringBuilder sb = new StringBuilder();
            foreach (IRecordingResult result in results.Where(filterFunction).OrderByDescending(r => r.Sum))
            {
                sb.Append(string.Format("{0}  count: {1}  sum: {2:0.00}  avg: {3:0.00}  max: {4:0.00}  min: {5:0.00}",
                    GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}