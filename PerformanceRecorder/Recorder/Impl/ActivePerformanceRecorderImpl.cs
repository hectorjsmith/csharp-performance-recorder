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
            return GetResults().FlattenAndCombine().ToList();
        }

        public IRecordingTree RegisterMethd(IMethodDefinition methodDefinition)
        {
            return RegisterMethd(methodDefinition, null);
        }

        public IRecordingTree RegisterMethd(IMethodDefinition methodDefinition, IRecordingTree parent)
        {
            return AddNewMethodToTree(methodDefinition, parent);
        }

        public void RecordMethodDuration(IRecordingTree methodNode, double duration)
        {
            if (duration < 0.0)
            {
                throw new ArgumentException(string.Format("Duration cannot be negative. Trying to add {0} duration", duration));
            }
            AddResult(methodNode, duration);
        }

        public void Reset()
        {
            _resultTree = new RecordingTreeImpl();
        }

        private IRecordingTree AddNewMethodToTree(IMethodDefinition methodDefinition, IRecordingTree parentNode)
        {
            IRecordingTree node = FindNode(methodDefinition, parentNode);
            if (node == null)
            {
                IRecordingResult result = new RecordingResultImpl(methodDefinition);
                if (parentNode == null)
                {
                    // New top level node
                    return _resultTree.AddChild(result);
                }
                else
                {
                    // New node
                    return parentNode.AddChild(result);
                }
            }
            return node;
        }

        private void AddResult(IRecordingTree methodNode, double duration)
        {
            if (methodNode != null)
            {
                // Update node
                methodNode.Value.AddResult(duration);
            }
            else
            {
                throw new ArgumentException("Method node is null");
            }
        }

        private IRecordingTree FindNode(IMethodDefinition methodDefinition, IRecordingTree parent)
        {
            if (parent == null)
            {
                return _resultTree.Children().Where(node => node.Value?.Id == methodDefinition.ToString()).FirstOrDefault();
            }
            else
            {
                return parent.Children().Where(node => node.Value?.Id == methodDefinition.ToString()).FirstOrDefault();
            }
        }
    }
}