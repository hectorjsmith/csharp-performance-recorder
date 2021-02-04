---
title: Result Data
weight: 10
---

Access the result data from an API instance.

{{< highlight csharp "linenos=table" >}}
// Get results from API
IRecordingSessionResult results = api.GetResults();
{{< /highlight >}}

The `IRecordingSessionResult` interface exposes both the recording result tree, and a flat collection of results.

## Recording Result Data

The `IRecordingResult` interface exposes all the data that is collected for a given method.

The recording data includes the name of the method, how many times it was called, the total execution time across all invocations, the longest/shortest execution time, and the average execution time.

{{< hint info >}}
**NOTE:** All execution times are stored in milliseconds with 3 decimal places.
{{< /hint >}}

The `IRecordingResultWithDepth` interface extends the `IRecordingResult` interface and adds a `Depth` property.
This property is based on how *deep* the method is in the instrumented call stack.

For example, if `methodA` calls `methodB`, the *depth* of `methodB` will be equal to the *depth* of `methodA` plus 1.

{{< hint info >}}
**NOTE:** The `Depth` property only considers recorded methods.
{{< /hint >}}

## Result Data Tree

As the name suggests, the result data tree (`IRecordingTree`) stores the method execution times in a tree structure.
This tree structure mirrors the call stack.

For example, if `methodA` calls `methodB`, the recording data for `methodB` will be stored as a child of the recording data for `methodA`.

The result data tree can be used to create [custom formatters]({{< ref "/advanced/customFormatters" >}}) that take the *depth* of a given method into account.

{{< hint info >}}
**NOTE:** If a method (e.g. `methodA`) gets called from two places (e.g. `methodB` and `methodC`), that method will appear in two places in the recording result tree.

This makes it possible to see the execution time of a given method based on the context it was called from.
{{< /hint >}}

## Flat Recording Data

The `FlatRecordingData` property returns a flattened collection of recording results.

The flat result data should be used when processing the recording results without taking the depth into consideration. An example of this is the *padded result formatter*.

{{< hint info >}}
**NOTE:** When the results are flattened, all results for the same method get combined into a single result. Once the result data is flattened, the *depth* data is lost, so it no longer makes sense to have multiple results for the same method.
{{< /hint >}}
