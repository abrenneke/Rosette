using System;
using Rosette.Data;
using Rosette.Fractals.Mandelbrot;
using Rosette.Fractals.Escapes;

namespace Rosette.Fractals.JuliaSet
{
    class JuliaSet1 : IFractal
    {
        /// <summary>Gets or sets the max iterations of the Julia Set. Defaults to 128.</summary>
        public int MaxIterations { get; set; }

        /// <summary>Gets the friendly name of the Julia Set.</summary>
        public string FriendlyName
        { get { return "Julia Set 1"; }}

        /// <summary>Returns the bounds of the Julia Set.</summary>
        public Bounds Bounds { get { return bounds; } }
        private readonly Bounds bounds = new Bounds(new Bound(-1.5, 1.5), new Bound(-.08, .08));

        private IEscapeAlgorithm escapeAlgorithm;

        public JuliaSet1(IEscapeAlgorithm escapeAlgorithm) 
        { 
            MaxIterations = 128;
            this.escapeAlgorithm = escapeAlgorithm;
        }

        public float TestPoint(double pointReal, double pointImaginary)
        {
            var constantReal = (1.6180339887 - 2);
            var constantImaginary = (1.6180339887 - 1);

            var iterations = 0;
            double iterationPointReal = pointReal;
            double iterationPointImaginary = pointImaginary;
            bool HasEscaped = false;
            while (iterations < MaxIterations)
            {
                var originalIR = iterationPointReal;
                var iterationRealSquare = iterationPointReal * iterationPointReal;
                var iterationImaginarySquare = -iterationPointImaginary * iterationPointImaginary;
                iterationPointReal = iterationRealSquare + iterationImaginarySquare + constantReal;
                iterationPointImaginary = 2 * originalIR * iterationPointImaginary + constantImaginary;

                if (iterationPointReal >= 500 || iterationPointImaginary >= 500)
                {
                    HasEscaped = true;
                    break;
                }

                HasEscaped = false;
                iterations++;
            }

            return escapeAlgorithm.GetNormalizedIterations(HasEscaped, iterationPointReal, iterationPointImaginary, iterations, MaxIterations);
        }
    }
}
