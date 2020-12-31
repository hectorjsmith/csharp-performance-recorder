using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PerformanceRecorder.Formatter.Helper
{
    public static class StringExtension
    {
        public static String Repeat(this String input, int count)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(input);
            }
            return sb.ToString();
        }
        
        public static string AlignStringsToMarker(this string input, string marker)
        {
            string[] lines = Regex.Split(input, Environment.NewLine);
            int targetIndex = lines.Select(l => l.IndexOf(marker, StringComparison.Ordinal)).Max();

            for (int lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex];
                int indexOfMarker = line.IndexOf(marker);
                line = line.Replace(marker, " ".Repeat(targetIndex - indexOfMarker));
                lines[lineIndex] = line;
            }
            return string.Join(Environment.NewLine, lines);
        }
    }
}