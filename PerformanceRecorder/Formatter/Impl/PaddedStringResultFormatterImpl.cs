using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerformanceRecorder.Formatter.Helper;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Impl
{
    internal class PaddedStringResultFormatterImpl : BaseStringResultFormatter<IRecordingResult>, IStringResultFormatter
    {
        private const string AlignmentMarker = "%";

        public PaddedStringResultFormatterImpl(bool includeNamespaceInString, int decimalPlacesInResult)
            : base(includeNamespaceInString, decimalPlacesInResult)
        {
        }

        public override string FormatAs(IRecordingSessionResult treeResults,
            Func<IRecordingResult, bool> filterFunction)
        {
            IList<IRecordingResult> results = treeResults.FlatData().ToList();
            if (!results.Any())
            {
                return "";
            }

            int countLength = results.FindLengthOfLongestCount();
            int sumLength = results.FindLengthOfLongestSum(DecimalPlacesInResult);

            string formatString = PaddedResultFormatString
                .Replace(CountLengthPlaceholder, "" + countLength)
                .Replace(NumberLengthPlaceholder, "" + sumLength);

            StringBuilder sb = new StringBuilder();
            foreach (IRecordingResult result in results.Where(filterFunction).OrderByDescending(r => r.Sum))
            {
                string resultNameWithAlignmentMarker =
                    result.GenerateResultName(IncludeNamespaceInString) + AlignmentMarker;

                sb.Append(string.Format(formatString,
                    resultNameWithAlignmentMarker, result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }

            return sb.ToString().AlignStringsToMarker(AlignmentMarker, alignRight: true);
        }
    }
}