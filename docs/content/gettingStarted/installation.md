---
title: Installation
---

The recommended way to install this library is to install it from Nuget.

Follow the instructions on the [nuget.org package page](https://www.nuget.org/packages/PerformanceRecorder/) to add the library to your project.

## Alternative: Compile from Source

Use the build tools provided to build the project from source.
These are the same steps the CI/CD process uses to compile the library.

**1. Set Version**

Use the `tools/build/updateVersion.sh` script to inject the current project version into the `.csproj` file.

{{< hint info >}}
**NOTE:** The project is versioned based on git tags. The `.csproj` file in the repository has the version set to `0.0.0`.
{{< /hint >}}

**2. Build Project**

Use the `tools/build/build.sh` script to build out the project and generate a `.nupkg` file.
This file can then be manually added to a project.

