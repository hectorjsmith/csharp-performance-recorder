---
title: Custom Formatters
weight: 30
---

In addition to the standard formatters provided with the library, it is possible to define custom formatters.
These custom formatters can be used to render the results in formats not supported natively by the library.

While it is possible to extend one of the provided interfaces to create new formatters, that is not a requirement. A completely custom class can be used.

- `IResultFormatter` - The most general interface. Is generic on the type of result data as well as on the return type
- `IResultWithDepthFormatter` - Generic on the return type, but takes recording results with depth
- `IStringResultFormatter` - Generic on the type of recording data, but returns a string
- `IStringResultWithDepthFormatter` - The strictest interface. Returns a string and takes result data with depth

## Example

The following custom formatter (defined in the example application) prints a flat list of recording results with the method name followed by the sum of all execution times.

{{< highlight csharp "linenos=table" >}}
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
{{< /highlight >}}
