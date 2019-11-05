using System;
using System.Collections.Generic;
using System.Linq;
using PerformanceRecorder.Result;
using PerformanceRecorder.Result.Impl;

namespace PerformanceRecorder.Recorder.RecordingTree.Impl
{
    internal class RecordingTreeImpl : TreeNodeImpl<IRecordingResult, IRecordingTree>, IRecordingTree
    {
        public RecordingTreeImpl(IRecordingResult value) : base(value)
        {
        }

        public RecordingTreeImpl() : base()
        {
        }

        public IEnumerable<IRecordingResult> FlattenAndCombine()
        {
            return Flatten().GroupBy(r => r.Id).Select(group => new RecordingResultImpl(group));
        }

        protected override IRecordingTree GetDefault()
        {
            return null;
        }

        protected override IRecordingTree GetMe()
        {
            return this;
        }

        protected override IRecordingTree GetNew(IRecordingResult value, IRecordingTree parent)
        {
            return new RecordingTreeImpl(value) { Parent = parent };
        }

        public IRecordingTree Filter(Func<IRecordingResult, bool> filterFunction)
        {
            return FilterTree(this, filterFunction);
        }

        private IRecordingTree FilterTree(IRecordingTree node, Func<IRecordingResult, bool> filterFunction)
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