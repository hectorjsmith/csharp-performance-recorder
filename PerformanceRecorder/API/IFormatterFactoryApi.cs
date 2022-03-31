using PerformanceRecorder.Formatter;

namespace PerformanceRecorder.API
{
    /// <summary>
    /// Factory class for the standard result formatters included in the library.
    /// </summary>
    public interface IFormatterFactoryApi
    {
        /// <summary>
        /// Boolean flag to include the full namespace in the printed results or not.
        /// </summary>
        bool IncludeNamespaceInString { get; set; }

        /// <summary>
        /// Set the number of decimal places to include in the printed results. This applies to all time fields.
        /// </summary>
        int DecimalPlacesInResults { get; set; }

        /// <summary>
        /// Build a new <see cref="IStringResultWithDepthFormatter"/> instance.
        /// </summary>
        IStringResultWithDepthFormatter NewNestedStringResultFormatter();

        /// <summary>
        /// Build a new <see cref="IStringResultFormatter"/> instance.
        /// </summary>
        IStringResultFormatter NewPaddedStringResultFormatter();

        /// <summary>
        /// Build a new <see cref="IStringResultFormatter"/> instance.
        /// </summary>
        IStringResultFormatter NewPlainStringResultFormatter();
    }
}