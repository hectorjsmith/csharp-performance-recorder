using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    class PlainStringResultFormatterImpl : IResultFormatter<string>
    {
        public string FormatAs(ICollection<IRecordingResult> results)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IRecordingResult result in results.OrderByDescending(r => r.Sum))
            {
                sb.Append(string.Format("{0}.{1}.{2}  count: {3}  sum: {4:0.00}  avg: {5:0.00}  max: {6:0.00}  min: {7:0.00}",
                    result.Namespace, result.ClassName, result.MethodName, result.Count, result.Sum, result.Avg, result.Max, result.Min));
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
