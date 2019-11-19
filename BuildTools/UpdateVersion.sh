#!/bin/bash

csprojPath=PerformanceRecorder/PerformanceRecorder.csproj
version=`git describe --tags`

sed -e "s;<Version>.*</Version>;<Version>${version}</Version>;g" -i ${csprojPath} -b
