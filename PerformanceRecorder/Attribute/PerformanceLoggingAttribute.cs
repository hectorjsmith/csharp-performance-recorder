using AspectInjector.Broker;
using PerformanceRecorder.Log;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Attribute
{
    [Aspect(Scope.Global)]
    [Injection(typeof(PerformanceLoggingAttribute))]
    public class PerformanceLoggingAttribute : System.Attribute
    {
        private static readonly Stack<RecorderStackItem> MethodStack = new Stack<RecorderStackItem>();

        private static ILogger Logger => StaticRecorderManager.Logger;

        [Advice(Kind.Around)]
        public object HandleAround(
            [Argument(Source.Name)] string methodName,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            try
            {
                return method(arguments);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in method: " + methodName, ex);
                HandleAfter(methodName);
                throw;
            }
        }

        [Advice(Kind.Before)]
        public void HandleBefore(
            [Argument(Source.Name)] string methodName,
            [Argument(Source.Instance)] object instance)
        {
            try
            {
                RegisterMethodBeforeItRuns(methodName, instance);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in HandleBefore code for method: " + methodName, ex);
            }
        }

        [Advice(Kind.After)]
        public void HandleAfter(
            [Argument(Source.Name)] string methodName)
        {
            try
            {
                RecordMethodDurationAfterItRuns();
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in HandleAfter code for method: " + methodName, ex);
            }
        }

        private void RegisterMethodBeforeItRuns(string methodName, object instance)
        {
            RecorderStackItem parent = null;
            if (MethodStack.Count > 0)
            {
                parent = MethodStack.Peek();
            }

            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            IMethodDefinition methodDefinition = GenerateMethodDefinition(instance.GetType(), methodName);
            IRecordingTree methodNode = recorder.RegisterMethd(methodDefinition, parent?.Node);
            MethodStack.Push(new RecorderStackItem(methodNode, GetCurrentTimeInMs()));
        }

        private void RecordMethodDurationAfterItRuns()
        {
            double endTime = GetCurrentTimeInMs();
            if (MethodStack.Count == 0)
            {
                throw new InvalidOperationException("Method not found on stack when trying to record duration");
            }

            RecorderStackItem item = MethodStack.Pop();
            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            recorder.RecordMethodDuration(item.Node, endTime - item.StartTime);
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
        public RecorderStackItem(IRecordingTree node, double startTime)
        {
            Node = node ?? throw new ArgumentNullException(nameof(node));
            StartTime = startTime;
        }

        public IRecordingTree Node { get; }

        public double StartTime { get; }
    }
}