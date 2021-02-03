---
title: Printing Results
weight: 20
---

Use the API to get the results of a given recording session.

```csharp
IRecordingSessionResult results = api.GetResults();
```

{{< hint info >}}
**NOTE:** You can run this at any time, even when recording is enabled. It will return all results up to the point it was called.
{{< /hint >}}

## Format Results

Once you have the recording results, you can use one of the provided formatters to convert them to a string.

```csharp
IFormatterFactoryApi formatterFactory = api.NewFormatterFactoryApi();
var formatter = formatterFactory.NewNestedStringResultFormatter();

Console.WriteLine(formatter.FormatAs(results)
```

**Example Output:**

```
+-
   +- ApplicationImpl.RunApplication              count:  1  sum: 1.85  avg: 1.85  max: 1.85  min: 1.85
      +- WorkerImpl.RunOperationB                 count:  1  sum: 1.13  avg: 1.13  max: 1.13  min: 1.13
      |  +- WorkerImpl.RunPrivateOperationB2      count:  1  sum: 0.41  avg: 0.41  max: 0.41  min: 0.41
      |  |  +- WorkerImpl.RunPrivateOperationB21  count:  1  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
      +- ApplicationImpl.Worker                   count: 11  sum: 0.16  avg: 0.01  max: 0.14  min: 0.00
         +- WorkerImpl..ctor                      count: 11  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
```

Related: [Result Data](/advanced/resultData) | [Filtering](/advanced/filtering) | [Custom Formatters](/advanced/customFormatters)
