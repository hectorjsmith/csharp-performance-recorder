using ExampleApplication.Worker;
using PerformanceRecorder.Attribute;

namespace ExampleApplication.App
{
    internal class ApplicationImpl : IApplication
    {
        // You can add the attribute to properties
        [PerformanceLogging]
        private IWorker Worker => new WorkerImpl();

        // You can add the attribute to methods
        [PerformanceLogging]
        public void RunPreApplication()
        {
        }

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