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

        public ICollection<IRecordingResult> GetResults()
        {
            return _resultTree.Flatten().ToList();
        }

        public void MethodStart(IMethodDefinition methodDefinition, IMethodDefinition parent)
        {
            AddNewMethodToTree(methodDefinition, parent);
        }

        public void RecordMethodDuration(IMethodDefinition methodDefinition, double duration)
        {
            RecordMethodDuration(methodDefinition, null, duration);
        }

        public void RecordMethodDuration(IMethodDefinition methodDefinition, IMethodDefinition parent, double duration)
        {
            if (duration < 0.0)
            {
                throw new ArgumentException(string.Format("Duration cannot be negative. Trying to add {0} duration", duration));
            }
            AddResult(methodDefinition, parent, duration);
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

        private void AddResult(IMethodDefinition methodDefinition, IMethodDefinition parent, double duration)
        {
            IRecordingTree node = _resultTree.Find(n => n.Id == methodDefinition.ToString());
            if (node != null)
            {
                // Update node
                node.Value.AddResult(duration);
            }
        }
    }
}