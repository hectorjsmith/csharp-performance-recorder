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
        private readonly IPerformanceRecorderApi _api;

        [PerformanceLogging]
        private IWorker Worker => new WorkerImpl();

        public ApplicationImpl(IPerformanceRecorderApi api)
        {
            _api = api;
            api.EnablePerformanceRecording();
        }

        public void RunApplication()
        {
            for (int i = 0; i < 10; i++)
            {
                Worker.RunOperationA();
            }
        }

        public void PrintResults()
        {
            ICollection<IRecordingResult> results = _api.GetResults();
            foreach (IRecordingResult result in results)
            {
                Console.WriteLine(string.Format("{0}: count: {1}  avg: {2}", result.Id, result.Count, result.Avg));
            }
            Console.WriteLine("Done");
        }
    }
}
