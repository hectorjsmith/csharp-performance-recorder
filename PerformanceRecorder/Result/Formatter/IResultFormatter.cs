using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result.Formatter
{
    interface IResultFormatter<TOutputType>
    {
        bool IncludeNamespaceInString { get; set; }

        TOutputType FormatAs(ICollection<IRecordingResult> results);
    }
}
