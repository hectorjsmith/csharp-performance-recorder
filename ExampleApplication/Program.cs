using ExampleApplication.App;
using PerformanceRecorder.API.Impl;
using System;

namespace ExampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IApplication app = new ApplicationImpl(new PerformanceRecorderApiImpl());
            app.RunApplication();
            app.PrintResults();
        }
    }
}
