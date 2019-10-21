using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result.Formatter
{
    interface IResultFormatter<TOutputType>
    {
        TOutputType FormatAs(ICollection<IRecordingResult> results);
    }
}
