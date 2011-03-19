using System.Threading.Tasks;
using Rosette.Data;

namespace Rosette.Fractals
{
    /// <summary>Proceses a fractal in straight C# using a multithreaded approach.</summary>
    public class ParallelFractalImageProcessor : IFractalImageProcessor
    {
        private int width;
        private int height;

        private double xIncrement;
        private double yIncrement;

        private double minBoundsX;
        private double minBoundsY;

        /// <summary>Precomputed values used during the rendering to speed up calculations.</summary>
        private void PrecomputeValues(IFractal fractal, Bounds bounds)
        {
            xIncrement = bounds.Width / width;
            yIncrement = bounds.Height / height;
            minBoundsX = bounds.X.MinValue;
            minBoundsY = bounds.Y.MinValue;
        }

        /// <summary>Processes a fractal, computing its values, returning a RenderedFractal.</summary>
        public RenderedFractal Process(IFractal fractal, Bounds boundsToRender, Dimensions imageDimensions)
        {
            width = imageDimensions.Width;
            height = imageDimensions.Height;

            PrecomputeValues(fractal, boundsToRender);

            var buffer = new float[width * height];

            Parallel.For(0, height, y =>
            {
                Parallel.For(0, width, x =>
                {
                    SetPoint(buffer, fractal, x, y);
                });
            });

            return new RenderedFractal(buffer, fractal.MaxIterations, boundsToRender, fractal);
        }

        /// <summary>Sets a point on the image to the result of testing the point on the fractal.</summary>
        private void SetPoint(float[] buffer, IFractal fractal, int x, int y)
        {
            var transformedX = xIncrement * x + minBoundsX;
            var transformedY = yIncrement * y + minBoundsY;

            var iterations = fractal.TestPoint(transformedX, transformedY);

            buffer[(y * width) + x] = iterations;
        }
    }
}
