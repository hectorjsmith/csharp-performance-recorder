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
        private readonly IRecordingTree _treeData;

        private ICollection<IRecordingResult> _flatResultData;

        public RecordingSessionResultImpl(IRecordingTree treeData)
        {
            _treeData = treeData ?? throw new ArgumentNullException(nameof(treeData));
        }

        public bool IncludeNamespaceInString { get; set; } = true;

        public int DecimalPlacesInResults { get; set; } = 3;

        public int Count => FlatResultData.Count;

        private ICollection<IRecordingResult> FlatResultData => _flatResultData ?? (_flatResultData = _treeData.FlattenAndCombine().ToList());

        public ICollection<IRecordingResult> FlatData()
        {
            return FlatResultData;
        }

        public string ToRawString()
        {
            return ToRawString(r => true);
        }

        public string ToRawString(Func<IRecordingResult, bool> filterFunction)
        {
            IResultFormatter<string> formatter = new PlainStringResultFormatterImpl(IncludeNamespaceInString, DecimalPlacesInResults);
            return formatter.FormatAs(_treeData, filterFunction);
        }

        public string ToPaddedString()
        {
            return ToPaddedString(r => true);
        }

        public string ToPaddedString(Func<IRecordingResult, bool> filterFunction)
        {
            IResultFormatter<string> formatter = new PaddedStringResultFormatterImpl(IncludeNamespaceInString, DecimalPlacesInResults);
            return formatter.FormatAs(_treeData, filterFunction);
        }

        public string ToNestedString()
        {
            return ToNestedString(r => true);
        }

        public string ToNestedString(Func<IRecordingResult, bool> filterFunction)
        {
            IResultFormatter<string> formatter = new NestedStringResultFormatterImpl(IncludeNamespaceInString, DecimalPlacesInResults);
            return formatter.FormatAs(_treeData, filterFunction);
        }
    }
}