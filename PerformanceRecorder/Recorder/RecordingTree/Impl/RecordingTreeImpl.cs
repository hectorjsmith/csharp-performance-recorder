using PerformanceRecorder.Result;

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