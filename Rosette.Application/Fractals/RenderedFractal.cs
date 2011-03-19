using Rosette.Data;

namespace Rosette.Fractals
{
    /// <summary>A fractal image which has been processed.</summary>
    public class RenderedFractal
    {
        /// <summary>Gets the raw data of the fractal. The image is flatted in to a single byte array.</summary>
        public float[] RawData { get; private set; }

        /// <summary>Gets the maximum number of iterations used to render the fractal.</summary>
        public int MaxIterations { get; private set; }

        /// <summary>Gets the bounds used to render the fractal.</summary>
        public Bounds Bounds { get; private set; }

        /// <summary>Gets the fractal that was rendered.</summary>
        public IFractal Fractal { get; private set; }

        /// <summary>Creates a new RenderedFractal with the data of the fractal.</summary>
        public RenderedFractal(float[] data, int maxIterations)
        {
            RawData = data;
            MaxIterations = maxIterations;
        }

        /// <summary>Creates a new RenderedFractal with the data of the fractal.</summary>
        public RenderedFractal(float[] data, int maxIterations, Bounds bounds, IFractal fractal) : this(data, maxIterations)
        {
            Bounds = bounds;
            Fractal = fractal;
        }
    }
}
