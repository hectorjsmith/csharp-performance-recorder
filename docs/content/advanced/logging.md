---
title: Logging
weight: 50
---

The library supports injecting a custom logger implementation.

To do this, use the `SetLogger` method on the library API. Any logger implementation must implement the `ILogger` interface.

{{< highlight csharp "linenos=table" >}}
IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();
// MyCustomLogger implements ILogger
api.SetLogger(new MyCustomLogger());
{{< /highlight >}}

To disable logging, use the `SetLogger` method to set the logger to `null`.

{{< highlight csharp "linenos=table" >}}
// Disable logging
api.SetLogger(null);
{{< /highlight >}}
