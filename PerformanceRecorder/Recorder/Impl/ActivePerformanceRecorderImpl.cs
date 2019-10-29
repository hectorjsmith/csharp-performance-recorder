using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Recorder.RecordingTree.Impl;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorder.Recorder.Impl
{
    internal class ActivePerformanceRecorderImpl : IPerformanceRecorder
    {
        private IRecordingTree _resultTree = new RecordingTreeImpl();

        public IRecordingTree GetResults()
        {
            return _resultTree;
        }

        public ICollection<IRecordingResult> GetFlatResults()
        {
            return GetResults().Flatten().ToList();
        }

        public void RegisterMethd(IMethodDefinition methodDefinition)
        {
            RegisterMethd(methodDefinition, null);
        }

        public void RegisterMethd(IMethodDefinition methodDefinition, IMethodDefinition parent)
        {
            AddNewMethodToTree(methodDefinition, parent);
        }

        public void RecordMethodDuration(IMethodDefinition methodDefinition, double duration)
        {
            if (duration < 0.0)
            {
                throw new ArgumentException(string.Format("Duration cannot be negative. Trying to add {0} duration", duration));
            }
            AddResult(methodDefinition, duration);
        }

        public void Reset()
        {
            _resultTree = new RecordingTreeImpl();
        }

        private void AddNewMethodToTree(IMethodDefinition methodDefinition, IMethodDefinition parent)
        {
            IRecordingTree node = _resultTree.Find(n => n.Id == methodDefinition.ToString());
            if (node == null)
            {
                IRecordingResult result = new RecordingResultImpl(methodDefinition);
                if (parent == null)
                {
                    // New top level node
                    _resultTree.AddChild(result);
                }
                else
                {
                    // Find parent
                    IRecordingTree parentNode = _resultTree.Find(n => n.Id == parent.ToString());

                    // New node
                    parentNode.AddChild(result);
                }
            }
        }

        private void AddResult(IMethodDefinition methodDefinition, double duration)
        {
            if (duration < 0.0)
            {
                throw new ArgumentException(string.Format("Cannot add negative method duration. Trying to add result {0} to method '{1}'"
                    ,duration, methodDefinition.ToString()));
            }

            IRecordingTree node = _resultTree.Find(n => n.Id == methodDefinition.ToString());
            if (node != null)
            {
                // Update node
                node.Value.AddResult(duration);
            }
            else
            {
                throw new ArgumentException("Unregistered method: " + methodDefinition.ToString());
            }
        }
    }
}