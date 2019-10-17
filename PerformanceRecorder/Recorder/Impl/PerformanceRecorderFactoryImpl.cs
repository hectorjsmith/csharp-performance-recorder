using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Recorder.Impl
{
    class PerformanceRecorderFactoryImpl : IPerformanceRecorderFactory
    {
        public IPerformanceRecorder New()
        {
            return new ActivePerformanceRecorderImpl();
        }
    }
}
