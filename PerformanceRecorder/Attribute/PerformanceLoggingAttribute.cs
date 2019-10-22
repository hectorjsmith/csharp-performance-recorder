using AspectInjector.Broker;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections;
using System.Collections.Generic;

namespace PerformanceRecorder.Attribute
{
    [Aspect(Scope.Global)]
    [Injection(typeof(PerformanceLoggingAttribute))]
    public class PerformanceLoggingAttribute : System.Attribute
    {
        private static readonly Stack<(IMethodDefinition, double)> MethodStack = new Stack<(IMethodDefinition, double)>();

        [Advice(Kind.Before)]
        public void HandleBefore(
            [Argument(Source.Name)] string methodName,
            [Argument(Source.Instance)] object instance)
        {
            IMethodDefinition methodDefinition = GenerateMethodDefinition(instance.GetType(), methodName);
            MethodStack.Push((methodDefinition, GetCurrentTimeInMs()));
        }

        [Advice(Kind.After)]
        public void HandleAfter()
        {
            double endTime = GetCurrentTimeInMs();
            (IMethodDefinition method, double startTime) = MethodStack.Pop();

            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            recorder.RecordMethodDuration(method, endTime - startTime);
        }

        private double GetCurrentTimeInMs()
        {
            return (double)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
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