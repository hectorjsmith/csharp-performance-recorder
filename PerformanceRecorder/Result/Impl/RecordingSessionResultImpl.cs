using PerformanceRecorder.Result.Formatter;
using PerformanceRecorder.Result.Formatter.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace PerformanceRecorder.Result.Impl
{
    class RecordingSessionResultImpl : IRecordingSessionResult
    {
        private IResultFormatter<string> _plainFormatter = new PlainStringResultFormatterImpl();
        private ICollection<IRecordingResult> _rawData;

        public RecordingSessionResultImpl(ICollection<IRecordingResult> rawData)
        {
            _rawData = rawData ?? throw new ArgumentNullException(nameof(rawData));
        }

        public bool IncludeNamespaceInString { get; set; }

        public ICollection<IRecordingResult> RawData()
        {
            return _rawData;
        }

        public string ToFormattedString()
        {
            return _plainFormatter.FormatAs(_rawData);
        }

        public string ToRawString()
        {
            throw new NotImplementedException();
        }
    }
}
