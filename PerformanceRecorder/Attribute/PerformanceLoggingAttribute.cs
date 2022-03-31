using AspectInjector.Broker;
using PerformanceRecorder.Log;
using PerformanceRecorder.Manager;
using PerformanceRecorder.Recorder.Worker;
using System;
using System.Reflection;

namespace PerformanceRecorder.Attribute
{
    /// <summary>
    /// Main attribute used to add instrumentation to a method, class, or property.
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(PerformanceLoggingAttribute))]
    public class PerformanceLoggingAttribute : System.Attribute
    {
        private static ILogger Logger => StaticRecorderManager.Logger;

        /// <summary>
        /// This method is called before an annotated function is called and wraps around it.
        /// </summary>
        [Advice(Kind.Around)]
        public object HandleAround(
            [Argument(Source.Metadata)] MethodBase metadata,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> method)
        {
            string methodName = GetMethodName(metadata);
            try
            {
                return method(arguments);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in method: " + methodName, ex);
                HandleAfter(metadata);
                throw;
            }
        }

        /// <summary>
        /// This method is called before an annotated function is executed.
        /// </summary>
        [Advice(Kind.Before)]
        public void HandleBefore(
            [Argument(Source.Metadata)] MethodBase metadata,
            [Argument(Source.Instance)] object instance)
        {
            string methodName = GetMethodName(metadata);
            try
            {
                StaticRecordingWorker.RegisterMethodBeforeItRuns(methodName, instance);
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in HandleBefore code for method: " + methodName, ex);
            }
        }

        /// <summary>
        /// This method is called after an annotated function is executed.
        /// </summary>
        [Advice(Kind.After)]
        public void HandleAfter(
            [Argument(Source.Metadata)] MethodBase metadata)
        {
            string methodName = GetMethodName(metadata);
            try
            {
                StaticRecordingWorker.RecordMethodDurationAfterItRuns();
            }
            catch (Exception ex)
            {
                Logger.Error("Exception in HandleAfter code for method: " + methodName, ex);
            }
        }

        private string GetMethodName(MethodBase methodBase)
        {
            return methodBase.Name;
        }
    }
}