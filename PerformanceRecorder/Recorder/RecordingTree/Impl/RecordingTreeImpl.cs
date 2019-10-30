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
    }
}