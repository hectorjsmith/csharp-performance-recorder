using ExampleApplication.Worker;
using PerformanceRecorder.API;
using PerformanceRecorder.API.Impl;
using PerformanceRecorder.Attribute;
using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleApplication.App
{
    class ApplicationImpl : IApplication
    {
        // You can add the attribute to properties
        [PerformanceLogging]
        private IWorker Worker => new WorkerImpl();

        // You can add the attribute to methods
        [PerformanceLogging]
        public void RunApplication()
        {
            for (int i = 0; i < 10; i++)
            {
                Worker.RunOperationA();
            }
            Worker.RunOperationB();
        }
    }
}
