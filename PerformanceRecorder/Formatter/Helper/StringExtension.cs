using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PerformanceRecorder.Formatter.Helper
{
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
        public static string AlignStringsToMarker(this string input, string marker)
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
                int indexOfMarker = line.IndexOf(marker, StringComparison.Ordinal);
                line = line.Replace(marker, " ".Repeat(targetIndex - indexOfMarker));
                lines[lineIndex] = line;
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}