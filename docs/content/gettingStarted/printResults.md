---
title: Printing Results
weight: 20
---

Use the API to get the results of a given recording session.

{{< highlight csharp "linenos=table" >}}
// Get results from API
IRecordingSessionResult results = api.GetResults();
{{< /highlight >}}

{{< hint info >}}
**NOTE:** You can run this at any time, even when recording is enabled. It will return all results up to the point it was called.
{{< /hint >}}

## Format Results

Once you have the recording results, you can use one of the provided formatters to convert them to a string.

### Formatter Factory

The formatter factory is used to build all of the provided formatters.

{{< highlight csharp "linenos=table" >}}
// Build new factory
IFormatterFactoryApi formatterFactory = api.NewFormatterFactoryApi();
{{< /highlight >}}

The factory has methods to control the behaviour of the formatters it generates.

- `IncludeNamespaceInString` - controls whether the full method namespace is included in the results
- `DecimalPlacesInResults` - controls how many decimal places to show when showing execution times (max: 3)

### Nested Formatter

In most cases the *nested string formatter* is the most useful.
It renders the results as a tree that makes it easy to see where each method fits in the call stack.

{{< highlight csharp "linenos=table" >}}
IFormatterFactoryApi formatterFactory = api.NewFormatterFactoryApi();
var formatter = formatterFactory.NewNestedStringResultFormatter();

// Write formatted results to console
Console.WriteLine(formatter.FormatAs(results)
{{< /highlight >}}

**Example Output:**

{{< highlight text "linenos=table" >}}
+-
   +- ApplicationImpl.RunApplication              count:  1  sum: 1.85  avg: 1.85  max: 1.85  min: 1.85
      +- WorkerImpl.RunOperationB                 count:  1  sum: 1.13  avg: 1.13  max: 1.13  min: 1.13
      |  +- WorkerImpl.RunPrivateOperationB2      count:  1  sum: 0.41  avg: 0.41  max: 0.41  min: 0.41
      |  |  +- WorkerImpl.RunPrivateOperationB21  count:  1  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
      +- ApplicationImpl.Worker                   count: 11  sum: 0.16  avg: 0.01  max: 0.14  min: 0.00
         +- WorkerImpl..ctor                      count: 11  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
{{< /highlight >}}

---

Related: [Result Data]({{< ref "/advanced/resultData" >}}) | [Filtering]({{< ref "/advanced/filtering" >}}) | [Custom Formatters]({{< ref "/advanced/customFormatters" >}}) 
