using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorder.Recorder.RecordingTree.Impl
{
    internal class RecordingTreeImpl : TreeNodeImpl<IRecordingResultWithDepth, IRecordingTree>, IRecordingTree
    {
        public RecordingTreeImpl(IRecordingResultWithDepth value) : base(value)
        {
        }

        public RecordingTreeImpl() : base()
        {
        }

        public IEnumerable<IRecordingResult> FlattenAndCombine()
        {
            return Flatten().GroupBy(r => r.Id).Select(group => new RecordingResultImpl(group));
        }

        public IRecordingTree Filter(Func<IRecordingResultWithDepth, bool> filterFunction)
        {
            return FilterTree(this, filterFunction);
        }

        protected override IRecordingTree GetDefault()
        {
            return null;
        }

        protected override IRecordingTree GetMe()
        {
            return this;
        }

        protected override IRecordingTree GetNew(IRecordingResultWithDepth value, IRecordingTree parent)
        {
            return new RecordingTreeImpl(value) { Parent = parent };
        }

        private IRecordingTree FilterTree(IRecordingTree node, Func<IRecordingResultWithDepth, bool> filterFunction)
        {
            IRecordingTree newTree = new RecordingTreeImpl(node.Value);
            if (node.Value != null && !filterFunction(node.Value))
            {
                return null;
            }

            foreach (IRecordingTree childNode in node.Children())
            {
                IRecordingTree newNode = FilterTree(childNode, filterFunction);
                if (newNode != null)
                {
                    newTree.AddChild(newNode);
                }
            }
            return newTree;
        }
    }
}