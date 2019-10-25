using System;
using System.Collections.Generic;
using System.Text;
using PerformanceRecorder.Recorder.RecordingTree;

namespace PerformanceRecorder.Result.Formatter.Impl
{
    class NestedStringResultFormatterImpl : BaseStringResultFormatter
    {
        public override string FormatAs(IRecordingTree results)
        {
            if (results.ChildCount == 0)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();

            foreach (IRecordingTree child in results.Children())
            {
                sb.Append(" ├── ");
                sb.Append(FormatStringForRecording(child.Value));
                sb.Append(Environment.NewLine);
                sb.Append(" ├── ");
                sb.Append(FormatAs(child));
            }

            return sb.ToString();
        }

        private string FormatStringForRecording(IRecordingResult result)
        {
            if (result == null)
            {
                return "";
            }
            return string.Format("{0}  count: {1,1}  sum: {2:0.00}  avg: {3:0.00}  max: {4:0.00}  min: {5:0.00}",
                GenerateResultName(result), result.Count, result.Sum, result.Avg, result.Max, result.Min);
        }
    }
}
