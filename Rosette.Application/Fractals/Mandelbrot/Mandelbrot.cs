using System;
using Rosette.Data;
using Rosette.Fractals.Escapes;

namespace Rosette.Fractals.Mandelbrot
{
    public class Mandelbrot : IFractal
    {
        /// <summary>Gets or sets the max iterations of the mandelbrot. Defaults to 10.</summary>
        public int MaxIterations { get; set; }

        /// <summary>Returns the bounds of the Mandelbrot Set.</summary>
        public Bounds Bounds { get { return bounds; } } 
        private readonly Bounds bounds = new Bounds(new Bound(-2.0, 1.0), new Bound(-1.0, 1.0));

        /// <summary>Gets the friendly name of the mandelbrot set.</summary>
        public string FriendlyName { get { return "Mandelbrot Set"; } }

        private IEscapeAlgorithm escapeAlgorithm;

        public Mandelbrot(IEscapeAlgorithm escapeAlgorithm) 
        { 
            MaxIterations = 128;
            this.escapeAlgorithm = escapeAlgorithm;
        }

        public float TestPoint(double pointReal, double pointImaginary)
        {
            //(z) - starts at critical point z=0.
            double iterationPointReal = 0;
            double iterationPointImaginary = 0;

            var iterations = 0;
            bool HasEscaped = false;
            while (iterations < MaxIterations)
            {
                var originalIR = iterationPointReal;
                var iterationRealSquare = iterationPointReal * iterationPointReal;
                var iterationImaginarySquare = iterationPointImaginary * iterationPointImaginary;

                if (iterationRealSquare + (iterationPointImaginary * iterationPointImaginary) >= 500)
                {
                    HasEscaped = true;
                    break;
                }

                iterationPointReal = iterationRealSquare - iterationImaginarySquare + pointReal;
                iterationPointImaginary = 2 * originalIR * iterationPointImaginary + pointImaginary;

                HasEscaped = false;
                iterations++;
            }

            return escapeAlgorithm.GetNormalizedIterations(HasEscaped, iterationPointReal, iterationPointImaginary, iterations, MaxIterations);
        }
    }
}
