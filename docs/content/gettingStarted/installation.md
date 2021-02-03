---
title: Installation
---

The recommended way to install this library is to install it from Nuget.

Follow the instructions on the [nuget.org package page](https://www.nuget.org/packages/PerformanceRecorder/) to add the library to your project.

## Alternative: Compile from Source

Use the build tools provided to build the project from source.

**1. Set Version**

Use the `tools/build/updateVersion.sh` script to inject the project version into the `.csproj` file.

{{< hint info >}}
**NOTE:** Versioning is done based on git tags.
{{< /hint >}}

**2. Build Project**

Use the `tools/build/build.sh` script to build out the project and generate a `.nupkg` file.
This file can then be manually added to a project.

{{< hint info >}}
**NOTE:** This are the same steps the Gitlab CI/CD project uses to build the project.
{{< /hint >}}
