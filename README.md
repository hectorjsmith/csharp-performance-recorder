# C# Performance Recorder

[![documentation](https://img.shields.io/badge/docs-latest-orange)](https://hectorjsmith.gitlab.io/csharp-performance-recorder/)
[![pipeline status](https://gitlab.com/hectorjsmith/csharp-performance-recorder/badges/develop/pipeline.svg)](https://gitlab.com/hectorjsmith/csharp-performance-recorder/commits/develop)
[![project version on nuget](https://badgen.net/nuget/v/PerformanceRecorder/latest)](https://www.nuget.org/packages/PerformanceRecorder/)

C# performance recorder library using aspect oriented programming.

## How to Use

*See the included example project to see it in action*

### Installation

Add the library to your project by following the instructions on the [nuget.org package page](https://www.nuget.org/packages/PerformanceRecorder/).

### Attribute

Use the `[PerfomanceLogging]` attribute on your methods, properties, or classes to have their execution time recorded.

```csharp
[PerformanceLogging]
public void RunApplication()
{
    // ...
}
```

### API

The library includes an API class that exposes all the functionality you need.

```csharp
IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
```

Turn on/off execution time recording globally:

```csharp
api.EnablePerformanceRecording();
// ...
api.DisablePerformanceRecording();
```

### Record Actions

It is also possible to record any function or operation by using the `RecordAction` method on the API.

For example:

```csharp
api.RecordAction("myAction", () => {
    // ...
});
```

The provided name is used as the method name in the result data.

### Logging

The library supports injecting a logger object to be used to log issues using the API.
To avoid dependencies on any particular logging library, the logger instance used should implement the `PerformanceRecorder.Log.ILogger` interface.

To inject a logger:

```csharp
IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
ILogger logger = // ...
api.SetLogger(logger);
```

The logger can be set to `null` to disable logging.

### Results

You can retrieve the results of a performance recording session using the API

```csharp
IRecordingSessionResult results = api.GetResults();
```

The `IRecordingSessionResult` interface provides access to all the recording data for that session.

### Provided String Formatters

This project includes a few result formatters to convert the recording results into strings.
Access them using the following factory class:

```csharp
IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
IFormatterFactoryApi formatterFactory = api.NewFormatterFactoryApi();
```

**Plain String**

```csharp
IStringResultFormatter plainFormatter = formatterFactory.NewPlainStringResultFormatter();
```

The results are printed out with one method per line.
Each line includes the method ID and the recording data.

*Example:*

```
ApplicationImpl.RunApplication  count:  1  sum: 4.12  avg: 4.12  max: 4.12  min: 4.12
ApplicationImpl.Worker  count: 11  sum: 2.47  avg: 0.22  max: 2.31  min: 0.00
WorkerImpl.RunOperationA  count: 10  sum: 0.18  avg: 0.02  max: 0.18  min: 0.00
WorkerImpl..ctor  count: 11  sum: 0.18  avg: 0.02  max: 0.17  min: 0.00
WorkerImpl.RunOperationB  count:  1  sum: 0.13  avg: 0.13  max: 0.13  min: 0.13
```

**Padded String**

The same concept as the raw string, except that the different fields are padded in such a way as to form columns (assuming monospace font).

*Example:*

```
ApplicationImpl.RunApplication  count:  1  sum: 4.12  avg: 4.12  max: 4.12  min: 4.12
        ApplicationImpl.Worker  count: 11  sum: 2.47  avg: 0.22  max: 2.31  min: 0.00
      WorkerImpl.RunOperationA  count: 10  sum: 0.18  avg: 0.02  max: 0.18  min: 0.00
              WorkerImpl..ctor  count: 11  sum: 0.18  avg: 0.02  max: 0.17  min: 0.00
      WorkerImpl.RunOperationB  count:  1  sum: 0.13  avg: 0.13  max: 0.13  min: 0.13
```

**Nested String**

This mode will format the results in a tree structure.
This allows to see where methods are called from.

*Example:*

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
And the runtime of the parent method will include the runtime of each child, any missing time is due to either a non-instrumented method/property, or an overhead of the performance recorder.

**Options**

The provided `IFormatterFactoryApi` interface has properties to control the behaviour new formatters built by this factory.
Note that they do not change any existing formatters.

* `factory.IncludeNamespaceInString` - Set to true to show namespaces, false to hide them. The namespace is often quite long and makes the results harder to read.
* `factory.DecimalPlacesInResults` - Control the number of decimal places in the string generated by formatters. Must be between 0 and 3.

**Filtering**

It is possible to filter the output of the string formatters by passing in a filter function to the `FormatAs` method.
The filter function is in the form of: `Func<IRecordingResult, bool> filterFunction`.

Any recording result that fails the filter (the filter method returns false) will be skipped in the output string.
When a filter function is not provided all results are included.

*Example:*

In the following snippet the `stringResults` variable will only include the results that have a total run time of over 1ms.
All other results will be skipped.

```csharp
string stringResutls = paddedFormatter.FormatAs(results, r => r.Sum < 1);
```

**Custom formatters**

Custom formatters can be defined to render the results in different ways.
They can optionally extend the `IResultFormatter<out TOutputType, out TRecordingType>` interface.

See the `MyCustomFormatter` class in the example project for an example on how to define a custom formatter.

### Result Data

Internally the recording results are stored in a tree structure.
Recordings for a method are added to the tree as a child of the first instrumented method above it in the call stack.

The raw tree data is accessible from the `IRecordingSessionResult` interface using the `RecordingTree` property.

The raw result data for each method is stored as a `IRecordingResultWithDepth` interface and includes data about the method execution and how deep it is in the call tree.

Note: The provided padded result formatter (`NewPaddedStringResultFormatter`) generates a string that reflects the internal recording tree structure.

It is also possible to get a flattened collection of results using the `FlatRecordingData` property on the `IRecordingSessionResult` interface.
This property will return a flat collection of recording results where all recordings for the same method have been combined into a single result.
Note that results in this collection will no longer have the `Depth` property.

To get a flat list of results that includes the call depth, use the `results.RecordingTree.Flatten()` method.
This will not combine all results for the same method into a single result and will maintain the depth data for each instance.

## Credits

*Made possible by the [AspectInjector](https://github.com/pamidur/aspect-injector) library*
