using System;

namespace Lib.Extensions
{
    public static class StringExtensions
    {
        public static string ToCustomString(this DateTime? date)
        {
            return date?.ToString("d") ?? string.Empty;
        }
    }
}
