using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerformanceRecorder.Formatter.Helper;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Impl
{
    internal class PlainStringResultFormatterImpl : BaseStringResultFormatter<IRecordingResult>, IStringResultFormatter
    {
        public PlainStringResultFormatterImpl(bool includeNamespaceInString, int decimalPlacesInResult)
            : base(includeNamespaceInString, decimalPlacesInResult)
        {
        }

        public override string FormatAs(IRecordingSessionResult results, Func<IRecordingResult, bool> filterFunction)
        {
            StringBuilder sb = new StringBuilder();

            ICollection<IRecordingResult> flatResults = results.FlatRecordingData;
            foreach (IRecordingResult result in flatResults.Where(filterFunction).OrderByDescending(r => r.Sum))
            {
                sb.Append(string.Format(PlainResultFormatString,
                    result.GenerateResultName(IncludeNamespaceInString), result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}