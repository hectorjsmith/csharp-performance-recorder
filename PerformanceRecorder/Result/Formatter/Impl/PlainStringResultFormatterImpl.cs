using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    internal class PlainStringResultFormatterImpl : IResultFormatter<string>
    {
        public bool IncludeNamespaceInString { get; set; }

        public string FormatAs(ICollection<IRecordingResult> results)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IRecordingResult result in results.OrderByDescending(r => r.Sum))
            {
                sb.Append(string.Format("{0}  count: {1}  sum: {2:0.00}  avg: {3:0.00}  max: {4:0.00}  min: {5:0.00}",
                    GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        private string GenerateResultName(IRecordingResult result)
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