---
title: Documentation
---

<!-- markdownlint-capture -->
<!-- markdownlint-disable MD033 -->

<span class="badge-placeholder">[![Project](https://img.shields.io/badge/project-gitlab-brightgreen?style=flat&logo=gitlab)](https://gitlab.com/hectorjsmith/csharp-performance-recorder/)</span>
<span class="badge-placeholder">[![Build Status](https://gitlab.com/hectorjsmith/csharp-performance-recorder/badges/develop/pipeline.svg)](https://gitlab.com/hectorjsmith/csharp-performance-recorder/commits/develop)</span>
<span class="badge-placeholder">[![Nuget Version](https://user-content.gitlab-static.net/b9d2e4b793a5d69da23c0777e858fa6405ef1b71/68747470733a2f2f62616467656e2e6e65742f6e756765742f762f506572666f726d616e63655265636f726465722f6c6174657374)](https://www.nuget.org/packages/PerformanceRecorder/)</span>
<span class="badge-placeholder">[![License: MIT](https://img.shields.io/badge/license-MIT-brightgreen)](https://gitlab.com/hectorjsmith/csharp-performance-recorder/-/blob/develop/LICENSE)</span>

<!-- markdownlint-restore -->

Use the `[PerformanceLogging]` attribute to record the execution time of methods in your .NET application.

## Features

- Easy to instrument methods, just add the attribute
- Built using AOP (aspect oriented programming)
- Show nested results for insights into where methods are in the callstack

## Sample

### Instrumented Method

```csharp
[PerformanceLogging]
public void RunApplication()
{
    // ...
}
```

### Output

```
+-
   +- ApplicationImpl.RunApplication              count:  1  sum: 1.85  avg: 1.85  max: 1.85  min: 1.85
      +- WorkerImpl.RunOperationB                 count:  1  sum: 1.13  avg: 1.13  max: 1.13  min: 1.13
      |  +- WorkerImpl.RunPrivateOperationB2      count:  1  sum: 0.41  avg: 0.41  max: 0.41  min: 0.41
      |  |  +- WorkerImpl.RunPrivateOperationB21  count:  1  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
      +- ApplicationImpl.Worker                   count: 11  sum: 0.16  avg: 0.01  max: 0.14  min: 0.00
         +- WorkerImpl..ctor                      count: 11  sum: 0.00  avg: 0.00  max: 0.00  min: 0.00
```
