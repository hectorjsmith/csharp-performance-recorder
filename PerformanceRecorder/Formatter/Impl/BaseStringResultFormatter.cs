using PerformanceRecorder.Manager;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using PerformanceRecorder.Formatter.Helper;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Impl
{
    internal abstract class BaseStringResultFormatter<TRecordingType> : IResultFormatter<string, TRecordingType>
        where TRecordingType : IRecordingResult
    {
        protected const string NumberLengthPlaceholder = "_num_len_";
        protected const string CountLengthPlaceholder = "_count_len_";
        protected const string DecimalPointPlaceholder = "_dec_len_";
        protected const string DolarSignCharacter = "$";
        private const string PlainResultFormat = " count: {1}  sum: {2:0._dec_len_}  avg: {3:0._dec_len_}  max: {4:0._dec_len_}  min: {5:0._dec_len_}";
        private const string PaddedResultFormat = "count: {1,_count_len_}  sum: {2,_num_len_:0._dec_len_}  avg: {3,_num_len_:0._dec_len_}  max: {4,_num_len_:0._dec_len_}  min: {5,_num_len_:0._dec_len_}";

        protected BaseStringResultFormatter(bool includeNamespaceInString, int decimalPlacesInResult)
        {
            IncludeNamespaceInString = includeNamespaceInString;
            if (decimalPlacesInResult < 0)
            {
                StaticRecorderManager.Logger.Error(
                    $"Provided number of decimal places ({decimalPlacesInResult}) must be greater than 0, defaulting to 0");
                DecimalPlacesInResult = 0;
            }
            else if (decimalPlacesInResult > 3)
            {
                StaticRecorderManager.Logger.Error(
                    $"Provided number of decimal places ({decimalPlacesInResult}) must not be greater than 3, defaulting to 3");
                DecimalPlacesInResult = 3;
            }
            else
            {
                DecimalPlacesInResult = decimalPlacesInResult;
            }
        }

        protected bool IncludeNamespaceInString { get; }

        protected int DecimalPlacesInResult { get; }

        public abstract string FormatAs(IRecordingSessionResult results, Func<TRecordingType, bool> filterFunction);

        public string FormatAs(IRecordingSessionResult results)
        {
            return FormatAs(results, r => true);
        }

        protected string GetPlainResultFormat()
        {
            return ReplaceDecimalPlacePaddingInString(PlainResultFormat);
        }

        protected string GetPaddedResultFormat()
        {
            return ReplaceDecimalPlacePaddingInString(PaddedResultFormat);
        }

        protected string AlignAndRemoveDolarSigns(string input)
        {
            string[] lines = Regex.Split(input, Environment.NewLine);
            int targetIndex = lines.Select(l => l.IndexOf(DolarSignCharacter)).Max();

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex];
                int indexOfMarker = line.IndexOf(DolarSignCharacter);
                line = line.Replace(DolarSignCharacter, " ".Repeat(targetIndex - indexOfMarker));
                lines[lineIndex] = line;
            }
            return string.Join(Environment.NewLine, lines);
        }

        private string ReplaceDecimalPlacePaddingInString(string formatString)
        {
            if (DecimalPlacesInResult > 0)
            {
                return formatString.Replace(DecimalPointPlaceholder, "." + "0".Repeat(DecimalPlacesInResult));
            }
            else
            {
                return formatString.Replace(DecimalPointPlaceholder, "");
            }
        }
    }
}
