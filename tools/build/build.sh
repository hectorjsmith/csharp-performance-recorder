#!/bin/bash

dotnet build --configuration Release
dotnet pack PerformanceRecorder/PerformanceRecorder.csproj --configuration Release
