using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Rosette.Windows
{
    /// <summary>Extension methods for the System.Windows namespace.
    public static class WindowExtensions
    {
        /// <summary>Gets the distance to another point.</summary>
        public static double DistanceTo(this Point point, Point other)
        {
            return Math.Sqrt(Math.Pow(point.X - other.X, 2) + Math.Pow(point.Y - other.Y, 2));
        }
    }
}
