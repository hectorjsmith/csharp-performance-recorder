using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PerformanceRecorder.Formatter.Helper
{
    /// <summary>
    /// General purpose extensions for <see cref="String"/>.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Repeat string a given number of times.
        /// </summary>
        public static String Repeat(this String input, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(input);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Align multiple lines in the input string such that the marker string is aligned across all lines.
        /// 
        /// Note that the marker string is removed from the output.
        /// </summary>
        /// <param name="marker">Marker string that all strings are aligned to.</param>
        /// <param name="input">Input string</param>
        /// <param name="alignRight">
        /// If set to true, whitespace will be inserted at the start of each line to get alignment - this causes the
        /// strings to appear right-aligned.
        /// If set to false, whitespace is inserted just before the marker string.
        /// </param>
        public static string AlignStringsToMarker(this string input, string marker, bool alignRight = false)
        {
            string[] lines = Regex.Split(input, Environment.NewLine);
            
            // Find the largest index of the marker string across all lines. All other lines will be aligned to this index.
            int targetIndex = lines
                .Select(line => line.IndexOf(marker, StringComparison.Ordinal))
                .Max();

            // Align all lines to the largest marker index
            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex];
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                int indexOfMarker = line.IndexOf(marker, StringComparison.Ordinal);
                if (alignRight)
                {
                    line = " ".Repeat(targetIndex - indexOfMarker) + line.Replace(marker, "");
                }
                else
                {
                    line = line.Replace(marker, " ".Repeat(targetIndex - indexOfMarker));
                }
                lines[lineIndex] = line;
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}