using PerformanceRecorder.Recorder.RecordingTree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorder.Result.Impl
{
    internal class RecordingSessionResultImpl : IRecordingSessionResult
    {
        private readonly IRecordingTree _treeData;
        private ICollection<IRecordingResult> _flatResultData;

        public RecordingSessionResultImpl(IRecordingTree treeData)
        {
            _treeData = treeData ?? throw new ArgumentNullException(nameof(treeData));
        }

        public IRecordingTree RecordingTree => _treeData;
        
        public ICollection<IRecordingResult> FlatRecordingData =>
            _flatResultData ?? (_flatResultData = _treeData.FlattenAndCombine().ToList());
    }
}