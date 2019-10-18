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
ICollection<IRecordingResult> results = api.GetResults();
```

The results are in the form of a collection of `IRecordingResult` objects. From these objects you can retrieve information about the method that was instrumented, how many times it was executed, the minimum and maximum execution time, the average execution time, and the total amount of time spent executing that method.

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
