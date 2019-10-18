using PerformanceRecorder.Attribute;

namespace ExampleApplication.Worker
{
    // You can add the attribute to entire classes (will apply to all properties and methods)
    [PerformanceLogging]
    internal class WorkerImpl : IWorker
    {
        public void RunOperationA()
        {
        }

        public void RunOperationB()
        {
        }
    }
}