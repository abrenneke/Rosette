using Rosette.Data;

namespace Rosette.Fractals.Colorization
{
    /// <summary>Class able to color a point on a fractal graph based on the value of the point.</summary>
    public interface IFractalColorizer
    {
        /// <summary>
        /// Gets a color to render the point as from a point, 
        /// and whether the point has reached the maximum number of iterations (to be rendered differently)
        /// </summary>
        Color GetColor(float value, bool IsMaxIterations);

        /// <summary>Gets the name of the colorizer to show up in the UI.</summary>
        string FriendlyName { get; }
    }
}
