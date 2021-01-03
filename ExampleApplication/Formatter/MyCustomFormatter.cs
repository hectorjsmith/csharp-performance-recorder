using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PerformanceRecorder.Formatter;
using PerformanceRecorder.Result;

namespace ExampleApplication.Formatter
{
    internal class MyCustomFormatter : IStringResultFormatter
    {
        public string FormatAs(IRecordingSessionResult results)
        {
            return FormatAs(results, r => true);
        }

        public string FormatAs(IRecordingSessionResult results, Func<IRecordingResult, bool> filterFunction)
        {
            IEnumerable<string> resultsAsMethodNameAndSum = results
                .RecordingTree
                .Flatten()
                .Select(r => $"{r.MethodName}: {r.Sum} ms");
            return string.Join("\r\n", resultsAsMethodNameAndSum);
        }
    }
}