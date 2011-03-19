namespace Rosette.Extensions
{
    /// <summary>Static extensions for strings.</summary>
    public static class StringExtensions
    {
        /// <summary>Compares two string case-insensitively.</summary>
        public static bool IEquals(this string str, string other)
        {
            return str.Equals(other, System.StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Formats a string with string.format.</summary>
        public static string With(this string str, params object[] objects)
        {
            return string.Format(str, objects);
        }
    }
}
