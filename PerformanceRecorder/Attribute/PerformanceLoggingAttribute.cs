using AspectInjector.Broker;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

namespace PerformanceRecorder.Attribute
{
    [Aspect(Scope.Global)]
    [Injection(typeof(PerformanceLoggingAttribute))]
    public class PerformanceLoggingAttribute : System.Attribute
    {
        private static readonly Stack<RecorderStackItem> MethodStack = new Stack<RecorderStackItem>();

        [Advice(Kind.Around)]
        public object HandleAround(
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            try
            {
                return method(arguments);
            }
            catch
            {
                HandleAfter();
                throw;
            }
        }

        [Advice(Kind.Before)]
        public void HandleBefore(
            [Argument(Source.Name)] string methodName,
            [Argument(Source.Instance)] object instance)
        {
            IMethodDefinition methodDefinition = GenerateMethodDefinition(instance.GetType(), methodName);

            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            MethodStack.TryPeek(out RecorderStackItem parent);
            recorder.RegisterMethd(methodDefinition, parent?.MethodDefinition);

            MethodStack.Push(new RecorderStackItem(methodDefinition, GetCurrentTimeInMs()));
        }

        [Advice(Kind.After)]
        public void HandleAfter()
        {
            double endTime = GetCurrentTimeInMs();
            RecorderStackItem item = MethodStack.Pop();
            
            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            recorder.RecordMethodDuration(item.MethodDefinition, endTime - item.StartTime);
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

    internal class RecorderStackItem
    {
        public RecorderStackItem(IMethodDefinition methodDefinition, double startTime)
        {
            MethodDefinition = methodDefinition ?? throw new ArgumentNullException(nameof(methodDefinition));
            StartTime = startTime;
        }

        public IMethodDefinition MethodDefinition { get; }

        public double StartTime { get; }
    }
}