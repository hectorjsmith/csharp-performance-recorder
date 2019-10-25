using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    abstract class BaseStringResultFormatter : IResultFormatter<string>
    {
        public bool IncludeNamespaceInString { get; set; }

        public abstract string FormatAs(IRecordingTree results);

        protected int FindLengthOfLongestResultName(ICollection<IRecordingResult> results)
        {
            return results.Select(r => GenerateResultName(r).Length).Max();
        }

        protected int FindLengthOfLongestCount(ICollection<IRecordingResult> results)
        {
            double maxSum = results.Select(r => r.Count).Max();
            return string.Format("{0:0}", maxSum).Length;
        }

        protected int FindLengthOfLongestValue(ICollection<IRecordingResult> results)
        {
            double maxSum = results.Select(r => r.Sum).Max();
            return string.Format("{0:0.00}", maxSum).Length;
        }

        protected string GenerateResultName(IRecordingResult result)
        {
            if (IncludeNamespaceInString)
            {
                return string.Format("{0}.{1}.{2}", result.Namespace, result.ClassName, result.MethodName);
            }
            else
            {
                return string.Format("{0}.{1}", result.ClassName, result.MethodName);
            }
        }
    }
}
