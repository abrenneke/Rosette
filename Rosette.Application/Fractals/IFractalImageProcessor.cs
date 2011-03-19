using Rosette.Data;

namespace Rosette.Fractals
{
    /// <summary>Processes a fractal, doing the legwork of the computations.</summary>
    public interface IFractalImageProcessor
    {
        /// <summary>Processes a fractal, computing its values, returning a RenderedFractal.</summary>
        RenderedFractal Process(IFractal fractal, Bounds bounds, Dimensions imageDimensions);
    }
}
