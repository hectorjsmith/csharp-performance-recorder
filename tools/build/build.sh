#!/bin/bash

./tools/build/updateVersion.sh

dotnet build --configuration Release
dotnet pack PerformanceRecorder/PerformanceRecorder.csproj --configuration Release
