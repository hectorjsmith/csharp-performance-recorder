using PerformanceRecorder.Manager;
using System;
using PerformanceRecorder.Formatter.Helper;
using PerformanceRecorder.Result;

namespace PerformanceRecorder.Formatter.Impl
{
    internal abstract class BaseStringResultFormatter<TRecordingType> : IResultFormatter<string, TRecordingType>
        where TRecordingType : IRecordingResult
    {
        protected const string NumberLengthPlaceholder = "_num_len_";
        protected const string CountLengthPlaceholder = "_count_len_";
        private const string DecimalPointPlaceholder = "_dec_len_";
        private const string PlainResultFormat = "{0}  count: {1}  sum: {2:0._dec_len_}  avg: {3:0._dec_len_}  max: {4:0._dec_len_}  min: {5:0._dec_len_}";
        private const string PaddedResultFormat = "{0}  count: {1,_count_len_}  sum: {2,_num_len_:0._dec_len_}  avg: {3,_num_len_:0._dec_len_}  max: {4,_num_len_:0._dec_len_}  min: {5,_num_len_:0._dec_len_}";

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

        protected string PlainResultFormatString => ReplaceDecimalPlacePaddingInString(PlainResultFormat);

        protected string PaddedResultFormatString => ReplaceDecimalPlacePaddingInString(PaddedResultFormat);

        public abstract string FormatAs(IRecordingSessionResult results, Func<TRecordingType, bool> filterFunction);

        public string FormatAs(IRecordingSessionResult results)
        {
            return FormatAs(results, r => true);
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
