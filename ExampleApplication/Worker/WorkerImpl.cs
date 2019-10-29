using PerformanceRecorder.Attribute;

namespace ExampleApplication.Worker
{
    // You can add the attribute to entire classes (will apply to all properties and methods)
    [PerformanceLogging]
    internal class WorkerImpl : IWorker
    {
        public void RunOperationA()
        {
            RunPrivateOperationA1();
        }

        public void RunOperationB()
        {
            RunPrivateOperationB1();
            RunPrivateOperationB2();
        }

        private void RunPrivateOperationA1()
        {
        }

        private void RunPrivateOperationB1()
        {
        }

        private void RunPrivateOperationB2()
        {
            RunPrivateOperationB21();
        }

        private void RunPrivateOperationB21()
        {
        }
    }
}