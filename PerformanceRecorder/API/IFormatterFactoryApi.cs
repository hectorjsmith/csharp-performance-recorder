using PerformanceRecorder.Formatter;

namespace PerformanceRecorder.API
{
    public interface IFormatterFactoryApi
    {
        bool IncludeNamespaceInString { get; set; }

        int DecimalPlacesInResults { get; set; }

        IStringResultWithDepthFormatter NewNestedStringResultFormatter();
        IStringResultFormatter NewPaddedStringResultFormatter();
        IStringResultFormatter NewPlainStringResultFormatter();
    }
}