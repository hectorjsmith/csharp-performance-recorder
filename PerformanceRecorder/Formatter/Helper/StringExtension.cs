using System;
using System.Text;

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
    }
}