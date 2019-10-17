using PerformanceRecorder.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApplication.Worker
{
    // You can add the attribute to entire classes (will apply to all properties and methods)
    [PerformanceLogging]
    class WorkerImpl : IWorker
    {
        public void RunOperationA()
        {
        }

        public void RunOperationB()
        {

        }
    }
}
