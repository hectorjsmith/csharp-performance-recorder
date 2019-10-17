using ExampleApplication.App;
using PerformanceRecorder.API;
using PerformanceRecorder.API.Impl;
using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;

namespace ExampleApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Create a new instance of the performance logging API
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();

            // Enable performance logging (this will enable all attributes)
            api.EnablePerformanceRecording();

            // Run your code (any method decorated with an attribute will be recorded)
            IApplication app = new ApplicationImpl();
            app.RunApplication();

            // Print out the results
            PrintResults(api);
        }

        public static void PrintResults(IPerformanceRecorderApi api)
        {
            // Get the results off the API
            ICollection<IRecordingResult> results = api.GetResults();

            foreach (IRecordingResult result in results)
            {
                Console.WriteLine(string.Format("{0}.{1}: count: {2}  avg: {3}",
                    result.ClassName, result.MethodName, result.Count, result.Avg));
            }
            Console.WriteLine("Done");
        }
    }
}