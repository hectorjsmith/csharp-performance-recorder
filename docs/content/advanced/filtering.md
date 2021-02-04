---
title: Filtering
weight: 20
---

The result formatters support filtering the results to include.
This makes it possible to, for example, only show methods that took over 10ms to run.
Or methods that were called more than 10 times.

Filtering is done by providing a filter function to the format method.

{{< highlight csharp "linenos=table" >}}
// Get formatter
IFormatterFactoryApi formatterFactory = api.NewFormatterFactoryApi();
var formatter = formatterFactory.NewNestedStringResultFormatter();

// Format results with filter
string str = formatter.FormatAs(results, result => result.Count > 10);
{{< /highlight >}}

{{< hint info >}}
**NOTE:** The *nested result formatter* is the only default formatter which can be filtered by *depth*. This is because the other formatters use the flat result data which does not contain *depth* data.
{{< /hint >}}
