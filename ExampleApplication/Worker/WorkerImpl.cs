using PerformanceRecorder.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApplication.Worker
{
    class WorkerImpl : IWorker
    {
        [PerformanceLogging]
        public void RunOperationA()
        {
        }
    }
}
