using PerformanceRecorder.Formatter;
using PerformanceRecorder.Formatter.Impl;

namespace PerformanceRecorder.API
{
    class FormatterFactoryApiImpl : IFormatterFactoryApi
    {
        public bool IncludeNamespaceInString { get; set; } = true;

        public int DecimalPlacesInResults { get; set; } = 3;


        public IStringResultWithDepthFormatter NewNestedStringResultFormatter()
        {
            return new NestedStringResultFormatterImpl(IncludeNamespaceInString, DecimalPlacesInResults);
        }

        public IStringResultFormatter NewPaddedStringResultFormatter()
        {
            return new PaddedStringResultFormatterImpl(IncludeNamespaceInString, DecimalPlacesInResults);
        }

        public IStringResultFormatter NewPlainStringResultFormatter()
        {
            return new PlainStringResultFormatterImpl(IncludeNamespaceInString, DecimalPlacesInResults);
        }
    }
}