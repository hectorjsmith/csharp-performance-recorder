---
title: Initial Setup
weight: 10
---

## API

The library exposes an API class to serve as the main entry point to the functionality provided by the library.

Create a new instance of the API using the default constructor:

```csharp
IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
```

{{< hint info >}}
**NOTE**: In the background all instances of the API link back to a static state.
This means that any number of API instances can be created and they will all share the same state.
{{< /hint >}}

## Attributes

Add the `[PerformanceLogging]` attribute to any method you want to record.

```csharp
[PerformanceLogging]
public void RunApplication()
{
    // ...
}
```

It is possible to add the attribute to
- Instance methods (any visibility)
- Properties (get & set) (any visibility)
- Classes (all methods & properties of that class inherit the attribute)

{{< hint warning >}}
**NOTE:** Instrumenting `static` methods is currently not supported.
{{< /hint >}}

## Toggle Recording

Use an API instance to globally enable or disable recording.

This takes effect immediately across the entire application.

{{< tabs "initialSetupToggleRecording" >}}
{{< tab "Methods" >}}

```csharp
api.EnablePerformanceRecording();
// ...
api.DisablePerformanceRecording();
```

{{< /tab >}}
{{< tab "Property" >}}

```csharp
api.IsRecordingEnabled = true;
// ...
api.IsRecordingEnabled = false;
```

{{< /tab >}}
{{< /tabs >}}

Related: [Print Results](/gettingStarted/printResults) | [Recording Actions](/advanced/recordActions)
