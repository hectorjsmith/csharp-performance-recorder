using AspectInjector.Broker;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;

namespace PerformanceRecorder.Attribute
{
    [Aspect(Scope.Global)]
    [Injection(typeof(PerformanceLoggingAttribute))]
    public class PerformanceLoggingAttribute : System.Attribute
    {
        [Advice(Kind.Around)]
        public object HandleMethod(
            [Argument(Source.Name)] string methodName,
            [Argument(Source.Instance)] object instance,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            IMethodDefinition methodDefinition = GenerateMethodDefinition(instance.GetType(), methodName);

            // Initializing return value to null here since it is used in the lambda
            object result = null;
            recorder.RecordExecutionTime(methodDefinition,
                () => result = method.Invoke(arguments));
            return result;
        }

        private IMethodDefinition GenerateMethodDefinition(Type parentType, string methodName)
        {
            return new MethodDefinitionImpl(
                parentType.Namespace,
                parentType.Name,
                methodName);
        }
    }
}
