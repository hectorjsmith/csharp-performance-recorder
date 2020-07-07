using AspectInjector.Broker;
using PerformanceRecorder.Log;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder.Worker;
using System;

namespace PerformanceRecorder.Attribute
{
    [Aspect(Scope.Global)]
    [Injection(typeof(PerformanceLoggingAttribute))]
    public class PerformanceLoggingAttribute : System.Attribute
    {
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
                StaticRecordingWorker.RegisterMethodBeforeItRuns(methodName, instance);
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
                StaticRecordingWorker.RecordMethodDurationAfterItRuns();
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in HandleAfter code for method: " + methodName, ex);
            }
        }
    }
}