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

        public override string FormatAs(IRecordingSessionResult results,
            Func<IRecordingResult, bool> filterFunction)
        {
            ICollection<IRecordingResult> flatResults = results.FlatRecordingData;
            if (!flatResults.Any())
            {
                return "";
            }

            int countLength = flatResults.FindLengthOfLongestCount();
            int sumLength = flatResults.FindLengthOfLongestSum(DecimalPlacesInResult);

            string formatString = PaddedResultFormatString
                .Replace(CountLengthPlaceholder, "" + countLength)
                .Replace(NumberLengthPlaceholder, "" + sumLength);

            StringBuilder sb = new StringBuilder();
            foreach (IRecordingResult result in flatResults.Where(filterFunction).OrderByDescending(r => r.Sum))
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