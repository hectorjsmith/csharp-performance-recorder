#!/bin/bash

./BuildTools/UpdateVersion.sh

dotnet build --configuration Release
dotnet pack PerformanceRecorder/PerformanceRecorder.csproj --configuration Release
