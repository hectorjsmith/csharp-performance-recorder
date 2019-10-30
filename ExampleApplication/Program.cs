using ExampleApplication.App;
using PerformanceRecorder.API;
using PerformanceRecorder.API.Impl;
using PerformanceRecorder.Result;
using System;

namespace ExampleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Create a new instance of the performance logging API
            IPerformanceRecorderApi api = new PerformanceRecorderApiImpl();

            // Enable performance logging (this will enable all attributes)
            api.EnablePerformanceRecording();

            // Run your code (any method decorated with an attribute will be recorded)
            IApplication app = new ApplicationImpl();
            app.RunPreApplication();
            app.RunApplication();

            // Print out the results
            PrintResults(api);
        }

        private static void PrintResults(IPerformanceRecorderApi api)
        {
            // Get the results off the API
            IRecordingSessionResult results = api.GetResults();

            // Hide the namespace in the output data
            results.IncludeNamespaceInString = false;

            // Use the built-in formatter to generate result output
            Console.Write(results.ToNestedString());
            Console.WriteLine();
            Console.Write(results.ToPaddedString());
        }
    }
}