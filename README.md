# C# Performance Recorder

[![pipeline status](https://gitlab.com/hectorjsmith/csharp-performance-recorder/badges/master/pipeline.svg)](https://gitlab.com/hectorjsmith/csharp-performance-recorder/commits/master)

C# performance recorder library using aspect oriented programming.

## How to Use

*See the included example project to see it in action*

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

### Results

You can retrieve the results of a performance recording session using the API

```csharp
IRecordingSessionResult results = api.GetResults();
```

The session results object has methods to either get the raw results, or get the results formatted in a certain way.

Using the `.ToPaddedString()` method will return all the results as a string where each instrumented method is on a separate line and all columns have been padded so that they line up.
Sample output:

```
ApplicationImpl.RunApplication  count:  1  sum: 4.12  avg: 4.12  max: 4.12  min: 4.12
        ApplicationImpl.Worker  count: 11  sum: 2.47  avg: 0.22  max: 2.31  min: 0.00
      WorkerImpl.RunOperationA  count: 10  sum: 0.18  avg: 0.02  max: 0.18  min: 0.00
              WorkerImpl..ctor  count: 11  sum: 0.18  avg: 0.02  max: 0.17  min: 0.00
      WorkerImpl.RunOperationB  count:  1  sum: 0.13  avg: 0.13  max: 0.13  min: 0.13
```

When getting the results data as a string, the `IncludeNamespaceInString` property will control whether the full namespace is included in the output.
If set to true, the method name will include the namespace, class name, and method name. Otherwise it will only include class name and method name.

It is also possible to get the raw results using the `.RawData()` method.
The raw results are in the form of a collection of `IRecordingResult` objects.
From these objects you can retrieve information about the method that was instrumented, how many times it was executed, the minimum and maximum execution time, the average execution time, and the total amount of time spent executing that method.

```csharp
public interface IRecordingResult
{
    string Namespace { get; }

    string ClassName { get; }

    string MethodName { get; }

    double Sum { get; }

    double Count { get; }

    double Max { get; }

    double Min { get; }

    double Avg { get; }
    
    ///...
}
```

## Credits

*Made possible by the [AspectInjector](https://github.com/pamidur/aspect-injector) library*
