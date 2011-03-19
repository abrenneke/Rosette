using System;
using Rosette.Data;

namespace Rosette.Fractals
{
    /// <summary>Fractal definition. Holds the algorithms to generate a fractal image.</summary>
    public interface IFractal
    {
        /// <summary>Gets or sets the max iterations for testing a point of the fractal. Higher iterations take longer to process.</summary>
        int MaxIterations { get; set; }

        /// <summary>Gets the friendly name of the fractal to show in the UI.</summary>
        string FriendlyName { get; }

        /// <summary>Gets the bounds of the fractal.</summary>
        Bounds Bounds { get; }

        /// <summary>Tests whether the specified point lies in the fractal. Returns the normalized iteration count for reaching the conclusing. 
        /// A return value equal to MaxIterations means that the point lies within the fractal.</summary>
        float TestPoint(double pointReal, double pointImaginary);
    }
}
