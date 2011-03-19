using System;

namespace Rosette.Extensions
{
    /// <summary>Extensions for numerical data types.</summary>
    public static class NumberExtensions
    {
        /// <summary>Throws an ArgumentOutOfRangeException if the number is outside of the specified range.</summary>
        public static void MustBeWithinRange(this float num, float min, float max)
        {
            if (num < min || num > max)
                throw new System.ArgumentOutOfRangeException("Number was out of valid range. Valid range is {0} to {1}".With(min, max));
        }

        /// <summary>Throws an ArgumentOutOfRangeException if the number is outside of the specified range.</summary>
        public static void MustBeWithinRange(this int num, int min, int max)
        {
            if (num < min || num > max)
                throw new System.ArgumentOutOfRangeException("Number was out of valid range. Valid range is {0} to {1}".With(min, max));
        }

        /// <summary>Clamps a comparable object to a minimum and maximum.</summary>
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0) return min;
            else if (value.CompareTo(max) > 0) return max;
            else return value;
        }
    }
}
