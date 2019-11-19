#!/bin/bash

csprojPath=PerformanceRecorder/PerformanceRecorder.csproj
version=`git describe --tags`

echo "Updating version to: ${version}"

sed -e "s;<Version>.*</Version>;<Version>${version}</Version>;g" -i ${csprojPath} -b
