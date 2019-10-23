using PerformanceRecorder.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Recorder.RecordingTree.Impl
{
    class RecordingTreeImpl : TreeNodeImpl<IRecordingResult, IRecordingTree>, IRecordingTree
    {
        public RecordingTreeImpl(IRecordingResult value) : base(value)
        {
        }

        public RecordingTreeImpl() : base()
        {

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
