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
            api.SetLogger(new ConsoleLogger());

            // Enable performance logging (this will enable all attributes)
            api.EnablePerformanceRecording();

            // Run your code (any method decorated with an attribute will be recorded)
            IApplication app = new ApplicationImpl();
            app.RunPreApplication();
            app.RunApplication();

            api.RecordAction("customTopLevelAction", () =>
            {
                // ...
            });

            // Print out the results
            PrintResults(api);
        }

        private static void PrintResults(IPerformanceRecorderApi api)
        {
            // Get the results off the API
            IRecordingSessionResult results = api.GetResults();

            // Get formatter
            var formatterFactory = api.NewFormatterFactoryApi();
            
            // Hide the namespace in the output data
            formatterFactory.IncludeNamespaceInString = false;

            // Build formatters
            var nestedFormatter = formatterFactory.NewNestedStringResultFormatter();
            var paddedFormatter = formatterFactory.NewPaddedStringResultFormatter();
            
            // Use the built-in formatter to generate result output
            Console.Write(nestedFormatter.FormatAs(results));
            Console.WriteLine();
            Console.Write(paddedFormatter.FormatAs(results));
        }
    }
}