﻿using PerformanceRecorder.Log;
using PerformanceRecorder.Manager;
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
        private ILogger Logger => StaticRecorderManager.Logger;

        public IRecordingTree GetResults()
        {
            return _resultTree;
        }

        public ICollection<IRecordingResult> GetFlatResults()
        {
            return GetResults().FlattenAndCombine().ToList();
        }

        public IRecordingTree RegisterMethod(IMethodDefinition methodDefinition)
        {
            return RegisterMethod(methodDefinition, null);
        }

        public IRecordingTree RegisterMethod(IMethodDefinition methodDefinition, IRecordingTree parent)
        {
            return AddNewMethodToTree(methodDefinition, parent);
        }

        public void RecordMethodDuration(IRecordingTree methodNode, double duration)
        {
            if (duration < 0.0)
            {
                string message = string.Format("Duration cannot be negative. Trying to add {0} duration", duration);
                Logger.Error(message);
                throw new ArgumentException(message);
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
                int depth = CalculateDepth(parentNode);
                IRecordingResultWithDepth result = new RecordingResultWithDepthImpl(methodDefinition, depth);
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
                var writableResult = (IWritableRecordingResult) methodNode.Value;
                writableResult.AddResult(duration);
            }
            else
            {
                string message = "Method node is null";
                Logger.Error(message);
                throw new ArgumentException(message);
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

        private int CalculateDepth(IRecordingTree parentNode)
        {
            if (parentNode == null || parentNode.Value == null)
            {
                return 0;
            }
            return parentNode.Value.Depth + 1;
        }
    }
}