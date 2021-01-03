using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;

namespace PerformanceRecorder.Recorder.Worker
{
    internal class StaticRecordingWorker
    {
        private static readonly Stack<RecorderStackItem> MethodStack = new Stack<RecorderStackItem>();

        public static void RegisterMethodBeforeItRuns(string methodName, object instance)
        {
            RecorderStackItem parent = null;
            if (MethodStack.Count > 0)
            {
                parent = MethodStack.Peek();
            }

            IPerformanceRecorder recorder = StaticRecorderManager.GetRecorder();
            IMethodDefinition methodDefinition = GenerateMethodDefinition(instance.GetType(), methodName);
            IRecordingTree methodNode = recorder.RegisterMethod(methodDefinition, parent?.Node);
            MethodStack.Push(new RecorderStackItem(methodNode, GetCurrentTimeInMs()));
        }

        public static void RecordMethodDurationAfterItRuns()
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

        private static double GetCurrentTimeInMs()
        {
            return (double)DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private static IMethodDefinition GenerateMethodDefinition(Type parentType, string methodName)
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