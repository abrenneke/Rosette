using Rosette.Data;
using System.Windows;

namespace Rosette.Fractals
{
    /// <summary>Class responsible for rendering an area of a fractal, and determining where to plot what points. Does not handle actual images or colors, just the ideas of a rendered fractal.</summary>
    public interface IFractalRenderer
    {
        /// <summary>Gets or sets the ImageProvider used for the FractalRenderer.</summary>
        IImageProvider ImageProvider { get; set; }

        /// <summary>Gets the Bounds of the last image rendered by this renderer.</summary>
        Bounds? LastRenderedBounds { get; set; }

        /// <summary>Gets or sets the last fractal rendered by the renderer.</summary>
        IFractal LastRenderedFractal { get; set; }

        /// <summary>Renders a fractal to an IImageProvider.</summary>
        RenderedFractal Render(IFractal fractal, Bounds areaToRender);

        /// <summary>Renders an already computer fractal.</summary>
        RenderedFractal Render(RenderedFractal renderedFractal);

        /// <summary>Gets the point given in screen coordinates in fractal coordinates.</summary>
        Point GetFractalPoint(Point screenPoint);

        /// <summary>Gets the bounds given in screen coordinates in fractal coordinates.</summary>
        Bounds GetFractalBounds(Bounds screenBounds);
    }
}
