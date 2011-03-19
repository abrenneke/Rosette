using System;
using Rosette.Data;

namespace Rosette.Fractals.Colorization
{
    /// <summary>A simple fractal colorizer.</summary>
    public class SimpleColorizer : IFractalColorizer
    {
        /// <summary>
        /// Gets a color to render the point as from a point,
        /// and whether the point has reached the maximum number of iterations (to be rendered differently)
        /// </summary>
        public Color GetColor(float point, bool IsMaxIterations)
        {
            if (IsMaxIterations)
                return System.Windows.Media.Colors.Black;
            return Color.FromRgb((byte)((12 * point) % 256), 0, (byte)Math.Round(255 % point));
        }

        /// <summary>Gets the name of the colorizer to show up in the UI.</summary>
        public string FriendlyName
        {
            get { return "Example"; }
        }
    }
}
