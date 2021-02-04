# C# Performance Recorder

[![documentation](https://img.shields.io/badge/docs-latest-orange)](https://hectorjsmith.gitlab.io/csharp-performance-recorder/)
[![pipeline status](https://gitlab.com/hectorjsmith/csharp-performance-recorder/badges/develop/pipeline.svg)](https://gitlab.com/hectorjsmith/csharp-performance-recorder/commits/develop)
[![project version on nuget](https://badgen.net/nuget/v/PerformanceRecorder/latest)](https://www.nuget.org/packages/PerformanceRecorder/)

C# performance recorder library using aspect oriented programming.

Project documentation available [here](https://hectorjsmith.gitlab.io/csharp-performance-recorder/).

### Installation

Add the library to your project by following the instructions on the [nuget.org package page](https://www.nuget.org/packages/PerformanceRecorder/).

### Add Attribute

Use the `[PerfomanceLogging]` attribute on your methods, properties, or classes to have their execution time recorded.

```csharp
[PerformanceLogging]
public void RunApplication()
{
    // ...
}
```

### Enable Recording

Enable recording on the library API to have all method execution times recorded.

```csharp
IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
api.EnablePerformanceRecording();
```

### Print Results

Retrieve the results of a performance recording session using the API

```csharp
IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
IRecordingSessionResult results = api.GetResults();
```

The `IRecordingSessionResult` interface provides access to all the recording data for that session.

Use the one of the provided formatters (in this case, the *nested string formatter*) to print the results.

```csharp
IFormatterFactoryApi formatterFactory = api.NewFormatterFactoryApi();
var formatter = formatterFactory.NewNestedStringResultFormatter();

// Write formatted results to console
Console.WriteLine(formatter.FormatAs(results)
```

*The nested formatter will format the results in a tree structure to show where methods are called from.*

**Output:**

```
+-
   +- ApplicationImpl.RunApplication              count:  1  sum: 1.85  avg: 1.85  max: 1.85  min: 1.85
      +- WorkerImpl.RunOperationB                 count:  1  sum: 1.13  avg: 1.13  max: 1.13  min: 1.13
      |  +- WorkerImpl.RunPrivateOperationB2      count:  1  sum: 0.41  avg: 0.41  max: 0.41  min: 0.41
      |  |  +- WorkerImpl.RunPrivateOperationB21  count:  1  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
      +- ApplicationImpl.Worker                   count: 11  sum: 0.16  avg: 0.01  max: 0.14  min: 0.00
         +- WorkerImpl..ctor                      count: 11  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
```

As the indentation of the method ID increases, so does the depth at which it was run. In the example above, `RunPrivateOperationB2` was run from `RunOperationB`.
The execution time of the parent method will include the time of each child, any missing time is due to either a non-instrumented method/property, or an overhead of the performance recorder.

## Credits

*Made possible by the [AspectInjector](https://github.com/pamidur/aspect-injector) library*
