using System;
using PerformanceRecorder.Log;

namespace ExampleApplication
{
    public class ConsoleLogger : ILogger
    {
        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(string message, Exception ex)
        {
            Console.WriteLine(message);
            Console.WriteLine(ex);
        }
    }
}