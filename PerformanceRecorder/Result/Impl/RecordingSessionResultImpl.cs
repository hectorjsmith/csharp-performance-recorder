using PerformanceRecorder.Recorder.RecordingTree;
using PerformanceRecorder.Result.Formatter;
using PerformanceRecorder.Result.Formatter.Impl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PerformanceRecorder.Result.Impl
{
    internal class RecordingSessionResultImpl : IRecordingSessionResult
    {
        private readonly IResultFormatter<string> _plainFormatter = new PlainStringResultFormatterImpl();
        private readonly IResultFormatter<string> _paddedFormatter = new PaddedStringResultFormatterImpl();
        private readonly IResultFormatter<string> _nestedFormatter = new NestedStringResultFormatterImpl();
        private readonly IRecordingTree _treeData;

        private ICollection<IRecordingResult> _flatResultData;

        public RecordingSessionResultImpl(IRecordingTree treeData)
        {
            _treeData = treeData ?? throw new ArgumentNullException(nameof(treeData));
        }

        public bool IncludeNamespaceInString { get; set; } = true;

        public int Count => FlatResultData.Count;

        private ICollection<IRecordingResult> FlatResultData => _flatResultData ?? (_flatResultData = _treeData.Flatten().ToList());

        public ICollection<IRecordingResult> FlatData()
        {
            return FlatResultData;
        }

        public string ToPaddedString()
        {
            _paddedFormatter.IncludeNamespaceInString = IncludeNamespaceInString;
            return _paddedFormatter.FormatAs(_treeData);
        }

        public string ToRawString()
        {
            _plainFormatter.IncludeNamespaceInString = IncludeNamespaceInString;
            return _plainFormatter.FormatAs(_treeData);
        }

        public string ToNestedString()
        {
            _nestedFormatter.IncludeNamespaceInString = IncludeNamespaceInString;
            return _nestedFormatter.FormatAs(_treeData);
        }
    }
}