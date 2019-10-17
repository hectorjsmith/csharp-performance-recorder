using AspectInjector.Broker;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using System;

namespace PerformanceRecorder.Attribute
{
    [Aspect(Scope.Global)]
    [Injection(typeof(PerformanceLoggingAttribute))]
    public class PerformanceLoggingAttribute : System.Attribute
    {
        private static readonly IPerformanceRecorderFactory RecorderFactory = new PerformanceRecorderFactoryImpl();

        [Advice(Kind.Around)]
        public object HandleMethod(
            [Argument(Source.Name)] string methodName,
            [Argument(Source.Instance)] object instance,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            IPerformanceRecorder recorder = RecorderFactory.New();

            // Initializing return value to null here since it is used in the lambda
            object result = null;
            recorder.RecordExecutionTime(GeneratePerfLogName(instance.GetType(), methodName),
                () => result = method.Invoke(arguments));
            return result;
        }

        private string GeneratePerfLogName(Type parentType, string methodName)
        {
            return string.Format("{0}.{1}.{2}",
                parentType.Namespace,
                parentType.Name,
                methodName);
        }
    }
}
