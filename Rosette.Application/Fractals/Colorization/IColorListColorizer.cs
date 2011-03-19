using System.Collections.Generic;
using Rosette.Data;

namespace Rosette.Fractals.Colorization
{
    public interface IColorListColorizer : IFractalColorizer
    {
        /// <summary>Gets or sets the inner color of the colorizer. Typically makes up the "shape" of the fractal.</summary>
        Color InnerColor { get; set; }

        /// <summary>Gets or sets the list of colors used in the colorizer.</summary>
        IList<Color> Colors { get; set; }
    }
}
